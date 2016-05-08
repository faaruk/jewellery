<%@ Page Title="Tickets" Language="C#" MasterPageFile="~/DasbhoardMaster.Master"
    AutoEventWireup="true" CodeBehind="Tickets.aspx.cs" Inherits="Collaboration.Web.UI.Tickets.Tickets" %>
<%@ Import Namespace="Collaboration.Web.UI" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Src="~/UserControl/UC_Tickets.ascx" TagName="ViewTickets" TagPrefix="uc1" %>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">

   <%-- <script type="text/javascript">

        function uploadError(sender, args) {
            var errmsg = args.get_errorMessage();
            if (errmsg == "")
                $get("<%=dvTicket.ClientID%>").innerHTML = "There was some error in uploading the file";
            else
                $get("<%=dvTicket.ClientID%>").innerHTML = "Error : " + errmsg;

            $get("<%=dvTicket.ClientID%>").style.display = "block";
        }

        function uploadStarted(sender, args) {
            $get("<%=dvTicket.ClientID%>").innerHTML = "";
            $get("<%=dvTicket.ClientID%>").style.display = "none";

            var fileName = args.get_fileName();
            // var fileExt = fileName.substring(fileName.lastIndexOf(".") + 1).toLowerCase();
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
    </script>--%>


    <script type="text/javascript">

        $(".ajax__fileupload_dropzone").css("visibility", "hidden");

        function uploadErrorNew(sender, args) {
            var errmsg = args.get_errorMessage();
            if (errmsg == "")
                $get("<%=dvTicket.ClientID%>").innerHTML = "There was some error in uploading the file";
            else
                $get("<%=dvTicket.ClientID%>").innerHTML = "Error : " + errmsg;

            $get("<%=dvTicket.ClientID%>").style.display = "block";
        }

        function uploadStarted(sender, args) {

            $get("<%=dvTicket.ClientID%>").innerHTML = "";
            $get("<%=dvTicket.ClientID%>").style.display = "none";

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




    <div class="panel">
        <div class="bio-graph-heading">
            Tickets
        </div>
        <div class="panel-body">
            <div>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>                        
                        <asp:Button ID="btnShowPopup" runat="server" Text="Create Ticket" CssClass="btn btn-info btn-xs" />
                        <ajax:ModalPopupExtender ID="mpCreateNewTicket" runat="server" TargetControlID="btnShowPopup"
                            PopupControlID="pnlpopup" CancelControlID="btnCancel" BackgroundCssClass="modalBackground"
                            Y="1" BehaviorID="mpCreateNewTicket">
                        </ajax:ModalPopupExtender>
                        <div id="dvTicket" runat="server" style="display: none" class="alert alert-block alert-danger fade in">
                            <button data-dismiss="alert" class="close close-sm" type="button">
                                <i class="icon-remove"></i>
                            </button>
                            <asp:Literal ID="ltMessage" runat="server"></asp:Literal>
                        </div>
                        <asp:Panel ID="pnlpopup" runat="server" Height="600px" Width="500px" Style="display: none">
                            <div class="modal-dialog" style="width: 600px;">
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <button runat="server" id="imgCancel" type="button" class="close" data-dismiss="modal"
                                            aria-hidden="true" onclick="$find('mpCreateNewTicket').hide()">
                                            &times;</button>
                                        <h4 class="modal-title">
                                            Create New Ticket
                                        </h4>
                                    </div>
                                    <div id="dvMessage" runat="server" style="display: none" class="alert alert-block alert-danger fade in">
                                        <button data-dismiss="alert" class="close close-sm" type="button">
                                            <i class="icon-remove"></i>
                                        </button>
                                        <asp:Literal ID="ltTicketMessage" runat="server"></asp:Literal>
                                    </div>
                                    <div class="panel-body bio-graph-info form-horizontal">
                                        <div class="form-group">
                                            <label class="col-lg-4 control-label">
                                                Ticket Subject</label>
                                            <div class="col-lg-6">
                                                <asp:TextBox ID="txtTicketSubject" runat="server" CssClass="form-control" MaxLength="100"></asp:TextBox>
                                                <asp:RequiredFieldValidator Display="Dynamic" SetFocusOnError="true" ID="txtTicketSubjectRequired"
                                                    runat="server" ControlToValidate="txtTicketSubject" CssClass="failureNotification"
                                                    ForeColor="Red" ErrorMessage="Subject is required." InitialValue="" ValidationGroup="CreateTicketValidationGroup"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-lg-4 control-label">
                                                Description</label>
                                            <div class="col-lg-6">
                                               <%-- <asp:TextBox ID="txtDescription" runat="server" CssClass="form-control" MaxLength="500"
                                                    TextMode="MultiLine" Columns="10" Rows="3"></asp:TextBox>--%>
                                                     <textarea class="form-control" cols="10" rows="5" id="txtDescription" runat="server"></textarea>
                                                     <%--<textarea rows="4" cols="108" cssclass="form-control col-lg-12 textarea" runat="server"
                                    id="txtMessage" placeholder="Type a message here..."></textarea>--%>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <a class="col-lg-4 control-label" href="javascript:;">Assign Ticket To</a>
                                            <div class="col-lg-6">
                                                <asp:DropDownList ID="ddlUsers" runat="server" AutoPostBack="false" DataTextField="UserName"
                                                    CssClass="input-sm m-bot15" DataValueField="UserID">
                                                </asp:DropDownList>
                                            </div>
                                            <asp:RequiredFieldValidator Display="Dynamic" SetFocusOnError="true" ID="RequiredFieldValidator2"
                                                runat="server" ControlToValidate="ddlUsers" CssClass="failureNotification" ForeColor="Red"
                                                ErrorMessage="Please select Assignee" InitialValue="0" ValidationGroup="CreateTicketValidationGroup">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-lg-4 control-label">
                                            </label>
                                            <div class="col-lg-6">
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-lg-4 control-label">
                                            </label>
                                            <div class="col-lg-6">
                                               <%-- <ajax:AsyncFileUpload ID="flFile" Width="100%" runat="server" CompleteBackColor="Green"
                                                    UploaderStyle="Traditional" OnClientUploadError="uploadError" ErrorBackColor="Red"
                                                    OnUploadedComplete="Attachment_UploadedComplete" UploadingBackColor="#66CCFF"
                                                    OnClientUploadStarted="uploadStarted" />--%>
                                                      <ajax:AjaxFileUpload ID="specimenFileUpload1" runat="server" 
                                OnUploadComplete="File_Upload1" OnClientUploadComplete="uploadCompleteNew" MaximumNumberOfFiles="1"  OnClientUploadError="uploadErrorNew" 
                                
                                 />
                                                
                                            </div>
                                        </div>
                                    </div>
                                    <div class="modal-footer" style="border-top: 1px solid #e5e5e5; 
                                        padding: 19px 20px 20px; text-align: right;">
                                        <div class="form-group">
                                            <div>
                                                <asp:Button runat="server" Text="Submit" CssClass="btn btn-primary" ID="btnSubmit" OnClick="btnSubmitTicket_Click" ValidationGroup="CreateTicketValidationGroup">
                                                </asp:Button>
                                                <asp:Button data-dismiss="modal" runat="server" ID="btnCancel" Text="Close" CssClass="btn btn-default">
                                                </asp:Button>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="adv-table editable-table ">
                <div class="clearfix">
                    <div>
                        <asp:UpdatePanel ID="updViewDetails" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <ajaxToolkit:ModalPopupExtender ID="mpViewDetails" BackgroundCssClass="modalBackground"
                                    runat="server" TargetControlID="ldlDummyViewDetails" PopupControlID="pnlViewDetails"
                                    Y="10" X="300" CancelControlID="imgCancelViewDetails">
                                </ajaxToolkit:ModalPopupExtender>
                                <asp:Panel Style="display: none;" ID="pnlViewDetails" runat="server" BorderColor="ActiveBorder"
                                    BorderStyle="Solid" BorderWidth="0px">
                                    <div id="myModal1">
                                        <div class="modal-dialog" style="width: 950px; height: 500px">
                                            <div class="modal-content">
                                                <div class="modal-header">
                                                    <button runat="server" id="imgCancelViewDetails" type="button" class="close" data-dismiss="modal"
                                                        aria-hidden="true" onserverclick="btnCancelDetails_Click">
                                                        &times;</button>
                                                    <h4 class="modal-title">
                                                        Ticket Details
                                                    </h4>
                                                </div>
                                                <uc1:ViewTickets ID="ViewTickets" runat="server" />
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>
                                <asp:Label Text="" ID="ldlDummyViewDetails" runat="server"></asp:Label>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
                <div class="space15">
                </div>
                <asp:UpdatePanel ID="updGrid" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:GridView ID="gvTable" runat="server" AutoGenerateColumns="False" DataKeyNames="TicketThreadID"
                            AllowSorting="True" Width="100%" OnRowCommand="gvTable_RowCommand" OnRowDataBound="gvTable_RowDataBound"
                            EmptyDataText="No Recods Found" CssClass="table myTable" OnPreRender="gvTable_PreRender">
                            <Columns>
                                <%--<asp:TemplateField HeaderText="Order Number" HeaderStyle-ForeColor="#797979">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSerialNumber" runat="server" Text='<%# Bind("SerialNumber") %>'></asp:Label>
                                        <asp:HiddenField ID="hdHasUnreadMessages" runat="server" Value='<%# Bind("HasUnReadMessages") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                <asp:TemplateField HeaderText="Ticket Subject" HeaderStyle-ForeColor="#797979">
                                    <ItemTemplate>
                                        <asp:Label ID="lblNoOfMessages" runat="server" Text='<%# Bind("Subject") %>'></asp:Label>
                                        <asp:HiddenField ID="hdHasUnreadMessages" runat="server" Value='<%# Bind("HasUnReadTickets") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Last Reply By" HeaderStyle-ForeColor="#797979">
                                    <ItemTemplate>
                                        <asp:Label ID="lblUserName" runat="server" Text='<%# Bind("LastModifiedUserName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Last Reply Date" HeaderStyle-ForeColor="#797979">
                                    <ItemTemplate>
                                        <asp:Label ID="lblModifyDate" runat="server" Text='<%# Eval("ModifyDate", Resource.FormatDateTimeForBinders) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="AssignedTo" HeaderStyle-ForeColor="#797979">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAssignedToUserName" runat="server" Text='<%# Bind("AssignedToUserName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkEdit" runat="server" CssClass="btn btn-info btn-xs" Text="View Details"
                                            CommandName="ViewDetails" ToolTip="View Details" CommandArgument='<%# Eval("TicketThreadID").ToString()  %>'></asp:LinkButton>
                                            &nbsp;&nbsp;&nbsp;&nbsp;
                                             <asp:LinkButton ID="lnkCloseTicket" runat="server" CssClass="btn btn-info btn-xs" Text="Close Ticket"
                                            CommandName="CloseTicket" ToolTip="Close Ticket" CommandArgument='<%# Eval("TicketThreadID").ToString()  %>' Visible="false"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <!-- page end-->
</asp:Content>
