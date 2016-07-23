<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UC_TrackSHistory.ascx.cs"
    Inherits="Collaboration.Web.UI.UserControl.UC_TrackSHistory" %>

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
                            No history for this sample. 
                        </div>
                    </asp:PlaceHolder>
                    
                    <asp:GridView ID="gvTable" runat="server" AutoGenerateColumns="False" DataKeyNames="SampleTrackHistoryID"
                            AllowSorting="True" Width="100%" 
                            EmptyDataText="No Recods Found"
                            CssClass="table table-striped table-hover table-bordered dataTable myTable">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                
                                <asp:TemplateField HeaderText="Create Date" HeaderStyle-ForeColor="#797979">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCreateDate" runat="server" Text='<%# Eval("CreateDate") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Sample Status" HeaderStyle-ForeColor="#797979">
                                    <ItemTemplate>
                                        <asp:Label Visible="true" runat="server" ID="lblSampleStatusName" Text='<%# Eval("SampleStatusName")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="User Name" HeaderStyle-ForeColor="#797979">
                                    <ItemTemplate>
                                        <asp:Label Visible="true" runat="server" ID="lblUserName" Text='<%# Eval("UserName")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Name" HeaderStyle-ForeColor="#797979">
                                    <ItemTemplate>
                                        <asp:Label Visible="true" runat="server" ID="Label1" Text='<%# Eval("FLName")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="White" Font-Bold="True" ForeColor="Black" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#EFF3FB" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        </asp:GridView>
                </div>

            </ContentTemplate>

        </asp:UpdatePanel>

    </div>
</div>
