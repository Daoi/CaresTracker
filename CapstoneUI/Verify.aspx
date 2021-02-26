<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Verify.aspx.cs" Inherits="CapstoneUI.Verify" Async="true" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="Content/bootstrap.min.css" rel="stylesheet" runat="server" />
    <script src="Scripts/jquery-3.0.0.min.js"></script>
    <script src="Scripts/bootstrap.min.js"></script>
    <script src="Scripts/popper.min.js"></script>
    <script src="../../Scripts/jquery-1.3.2.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery-ui-1.7.1.custom.min.js" type="text/javascript"></script>
    <link href="style/style.css" rel="stylesheet" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="loginContainer d-flex ">
            <div class="card mb-3 text-center mx-auto verifyCard my-auto ">
                <div class="card-header forestGreen">
                    PHA and Lenfest North Covid-19 Tracker
                </div>
                <div>
                    <img src="img/pha_logonew.png" class="loginImg" />
                </div>
                <div class="card-body row">
                    <div class="col-sm-3 text-left">
                        <h5 class="text-body fw-bold">Password Requirements:</h5>
                        <h6 class="text-body fw-bold">Use at least 8 characters</h6>
                        <h6 class="text-body fw-bold">Include one uppercase letter</h6>
                        <h6 class="text-body fw-bold">Include one lowercase letter</h6>
                        <h6 class="text-body fw-bold">Include one number</h6>
                    </div>
                    <div class="col">
                        <asp:Label ID="lblInstructions" runat="server" CssClass="h6 text-dark" Text="Enter your credentials and new password."></asp:Label>
                        <br />
                        <asp:Label ID="lblError" runat="server" CssClass="h6 alert-danger"></asp:Label>
                        <br />
                        <br />
                        <div class="form-group row justify-content-center">
                            <div class="col-md-9">
                                <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" placeholder="Username"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group row justify-content-center">
                            <div class="col-md-9">
                                <asp:TextBox ID="txtTemporaryPassword" TextMode="Password" runat="server" CssClass="form-control" placeholder="Temporary Password"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group row justify-content-center">
                            <div class="col-md-9">
                                <asp:TextBox ID="txtNewPassword" TextMode="Password" runat="server" CssClass="form-control" placeholder="New Password"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group row justify-content-center">
                            <div class="col-md-9">
                                <asp:TextBox ID="txtRetypePassword" TextMode="Password" runat="server" CssClass="form-control" placeholder="Confirm New Password"></asp:TextBox>
                            </div>
                        </div>
                        <asp:Button ID="btnConfirm" runat="server" Text="Verify" CssClass="btn-primary btn-lg" OnClick="btnConfirm_Click" />

                    </div>
                    <div class="col-sm-3"></div>
                </div>
                <div class="card-footer text-muted">
                    CARES Tracker
                </div>
            </div>
        </div>
    </form>
</body>
</html>

