<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UC_Messages.ascx.cs"
    Inherits="Collaboration.Web.UI.UserControl.UC_Messages" %>
<%@ Import Namespace="Collaboration.Web.UI" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
  

<style type="text/css">
    .divAttachment {
        border: 2px solid #8ac007;
        border-radius: 25px;
        height: auto;
        margin-top: 92px;
        padding: 0 0 10px 15px;
        width: 100%;
    }

    .spanAttachment {
        width: auto;
        display: inline-block;
        margin-top: -30px;
        margin-left: 5px;
        background: #f1f2f7;
        overflow: auto;
        margin: -30px 6px 7px 16px;
    }

    .spanAttachmentAlt {
        width: auto;
        display: inline-block;
        margin-top: -30px;
        margin-left: 5px;
        background: white;
        overflow: auto;
        margin: -30px 6px 7px 16px;
    }
</style>



<%--<script type="text/javascript">

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
        // var fileExt = fileName.substring(fileName.lastIndexOf(".") + 1).toLowerCase();
        var filesizeuploaded = sender._inputFile.files[0].size;

        if (filesizeuploaded <= 5242880) {
            $get("dvMessageSuccess").style.display = "block";
            return true;            
        } else {
            //To cancel the upload, throw an error, it will fire OnClientUploadError
            var err = new Error();
            err.name = "Upload Error";
            err.message = "Please upload file with size upto 5 MB";
            throw (err);

            return false;
        }        
    }

    function uploadComplete(sender, args) {
        setTimeout(function () {
            $get("dvMessageSuccess").style.display = "none";
        }, 1000);
    }

   
</script>--%>

<script type="text/javascript">

    $(".ajax__fileupload_dropzone").css("visibility", "hidden");

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

        if (filesizeuploaded <= 5242880) {
            return true;
        } else {
            //To cancel the upload, throw an error, it will fire OnClientUploadError
            var err = new Error();
            err.name = "Upload Error";
            err.message = "Please upload file with size upto 5 MB";
            throw (err);

            return false;
        }
    }
    function uploadCompleteNew(sender, e) {
        if (sender._filesInQueue[sender._filesInQueue.length - 1]._isUploaded) {

        }
        //$(".removeButton").css("visibility", "visible");
        $(".removeButton").each(function () {
            if (!$(this).parent().find(".ajaxRemoveButton").length) {

                $('<div class="ajaxRemoveButton">Remove</div>').insertAfter($(this));
            }
        });
        //$(".removeButton").addClass("ajaxRemoveButton").removeClass("removeButton");

    }


