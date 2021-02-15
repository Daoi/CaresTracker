<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestPage.aspx.cs" Inherits="CapstoneUI.TestPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Create new CHW" />
        </div>
        <asp:Panel ID="Panel1" runat="server" Height="342px" Width="1281px">
            <br />
            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
            <asp:Label ID="Label1" runat="server" Text="Username"></asp:Label>
            <br />
            <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
            <asp:Label ID="Label2" runat="server" Text="Password"></asp:Label>
            <br />
            <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
            <asp:Label ID="Label3" runat="server" Text="First Name"></asp:Label>
            <br />
            &nbsp;<asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
            <asp:Label ID="Label4" runat="server" Text="Last Name"></asp:Label>
            <br />
            &nbsp;<asp:TextBox ID="TextBox5" runat="server"></asp:TextBox>
            <asp:Label ID="Label5" runat="server" Text="Email"></asp:Label>
            <br />
            &nbsp;<asp:TextBox ID="TextBox6" runat="server"></asp:TextBox>
            <asp:Label ID="Label6" runat="server" Text="Phone"></asp:Label>
            <br />
        </asp:Panel>
        <p>
            &nbsp;</p>

        <h2>
            Look Up User
        </h2>
        <asp:TextBox ID="TextBox7" runat="server"></asp:TextBox>
        <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Search" />
        <br />

                <p>
                    <asp:Label ID="Label7" runat="server" Text="Label"></asp:Label>
        </p>
        <p>
            &nbsp;</p>
        <p>
            &nbsp;</p>
    </form>
</body>
</html>
