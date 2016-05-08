<%@ Page Title="" Language="C#" MasterPageFile="~/DasbhoardMaster.Master"
    AutoEventWireup="true" CodeFile="TicketsClosed.aspx.cs" Inherits="Collaboration.Web.UI.Tickets.TicketsClosed" %>

<%@ Import Namespace="Collaboration.Web.UI" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Src="~/UserControl/UC_TicketsNew.ascx" TagName="ViewTickets" TagPrefix="uc1" %>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">

    


    <script type="text/javascript">

        $(".ajax__fileupload_dropzone").css("visibility", "hidden");

    </script>




    <div class="panel">
        <div class="bio-graph-heading">
            Closed Tickets
        </div>
        <div class="panel-body">
            <div>
                
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
                                                    <h4 class="modal-title">Ticket Details
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

                                <asp:TemplateField HeaderText="Ticket Subject" HeaderStyle-ForeColor="#797979">
                                    <ItemTemplate>
                                        <asp:Label ID="lblNoOfMessages" runat="server" Text='<%# Bind("Subject") %>'></asp:Label>
                                        <asp:HiddenField ID="hdHasUnreadMessages" runat="server" Value='<%# Bind("HasUnReadTickets") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Closed By" HeaderStyle-ForeColor="#797979">
                                    <ItemTemplate>
                                        <asp:Label ID="lblUserName" runat="server" Text='<%# Bind("LastModifiedUserName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Closed On" HeaderStyle-ForeColor="#797979">
                                    <ItemTemplate>
                                        <asp:Label ID="lblModifyDate" runat="server" Text='<%# Eval("ModifyDate", Resource.FormatDateTimeForBinders) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="AssignedTo" HeaderStyle-ForeColor="#797979" Visible="false">
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

