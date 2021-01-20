<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="CapstoneUI.WebForm1" %>

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
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="loginContainer d-flex ">
            <div class="card-mb3 text-center mx-auto loginCard my-auto ">
                <div class="card-header forestGreen">
                    PHA and Lenfest North Covid-19 Tracker
                </div>
                <div>
                    <img src="img/pha_logonew.png" class="loginImg" />
                </div>
                <div class="card-body">
                    <br />
                    <div class="form-group row justify-content-center">
                        <div class="col-md-6">
                            <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" placeholder="Username"></asp:TextBox>
                        </div>
                    </div>

                    <br />
                    <div class="form-group row justify-content-center">
                        <div class="col-md-6">
                            <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" placeholder="Password"></asp:TextBox>
                        </div>
                    </div>
                    <p class="card-text" style="color: deepskyblue">Forgot Password or Username?</p>
                    <asp:Button ID="btnLogin" runat="server" Text="Login" CssClass="btn-primary btn-lg" OnClick="btnLogin_Click" />
                </div>
                <div class="card-footer text-muted">
                    Brand Text
                </div>
            </div>
        </div>
    </form>
</body>
</html>
