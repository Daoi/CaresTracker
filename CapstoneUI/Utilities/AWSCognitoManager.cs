﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// for aws cognito
using Amazon;
using Amazon.Runtime;
using Amazon.CognitoIdentity;
using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;
using Amazon.Extensions.CognitoAuthentication;
using Amazon.SecurityToken;
using Amazon.SecurityToken.Model;
using System.Threading.Tasks;
using System.Configuration;
using Amazon.CognitoIdentity.Model;

namespace CapstoneUI.Utilities
{
    /// <summary>
    /// Class to manage interactions with AWS Cognito services.
    /// </summary>
    public class AWSCognitoManager
    {
        /// <summary>
        /// Secrets in appSettings.config
        /// </summary>
        private readonly string _clientID = ConfigurationManager.AppSettings["CLIENT_ID"];
        private readonly string _userPoolID = ConfigurationManager.AppSettings["USERPOOL_ID"];

        private readonly string _userIDPoolID = ConfigurationManager.AppSettings["USER_IDENTITYPOOL_ID"];

        private readonly string _adminIDPoolID = ConfigurationManager.AppSettings["ADMIN_IDENTITYPOOL_ID"];
        private readonly string _adminGroup = ConfigurationManager.AppSettings["ADMIN_GROUP"];

        /// <summary>
        /// User specific data
        /// </summary>
        private CognitoUser user;
        Dictionary<string, string> userAttributes;
        private AWSCredentials credentials;

        /// <summary>
        /// The same AWSCognitoManager throughout the application while the user is logged in.
        /// </summary>
        public AWSCognitoManager()
        {
            this.user = null;
            this.userAttributes = null;
            this.credentials = new AnonymousAWSCredentials();
        }
        
        /// <summary>
        /// Authenticates user credentials. Saves user info and credentials to class variables.
        /// </summary>
        /// <param name="username">Account username</param>
        /// <param name="password">Account password</param>
        /// <returns>Server response with access token.</returns>
        public async Task<AuthFlowResponse> SignInAsync(string username, string password)
        {
            using (var client = this.GetClient())
            {
                var pool = new CognitoUserPool(_userPoolID, _clientID, this.GetClient());
                var user = new CognitoUser(username, _clientID, pool, client);

                AuthFlowResponse authResponse = await user.StartWithSrpAuthAsync(new InitiateSrpAuthRequest()
                {
                    Password = password
                });

                this.user = user;
                this.userAttributes = await this.GetUserDetailsAsync();

                // get correct user permissions
                string poolID = this.userAttributes["custom:is_admin"] == "1" ? _adminIDPoolID : _userIDPoolID;
                this.credentials = this.user.GetCognitoAWSCredentials(poolID, RegionEndpoint.USEast1);

                return authResponse;
            }
        }

        /// <summary>
        /// Create a new user in the user pool.
        /// Sends a verification link to provided email.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password">8 characters: uppercase, lower</param>
        /// <param name="email"></param>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="phoneNumber">Must match the format: +12155555555</param>
        /// <param name="isAdmin">0: CHW, 1: Supervisors, Directors</param>
        /// <returns></returns>
        public async Task<SignUpResponse> CreateUserAsync(string username, string password, string email,
            string firstName, string lastName, string phoneNumber, int isAdmin = 0)
        {
            using (var client = this.GetClient())
            {
                var req = new SignUpRequest
                {
                    ClientId = _clientID,
                    Username = username,
                    Password = password
                };

                // add attributes
                var attrEmail = new AttributeType
                {
                    Name = "email",
                    Value = email
                };
                req.UserAttributes.Add(attrEmail);

                var attrFirstName = new AttributeType
                {
                    Name = "name",
                    Value = firstName
                };
                req.UserAttributes.Add(attrFirstName);

                var attrLastName = new AttributeType
                {
                    Name = "family_name",
                    Value = lastName
                };
                req.UserAttributes.Add(attrLastName);

                var attrPhone = new AttributeType
                {
                    Name = "phone_number",
                    Value = phoneNumber
                };
                req.UserAttributes.Add(attrPhone);

                var attrIsAdmin = new AttributeType
                {
                    Name = "custom:is_admin",
                    Value = isAdmin.ToString()
                };
                req.UserAttributes.Add(attrIsAdmin);

                var attrFirstLogin = new AttributeType
                {
                    Name = "custom:first_login",
                    Value = "1"
                };
                req.UserAttributes.Add(attrFirstLogin);

                var resp = await client.SignUpAsync(req);

                if (isAdmin == 1)
                {
                    await this.GrantUserAdminAsync(username);
                }

                return resp;
            }
        }

        /// <summary>
        /// Internal helper function to give a user admin privileges.
        /// Used for supervisor account creation.
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        private async Task<AdminAddUserToGroupResponse> GrantUserAdminAsync(string username)
        {
            using (var client = this.GetClient())
            {
                var req = new AdminAddUserToGroupRequest()
                {
                    UserPoolId = _userPoolID,
                    GroupName = _adminGroup,
                    Username = username
                };

                return await client.AdminAddUserToGroupAsync(req);
            }
        }

