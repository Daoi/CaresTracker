<%@ Page Title="" Language="C#" MasterPageFile="~/CapstoneUI.Master" AutoEventWireup="true" CodeBehind="ResidentProfile.aspx.cs" Inherits="CapstoneUI.ResidentProfile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container-fluid homepage backgroundblue justify-content-center">
        <div class="row  modal-header" style="height: 7%; font-size: large">
            <nav aria-label="breadcrumb">
                <ol class="breadcrumb bg-white">
                    <li class="breadcrumb-item" style="color: deepskyblue">
                        <asp:LinkButton ID="lnkHome" NavigateUrl="~/Homepage.aspx" runat="server" OnClick="lnkHome_Click">Dashboard</asp:LinkButton></li>
                    <li class="breadcrumb-item active bg-white" aria-current="page">Resident Profile</li>
                </ol>
            </nav>
            <asp:Label ID="lblPageInfo" runat="server" Enabled="true" Visible="true" CssClass="h3 my-2" Style="width: 74%"></asp:Label>
        </div>
        <div class="row justify-content-center pt-5">
            <div class="residentProfile col-4 text-center">
                <h2 class="h2 pt-4 mb-3">Resident Profile</h2>
                <h3>Resident Info</h3>
                <p class="residentParagraph text-left">First name: John</p>
                <p class="residentParagraph text-left">Last name: Doe</p>
                <p class="residentParagraph text-left">DOB: 1/1/1986</p>
                <h3>Resident Housing</h3>
                <p class="residentParagraph text-left">Address: 111 Philadelphia Street</p>
                <p class="residentParagraph text-left">Housing Type: PHA Development</p>
                <p class="residentParagraph text-left">Region: West Philadelphia </p>
                <h3>Covid-19 Test Info</h3>
                <p class="residentParagraph text-left">Date of last Covid-19 test: 1/1/2021</p>
                <p class="residentParagraph text-left">Last test result: Negative</p>
                <h3>Misc Info</h3>
                <p class="residentParagraph text-left">Number of interactions: 4</p>
                <asp:Button class="btn btn-primary mb-3 mt-3" ID="btnResidentInteractions" runat="server" Text="View Interactions" OnClick="btnResidentInteractions_Click" />
            </div>
        </div>


    </div>
</asp:Content>
