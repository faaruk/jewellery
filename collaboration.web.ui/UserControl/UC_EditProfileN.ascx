<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UC_EditProfileN.ascx.cs" 
    Inherits="Collaboration.Web.UI.UserControl.UC_EditProfileN" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
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

        if ((fileExt == "jpeg" || fileExt == "jpg" || fileExt == "png" || fileExt == "gif") && (filesizeuploaded <= 5242880)) {
            return true;
        } else {
            //To cancel the upload, throw an error, it will fire OnClientUploadError
            var err = new Error();
            err.name = "Upload Error";
            err.message = "Please upload only Valid Image files (.jpg,.jpeg,'.gif','.png') with Image size upto 5 MB";
            throw (err);

            return false;
        }
    }

</script>
<div>
    <div id="dvMessage" runat="server" style="display: none" class="alert alert-block alert-danger fade in">
        <button data-dismiss="alert" class="close close-sm" type="button">
            <i class="icon-remove"></i>
        </button>
        <asp:Literal ID="ltMessage" runat="server"></asp:Literal>
    </div>
    <div class="panel-body bio-graph-info form-horizontal cmxform tasi-form">
        <div class="form-group">
            <label class="col-lg-4 control-label">
                User Name</label>
            <div class="col-lg-6">
                <asp:TextBox ID="txtUserName" runat="server" CssClass="form-control" MaxLength="50" AutoPostBack="true" ReadOnly="false" OnTextChanged="txtUserName_TextChanged"></asp:TextBox>
                <asp:RequiredFieldValidator Display="Dynamic" SetFocusOnError="true" ID="UserNameRequired"
                    runat="server" ControlToValidate="txtUserName" CssClass="failureNotification"
                    ForeColor="Red" ErrorMessage="User Name is required." InitialValue="" ValidationGroup="EditProfileValidationGroup"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator Display="Dynamic" ID="rgUserName" runat="server"
                    ErrorMessage="User Name cannot contain spaces" ValidationGroup="EditProfileValidationGroup"
                    ControlToValidate="txtUserName" CssClass="failureNotification" ForeColor="Red"
                    SetFocusOnError="true" InitialValue="" ValidationExpression="^[\S]*$">
                </asp:RegularExpressionValidator>
                <asp:CustomValidator ID="UniqUsernameValidator" runat="server" ForeColor="Red" Display="Dynamic"></asp:CustomValidator>
            </div>
        </div>
        <div class="form-group">
            <label class="col-lg-4 control-label">
                Role</label>
            <div class="col-lg-6">
                <asp:TextBox ID="txtUserType" runat="server" CssClass="form-control" MaxLength="50"
                    ReadOnly="true"></asp:TextBox>
                <asp:DropDownList ID="ddlUserTypes" runat="server" DataTextField="RoleType" CssClass="form-control m-bot15"
                    DataValueField="RoleID">
                </asp:DropDownList>
            </div>
        </div>
        <div class="form-group">
            <label class="col-lg-4 control-label">
                First Name</label>
            <div class="col-lg-6">
                <asp:TextBox ID="txtFirstName" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>
                <asp:RequiredFieldValidator SetFocusOnError="true" ID="FirstNameRequired" runat="server"
                    Display="Dynamic" ControlToValidate="txtFirstName" CssClass="failureNotification"
                    ForeColor="Red" ErrorMessage="First Name is required." InitialValue="" ValidationGroup="EditProfileValidationGroup"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="form-group">
            <label class="col-lg-4 control-label">
                Last Name</label>
            <div class="col-lg-6">
                <asp:TextBox ID="txtLastName" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>
                <asp:RequiredFieldValidator ID="LastNameRequired" runat="server" Display="Dynamic"
                    ControlToValidate="txtLastName" CssClass="failureNotification" ForeColor="Red"
                    SetFocusOnError="true" ErrorMessage="Last Name is required." ValidationGroup="EditProfileValidationGroup"
                    InitialValue=""></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="form-group">
            <label class="col-lg-4 control-label">
                Email</label>
            <div class="col-lg-6">
                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>
                <asp:RequiredFieldValidator SetFocusOnError="true" ID="EmailRequired" Display="Dynamic"
                    runat="server" ControlToValidate="txtEmail" CssClass="failureNotification" ForeColor="Red"
                    ErrorMessage="Email is required." ValidationGroup="EditProfileValidationGroup"
                    InitialValue=""></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator Display="Dynamic" ID="RegularExpressionValidator2"
                    runat="server" ErrorMessage="Please Enter Valid Email ID" ValidationGroup="EditProfileValidationGroup"
                    ControlToValidate="txtEmail" CssClass="failureNotification" ForeColor="Red" SetFocusOnError="true"
                    InitialValue="" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">
                </asp:RegularExpressionValidator>
            </div>
        </div>
        <div class="form-group">
            <label class="col-lg-4 control-label">
                Mobile</label>
            <div class="col-lg-6">
                <asp:TextBox ID="txtMobile" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label class="col-lg-4 control-label">
                Change Picture</label>
            <div class="col-lg-6">
                   <ajax:AsyncFileUpload ID="imgProfie" Width="100%" runat="server" CompleteBackColor="Green"
                    UploaderStyle="Traditional" OnClientUploadError="uploadError" ErrorBackColor="Red"
                    OnUploadedComplete="ImgProfie_UploadedComplete" UploadingBackColor="#66CCFF"
                    OnClientUploadStarted="uploadStarted"  />
                   <%-- <asp:UpdatePanel ID="UpdatePanel" runat="server" UpdateMode="Conditional">
                      <ContentTemplate>
                    <asp:FileUpload ID="imgProfilePic" Width="100%" runat="server" />
                    
                    </ContentTemplate>
                  
                    </asp:UpdatePanel>--%>
            </div>
        </div>
        
        <div class="form-group">
            <div class="col-lg-offset-4 col-lg-10">
                <asp:Button runat="server" Text="Save" CssClass="btn btn-primary" ID="btnUpdate"
                    OnClick="btnUpdate_Click" ValidationGroup="EditProfileValidationGroup"></asp:Button>
                <asp:Button data-dismiss="modal" runat="server" ID="btnCancel" Text="Close" 
                    CssClass="btn btn-default" onclick="btnCancel_Click">
                </asp:Button>
            </div>
        </div>
        <%--   </form> --%>
    </div>
</div>
