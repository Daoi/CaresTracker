<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="CapstoneUI.WebForm1" Async="true" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="Content/bootstrap.min.css" rel="stylesheet" runat="server" />
    <script src="Scripts/jquery-3.0.0.min.js"></script>
    <script src="Scripts/bootstrap.min.js"></script>
    <script src="Scripts/popper.min.js"></script>
    <script src="../../Scripts/jquery-1.3.2.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery-ui-1.7.1.custom.min.js" type="text/javascript"></script>
    <link href="style/loginStyle.css" rel="stylesheet" />
    <link href="style/style.css" rel="stylesheet" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="backgroundImage d-flex">
            <asp:Panel ID="pnlCard" runat="server" CssClass="card mb-3 text-center mx-auto loginCard my-auto">
                <div class="card-header cherryBackground">
                    PHA and Lenfest North CARES Tracker
                </div>
                <div>
                    <img src="img/Temple_University_logo.png" class="mt-5 loginImg" />
                </div>
                <div class="card-body">
                    <!-- Login Panel -->
                    <asp:Panel ID="pnlLogin" runat="server">
                        <asp:Label ID="lblInstructions" runat="server" CssClass="text-black-50" Text="Enter your username and password."></asp:Label>
                        <br />
                        <asp:Label ID="lblError" runat="server" CssClass="h6 alert-danger"></asp:Label>
                        <br />
                        <br />
                        <div class="form-group row justify-content-center">
                            <div class="col-md-6">
                                <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" placeholder="Username"></asp:TextBox>
                            </div>
                        </div>
                        <br />
                        <div class="form-group row justify-content-center">
                            <div class="col-md-6">
                                <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control" placeholder="Password"></asp:TextBox>
                            </div>
                        </div>
                        <asp:Button ID="btnLogin" runat="server" Text="Login" CssClass="btn-lg cherryBackground" OnClick="btnLogin_Click" />
                        <br />
                        <asp:Button ID="btnForgotPassword" runat="server" CssClass="btn btn-link font-weight-bold cherryFont" Text="Forgot Password?" OnClick="switchPanels" />
                    </asp:Panel>
                    <!-- Password Reset Panel -->
                    <asp:Panel ID="pnlPasswordReset" runat="server" Visible="false">
                        <asp:Label ID="lblPRInstructions" runat="server" CssClass="text-black-50" Text="Enter your username to email your verification code."></asp:Label>
                        <div class="row">
                            <div class="col-sm-3 text-left my-auto">
                                <h5 class="text-body fw-bold">Password Requirements:</h5>
                                <h6 class="text-body fw-bold">Use at least 8 characters</h6>
                                <h6 class="text-body fw-bold">Include one uppercase letter</h6>
                                <h6 class="text-body fw-bold">Include one lowercase letter</h6>
                                <h6 class="text-body fw-bold">Include one number</h6>
                            </div>
                            <div class="col">
                                <br />
                                <asp:Label ID="lblPRError" runat="server" CssClass="h6 alert-danger"></asp:Label>
                                <br />
                                <br />
                                <div class="form-group row justify-content-center">
                                    <div class="col-md-9">
                                        <asp:TextBox ID="txtPRUsername" runat="server" CssClass="form-control" placeholder="Username"></asp:TextBox>
                                    </div>
                                </div>

                                <br />
                                <div class="form-group row justify-content-center">
                                    <div class="col-md-9">
                                        <asp:TextBox ID="txtPRVerificationCode" runat="server" CssClass="form-control" placeholder="Verification Code" Enabled="false"></asp:TextBox>
                                    </div>
                                </div>

                                <br />
                                <div class="form-group row justify-content-center">
                                    <div class="col-md-9">
                                        <asp:TextBox ID="txtPRNewPassword" runat="server" TextMode="Password" CssClass="form-control" placeholder="New Password" Enabled="false"></asp:TextBox>
                                    </div>
                                </div>

                                <br />
                                <div class="form-group row justify-content-center">
                                    <div class="col-md-9">
                                        <asp:TextBox ID="txtPRRetypePassword" runat="server" TextMode="Password" CssClass="form-control" placeholder=" Retype Password" Enabled="false"></asp:TextBox>
                                    </div>
                                </div>
                                <br />
                                <asp:Button ID="btnPRConfirm" runat="server" Text="Change Password" CssClass="btn-secondary btn-lg" OnClick="btnPRConfirm_Click" Enabled="false" />
                                <br />
                                <asp:Button ID="btnPRGoBack" runat="server" CssClass="btn btn-link font-weight-bold h3 mt-2" Text="Back to Login" OnClick="switchPanels" />
                            </div>
                            <div class="col-sm-3 text-left">
                                <br />
                                <br />
                                <br />
                                <asp:Button ID="btnPRSendCode" runat="server" CssClass="btn btn-warning" Text="Send Code" OnClick="btnPRSendCode_Click" />
                            </div>
                        </div>
                    </asp:Panel>
                </div>
                <div class="card-footer text-muted">
                    CARES Tracker
                </div>
            </asp:Panel>
        </div>
    </form>
</body>
</html>
