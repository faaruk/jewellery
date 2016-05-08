<%@ Page Title="" Language="C#" MasterPageFile="~/DasbhoardMaster.master" AutoEventWireup="true" CodeFile="ReturnedSamples.aspx.cs" Inherits="Collaboration.Web.UI.Tracking.ReturnedSamples" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Src="~/UserControl/UC_TrackSHistory.ascx" TagName="ViewMessages" TagPrefix="uc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="panel">
        <div class="bio-graph-heading">
            Returned Samples
        </div>
        <div class="panel-body">
            <div class="adv-table editable-table ">

                <div class="space15">
                </div>
                <asp:UpdatePanel ID="updGrid" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:GridView ID="gvTable" runat="server" AutoGenerateColumns="False" DataKeyNames="SampleTrackID"
                            AllowSorting="True" Width="100%" OnPageIndexChanging="gvTable_PageIndexChanging" OnRowCommand="gvTable_RowCommand"
                            OnSorting="gvTable_Sorting" EmptyDataText="No Recods Found"
                            CssClass="table table-striped table-hover table-bordered dataTable myTable"
                            OnPreRender="gvTable_PreRender">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:TemplateField HeaderText="Sample Serial Number" HeaderStyle-ForeColor="#797979">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSampleSerialNumber" runat="server" Text='<%# Eval("SampleSerialNumber") %>'></asp:Label>

                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Sample Status" HeaderStyle-ForeColor="#797979">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSampleStatusName" runat="server" Text='<%# Eval("SampleStatusName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkEdit" runat="server" CssClass="btn btn-info btn-xs" Text="View History"
                                            CommandName="ViewDetails" ToolTip="View Details" CommandArgument='<%# Eval("SampleID").ToString()+","+ Eval("SampleTrackID").ToString() +","+ Eval("SampleSerialNumber").ToString() %>'></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="White" Font-Bold="True" ForeColor="Black" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#EFF3FB" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        </asp:GridView>
                    </ContentTemplate>
                </asp:UpdatePanel>
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
                                                    <h4 class="modal-title">Sample Status History: 
                                                        <asp:Label Text="" ID="Label1" runat="server"></asp:Label>
                                                    </h4>
                                                </div>
                                                <uc1:ViewMessages ID="ViewMessages" runat="server" />
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>
                                <asp:Label Text="" ID="ldlDummyViewDetails" runat="server"></asp:Label>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>



        </div>

    </div>
</asp:Content>

