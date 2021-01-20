<%@ Page Title="" Language="C#" MasterPageFile="~/CapstoneUI.Master" AutoEventWireup="true" CodeBehind="NewResident.aspx.cs" Inherits="CapstoneUI.NewResident" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid homepage backgroundblue justify-content-center">

        <div class="row text-center justify-content-center">
            <h2 class="h2 pt-4 mb-3">New Resident Profile</h2>
        </div>

        <div class="row justify-content-center pt-5">
            <div class="newResident col-3 text-center">
                <h3 class="mt-1">Resident Info</h3>
                <div class="form-group text-left text-light justify-content-center">
                    <label for="txtResidentFirstName">First Name:</label>
                    <asp:TextBox ID="txtResidentFirstName" class="form-control " placeholder="Jane" runat="server"></asp:TextBox>
                </div>
                <div class="form-group text-left text-light">
                    <label for="txtResidentLastName">Last Name:</label>
                    <asp:TextBox ID="txtResidentLastName" class="form-control " placeholder="Doe" runat="server"></asp:TextBox>
                </div>
                <div class="form-group text-left text-light">
                    <label for="txtDOB">Date of Birth:</label>
                    <asp:TextBox ID="txtDOB" class="form-control " placeholder="01/01/2020" runat="server"></asp:TextBox>
                </div>
                <h3 class="pt-3">Resident Housing</h3>
                <div class="form-group text-left text-light">
                    <label for="txtAddress">Address:</label>
                    <asp:TextBox ID="txtAddress" class="form-control " placeholder="111 Philadelphia Street" runat="server"></asp:TextBox>
                </div>
                <div class="form-group text-left text-light">
                    <label for="txtHousingType">Housing Type:</label>
                    <asp:TextBox ID="txtHousingType" class="form-control " placeholder="Development" runat="server"></asp:TextBox>
                </div>
                <div class="form-group text-left text-light">
                    <label for="txtRegion">Region:</label>
                    <asp:TextBox ID="txtRegion" class="form-control " placeholder="West Philadelphia" runat="server"></asp:TextBox>
                </div>

                <asp:Button class="btn btn-primary mb-3 mt-3" ID="btnCreateResident" runat="server" Text="Create" />
            </div>
        </div>

    </div>

</asp:Content>