        /// <summary>
        /// Sends a verification code to user's email.
        /// Email must already have beeen verified.
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public async Task<ForgotPasswordResponse> SendForgotPasswordCodeAsync(string username)
        {
            using (var client = this.GetClient())
            {
                var req = new ForgotPasswordRequest()
                {
                    Username = username,
                    ClientId = _clientID
                };

                return await client.ForgotPasswordAsync(req);
            }
        }

        /// <summary>
        /// Changes the specified user's password.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password">New password</param>
        /// <param name="code">Verification code from email</param>
        /// <returns></returns>
        public async Task<ConfirmForgotPasswordResponse> ChangePasswordAsync(string username, string password, string code)
        {
            using (var client = this.GetClient())
            {
                var req = new ConfirmForgotPasswordRequest()
                {
                    Username = username,
                    Password = password,
                    ConfirmationCode = code,
                    ClientId = _clientID
                };

                return await client.ConfirmForgotPasswordAsync(req);
            }
        }

        /// <summary>
        /// Updates current user's account to show they have already logged in for the first time.
        /// This let's the system keep track of users that have not changed their temporary password.
        /// </summary>
        /// <returns></returns>
        public async Task<UpdateUserAttributesResponse> ConfirmFirstSignInAsync()
        {
            using (var client = this.GetClient())
            {
                List<AttributeType> lst = new List<AttributeType>()
                {
                    new AttributeType()
                    {
                        Name = "custom:first_login",
                        Value = "0"
                    }
                };

                var req = new UpdateUserAttributesRequest()
                {
                    UserAttributes = lst,
                    AccessToken = this.user.SessionTokens.AccessToken
                };

                return await client.UpdateUserAttributesAsync(req);
            }
        }

        /// <summary>
        /// Send a fresh email verification link to specified user.
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public async Task<ResendConfirmationCodeResponse> ResendVerificationLink(string username)
        {
            using (var client = this.GetClient())
            {
                var req = new ResendConfirmationCodeRequest()
                {
                    Username = username,
                    ClientId = _clientID
                };

                return await client.ResendConfirmationCodeAsync(req);
            }
        }

        /// <summary>
        /// Disable a user account so they cannot sign in.
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public async Task<AdminDisableUserResponse> DisableUserAsync(string username)
        {
            using (var client = this.GetClient())
            {
                var req = new AdminDisableUserRequest()
                {
                    Username = username,
                    UserPoolId = _userPoolID
                };

                return await client.AdminDisableUserAsync(req);
            }
        }

        /// <summary>
        /// Use to reenable a disabled account.
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public async Task<AdminEnableUserResponse> EnableUserAsync(string username)
        {
            using (var client = this.GetClient())
            {
                var req = new AdminEnableUserRequest()
                {
                    Username = username,
                    UserPoolId = _userPoolID
                };

                return await client.AdminEnableUserAsync(req);
            }
        }

        /// <summary>
        /// Delete an account from the user pool.
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public async Task<AdminDeleteUserResponse> DeleteUserAsync(string username)
        {
            using (var client = this.GetClient())
            {
                var req = new AdminDeleteUserRequest()
                {
                    Username = username,
                    UserPoolId = _userPoolID
                };

                return await client.AdminDeleteUserAsync(req);
            }
        }

        /// <summary>
        /// Get the user details from the user pool during authentication.
        /// </summary>
        /// <returns></returns>
        private async Task<Dictionary<string, string>> GetUserDetailsAsync()
        {
            Dictionary<string, string> toReturn = new Dictionary<string, string>();
            var resp = await user.GetUserDetailsAsync();
            foreach (AttributeType item in resp.UserAttributes)
            {
                toReturn.Add(item.Name, item.Value);
            }
            return toReturn;
        }

        /// <summary>
        /// Use to access user attributes after sign in.
        /// Dictionary keys match user pool attribute names.
        /// Create public accessors to use values outside of this class.
        /// </summary>
        /// <returns>A dictionary mapping attribute names to values.</returns>
        private Dictionary<string, string> GetUserAttributes()
        {
            return this.userAttributes;
        }

        /// <summary>
        /// Gets the username from the CognitoUser object.
        /// </summary>
        /// <returns>Username string</returns>
        public string Username
        {
            get { return this.user.Username; }
        }

        public string UserFirstName
        {
            get { return this.userAttributes["name"]; }
        }

        public string FullName
        {
            get { return this.userAttributes["name"] + ' ' + this.userAttributes["family_name"]; }
        }

        /// <summary>
        /// Use to setup admin/chw views and access throughout the application.
        /// </summary>
        public int IsAdmin
        {
            get { return int.Parse(this.userAttributes["custom:is_admin"]); }
        }

        /// <summary>
        /// Use to make sure users change their temporary passwords.
        /// </summary>
        public bool IsFirstLogin
        {
            get { return int.Parse(this.userAttributes["custom:first_login"]) == 1; }
        }

        /// <summary>
        /// Helper function to create a client from current credentials.
        /// </summary>
        /// <returns>Cognito client</returns>
        private AmazonCognitoIdentityProviderClient GetClient()
        {
            return new AmazonCognitoIdentityProviderClient(this.credentials);
        }
    }
}