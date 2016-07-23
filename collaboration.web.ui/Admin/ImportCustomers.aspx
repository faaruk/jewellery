<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ImportCustomers.aspx.cs"
    MasterPageFile="~/DasbhoardMaster.Master" Inherits="Collaboration.Web.UI.Admin.ImportCustomers" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <script type="text/javascript">

    function uploadError(sender, args) {
        var errmsg = args.get_errorMessage();
        if (errmsg == "")
            $get("<%=dvMessage.ClientID%>").innerHTML = "There was some error in uploading the file";
        else
            $get("<%=dvMessage.ClientID%>").innerHTML = "Error : " + errmsg;

        $get("<%=dvMessage.ClientID%>").style.display = "block";
    }

    function uploadStarted(sender, args) {
        $get("<%=dvMessage.ClientID%>").innerHTML = "";
        $get("<%=dvMessage.ClientID%>").style.display = "none";

        var fileName = args.get_fileName();
        var fileExt = fileName.substring(fileName.lastIndexOf(".") + 1).toLowerCase();
        var filesizeuploaded = sender._inputFile.files[0].size;

        if ((fileExt == "xls" || fileExt == "xlsx") && (filesizeuploaded <= 5242880)) {
            return true;
        } else {
            //To cancel the upload, throw an error, it will fire OnClientUploadError
            var err = new Error();
            err.name = "Upload Error";
            err.message = "Please upload only Valid Image files ('.xls','.xlsx') with Image size upto 5 MB";
            throw (err);

            return false;
        }
    }

</script>
    <!-- page start-->
    <div class="panel">
        <div class="bio-graph-heading">
            Import Customers
        </div>
        <div class="panel-body">         
            <div id="dvMessage" runat="server" style="display: none" class="alert alert-block alert-danger fade in">
                <button data-dismiss="alert" class="close close-sm" type="button">
                    <i class="icon-remove"></i>
                </button>
                <asp:Literal ID="ltMessage" runat="server"></asp:Literal>
            </div>          
         <div class="form-group">
            <label class="col-lg-4 control-label">
                Upload Customer File</label>
            <div class="col-lg-6">
                <ajax:AsyncFileUpload ID="flUploadCustomer" Width="100%" runat="server" CompleteBackColor="Green"
                    UploaderStyle="Traditional" OnClientUploadError="uploadError" ErrorBackColor="Red"
                    OnUploadedComplete="flUploadCustomer_UploadedComplete" UploadingBackColor="#66CCFF"
                    OnClientUploadStarted="uploadStarted" />
            </div>
        </div>
        </div>
    </div>
</asp:Content>
