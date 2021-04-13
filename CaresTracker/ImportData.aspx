<%@ Page Title="" Language="C#" MasterPageFile="~/CaresTracker.Master" AutoEventWireup="true" CodeBehind="ImportData.aspx.cs" Inherits="CaresTracker.ImportData" Async="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="loginContainer justify-content-center d-flex py-5">
        <div class="card mb-3 text-center importCard my-auto ">
            <div class="card-header cherryBackground" style="font-size: 18px">
                Import Data
            </div>
            <div class="card-body justify-content-center">
                <div class="row" style="height: 7%; font-size: large">
                    <nav aria-label="breadcrumb" class="mb-n3 ml-2">
                        <ol class="breadcrumb bg-white">
                            <li class="breadcrumb-item" style="color: deepskyblue">
                                <asp:LinkButton ID="lnkHome" NavigateUrl="./Homepage.aspx" runat="server" OnClick="lnkHome_Click">Dashboard</asp:LinkButton></li>
                            <li class="breadcrumb-item active bg-white" aria-current="page">Import Data</li>
                        </ol>
                    </nav>
                </div>
                <h4>Instructions</h4>
                <div class="row justify-content-center">
                    <p class="w-50">
                        Click browse to select a file to upload. The file must be a .csv file.
                        After you choose the file, select the development this file relates to
                        from the dropdown box.
                    </p>
                </div>
                <div class="row justify-content-center text-secondary mt-3">
                    <div class="custom-file align-items-center">
                        <asp:FileUpload CssClass="fileUpload custom-file-input" ID="fileUpload" runat="server"></asp:FileUpload>
                        <label class="custom-file-label fileUpload" for="MainContent_fileUpload">Choose file</label>
                    </div>
                </div>
                <div class="row justify-content-center mt-3">
                    <asp:DropDownList ID="ddlDevelopments" CssClass="developmentsID" runat="server"></asp:DropDownList>
                </div>
                <div class="row justify-content-center mt-5">
                    <asp:Button ID="btnSubmitImport" runat="server" Text="Import Resident List" CssClass="buttonStyle" OnClick="btnSubmitImport_Click" />
                </div>
                <div runat="server" visible="false" id="divUploadErrors" class="row justify-content-center mt-5 fileUploadErrors">
                    <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
                </div>
            </div>
            <div class="card-footer text-muted">
                CARES Tracker
            </div>
        </div>
    </div>
    <script>
        $(document).ready(function () {
            $("#MainContent_ddlDevelopments").select2({
                placeholder: "Select Development",
                allowClear: true,
                selectOnClose: true
            });
        });
    </script>

    <script>
        //Change file label to file uploaded
        $(".custom-file-input").on("change", function () {
            var fileName = $(this).val().split("\\").pop();
            $(this).siblings(".custom-file-label").addClass("selected").html(fileName);
        });
    </script>

</asp:Content>