</script>
<div>
    <div id="dvMessage" runat="server" style="display: none" class="alert alert-block alert-danger fade in">
        <button data-dismiss="alert" class="close close-sm" type="button">
            <i class="icon-remove"></i>
        </button>
        <asp:Literal ID="ltMessage" runat="server"></asp:Literal>
    </div>
    <div id="dvMessageSuccess" style="display: none" class="alert alert-block alert-success fade in">
        Uploading File ...
    </div>
    <div style="height: 500px;">
        <asp:UpdatePanel ID="updGrid" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="panel-body bio-graph-info form-horizontal cmxform tasi-form" style="height: 500px; overflow-y: auto; padding: top:0px;">
                    <asp:PlaceHolder ID="NoMessagePlaceHolder" runat="server" Visible="false" ViewStateMode="Disabled">
                        <div class="row" style="background: #f1f2f7; margin-left: 5px; margin-right: 5px;">
                            No messages for this order. 
                        </div>
                    </asp:PlaceHolder>
                    <asp:ListView ID="gvTable" runat="server" ShowHeader="false" AutoGenerateColumns="False"
                        DataKeyNames="MessageID" AllowSorting="false" OnItemDataBound="gvTable_RowDataBound">
                        <%--                        <Columns>
                            <asp:TemplateField>--%>
                        <ItemTemplate>
                            <div class="row" style="background: #f1f2f7; margin-left: 5px; margin-right: 5px;">
                                <div class="col-lg-2" style="padding: 0px 0px 0px 15px;">
                                    <div class="panel" style="background: #f1f2f7;">
                                        <div>
                                            <h6>
                                                <b>
                                                    <%# Eval("SentFromUserName")%></b>
                                            </h6>
                                            <%# Eval("CreateDate",Resource.FormatDateTimeForBinders)%>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-10 pull-left">
                                    <div class="panel-body" style="padding: 8px 0px;">
                                        <div class="msg-time-chat">
                                            <div class="msg-in" style="margin-left: 5px;">
                                                <div class="text" style="border: 0px">
                                                    <p>
                                                        <asp:Label Visible="true" runat="server" ID="lblMessageText" Text='<%# Eval("MessageText")%>'></asp:Label>
                                                    </p>
                                                    <div class="pull-right ">
                                                        <asp:Label runat="server" Visible="false" ID="lblLocationURL" Text='<%# Eval("LocationURL")%>' />
                                                        <asp:Label runat="server" Visible="false" ID="lblContentType" Text='<%# Eval("ContentType")%>' />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div runat="server" id="divAttachment" class="divAttachment">
                                    <span class="spanAttachment">Attached File</span>
                                    <div style="cursor: pointer;">
                                        <img alt="" style="width: 20px; height: 20px; margin-left: 5px;" src="../img/attach.png" />
                                        <p style="cursor: pointer; padding-left: 35px; margin-top: -19px;">
                                            <asp:LinkButton ID="lblAttachedFile" runat="server" OnClick="Download_Click" />
                                            <asp:Label ID="lblSize" runat="server" />
                                        </p>
                                    </div>
                                </div>
                            </div>
                        </ItemTemplate>
                        <AlternatingItemTemplate>
                            <div class="row" style="background: ; margin-left: 5px; margin-right: 5px;">
                                <div class="col-lg-2" style="padding: 0px 0px 0px 15px;">
                                    <div class="panel">
                                        <div>
                                            <h6>
                                                <b>
                                                    <%# Eval("SentFromUserName")%></b>
                                            </h6>
                                            <%# Eval("CreateDate",Resource.FormatDateTimeForBinders)%>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-10 pull-left">
                                    <div class="panel-body" style="padding: 8px 0px;">
                                        <div class="msg-time-chat">
                                            <div class="msg-in" style="margin-left: 5px;">
                                                <div class="text" style="border: 0px">
                                                    <p>
                                                        <asp:Label Visible="true" runat="server" ID="lblMessageText" Text='<%# Eval("MessageText")%>'></asp:Label>
                                                    </p>
                                                    <div class="pull-right ">
                                                        <asp:Label runat="server" Visible="false" ID="lblLocationURL" Text='<%# Eval("LocationURL")%>' />
                                                        <asp:Label runat="server" Visible="false" ID="lblContentType" Text='<%# Eval("ContentType")%>' />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div runat="server" id="divAttachmentAlt" class="divAttachment">
                                    <span class="spanAttachmentAlt">Attached File</span>
                                    <div style="cursor: pointer;">
                                        <img alt="" style="width: 20px; height: 20px; margin-left: 5px;" src="../img/attach.png" />
                                        <p style="cursor: pointer; padding-left: 35px; margin-top: -19px;">
                                            <asp:LinkButton ID="lblAttachedFile" runat="server" OnClick="Download_Click" />
                                            <asp:Label ID="lblSize" runat="server" />
                                        </p>
                                    </div>
                                </div>
                            </div>
                        </AlternatingItemTemplate>
                        <%--  </asp:TemplateField>
                        </Columns>--%>
                    </asp:ListView>
                    <div class="row" style="margin-left: 5px; margin-right: 5px; margin-bottom: 5px;">
                        <div class="chat-form" style="margin-top: 100px; padding-left: 15px; padding-right: 15px">
                            <div class="input-cont" style="margin-top: 18px;">
                                <textarea rows="2" cols="108" cssclass="form-control col-lg-12 textarea" runat="server"
                                    id="txtMessage" placeholder="Type a message here..."></textarea><br />
                                <asp:RequiredFieldValidator Display="Dynamic" SetFocusOnError="true" ID="RequiredFieldValidator1"
                                    runat="server" ControlToValidate="txtMessage" CssClass="failureNotification"
                                    ForeColor="Red" ErrorMessage="Message Text is required." InitialValue="" ValidationGroup="AddMessageValidationGroup"></asp:RequiredFieldValidator>
                            </div>
                            <div class="pull-left" style="margin-top: 18px; padding-left: 0px">
                                <a href="javascript:;">Assign Message To</a>
                                <asp:DropDownList ID="ddlUsers" runat="server" DataTextField="UserName" CssClass="input-sm m-bot15"
                                    DataValueField="UserID">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator Display="Dynamic" SetFocusOnError="true" ID="RequiredFieldValidator2"
                                    runat="server" ControlToValidate="ddlUsers" CssClass="failureNotification" ForeColor="Red"
                                    ErrorMessage="Please select Assignee" InitialValue="0" ValidationGroup="AddMessageValidationGroup"></asp:RequiredFieldValidator>
                                <%-- <ajax:AsyncFileUpload ID="flFile" Width="80%" runat="server" CompleteBackColor="Green"
                                    UploaderStyle="Traditional" OnClientUploadError="uploadError" ErrorBackColor="Red"
                                    OnUploadedComplete="Attachment_UploadedComplete" UploadingBackColor="#66CCFF"
                                    OnClientUploadStarted="uploadStarted" OnClientUploadComplete="uploadComplete" /> --%>
                        

    
                                <ajax:AjaxFileUpload ID="specimenFileUpload" runat="server"
                                    OnUploadComplete="File_Upload" OnClientUploadComplete="uploadCompleteNew" MaximumNumberOfFiles="1" />

          

                            </div>
                            <div class="form-group pull-right" style="margin-top: 18px; padding-right: 10px">
                                <div class="pull-left">
                                    <%-- <a href="javascript:;"><i class="icon-camera"></i></a>&nbsp;&nbsp;--%>
                                    <asp:Button ID="btnSend" runat="server" CssClass="btn btn-danger" Text="Send" ValidationGroup="AddMessageValidationGroup"
                                        OnClick="btnSend_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <%--   <div class="chat-form">
                    <div class="input-cont" style="margin-top: 18px; margin-left: 15px;">
                        <textarea rows="2" cols="130" cssclass="form-control col-lg-12 textarea" runat="server"
                            id="txtMessage" placeholder="Type a message here..."></textarea><br />
                        <asp:RequiredFieldValidator Display="Dynamic" SetFocusOnError="true" ID="MessageRequired"
                            runat="server" ControlToValidate="txtMessage" CssClass="failureNotification"
                            ForeColor="Red" ErrorMessage="Message Text is required." InitialValue="" ValidationGroup="AddMessageValidationGroup"></asp:RequiredFieldValidator>
                    </div>
                    <div class="pull-left" style="margin-top: 18px; margin-left: 15px;">
                        <a href="javascript:;">Assign To</a>
                        <asp:DropDownList ID="ddlUsers" runat="server" DataTextField="UserName" CssClass="input-sm m-bot15"
                            DataValueField="UserID">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator Display="Dynamic" SetFocusOnError="true" ID="AssigneeeRequired"
                            runat="server" ControlToValidate="ddlUsers" CssClass="failureNotification" ForeColor="Red"
                            ErrorMessage="Please select Assignee" InitialValue="0" ValidationGroup="AddMessageValidationGroup"></asp:RequiredFieldValidator>
                        <ajax:AsyncFileUpload ID="flFile" Width="100%" runat="server" CompleteBackColor="Green"
                            UploaderStyle="Traditional" OnClientUploadError="uploadError" ErrorBackColor="Red"
                            OnUploadedComplete="Attachment_UploadedComplete" UploadingBackColor="#66CCFF"
                            OnClientUploadStarted="uploadStarted" />
                    </div>
                    <div class="form-group pull-right" style="margin-top: 10px; margin-right: 10px;">
                        <div class="pull-left">
                            <a href="javascript:;"><i class="icon-camera"></i></a>&nbsp;&nbsp;
                            <asp:Button ID="btnSend" runat="server" CssClass="btn btn-danger" Text="Send" ValidationGroup="AddMessageValidationGroup"
                                OnClick="btnSend_Click" />
                        </div>
                    </div>
                </div>--%>
            </ContentTemplate>

        </asp:UpdatePanel>

    </div>
</div>
