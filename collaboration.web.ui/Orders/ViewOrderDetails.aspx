<%@ Page Title="" Language="C#" MasterPageFile="~/DasbhoardMaster.master" AutoEventWireup="true"
    CodeFile="ViewOrderDetails.aspx.cs" Inherits="Collaboration.Web.UI.Orders.ViewOrderDetails" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Src="../UserControl/UC_EditOrder.ascx" TagName="EditOrder" TagPrefix="uc1" %>
<%@ Register Src="../UserControl/UC_ViewOrderDetails.ascx" TagName="ViewOrderDetails"
    TagPrefix="uc1" %>
<%@ Register Src="../UserControl/UC_DynamicOrderDetails.ascx" TagName="DynamicOrderDetails"
    TagPrefix="uc1" %>
<%@ Register Src="../UserControl/UC_Messages.ascx" TagName="ViewMessages" TagPrefix="uc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        function SaveMessage() {
            alert('CAD has been emailed to Customer for review.');
            SetScrollEvent();
            //document.getElementById('" + this.txtCustomer.ClientID + "').focus();
        }
    </script>

    <script type="text/javascript">
        function SetScrollEvent() {
            window.scrollTo(0, 0);
        }
    </script>

    <!--Left Side-->
    <div>

        <div class="bio-graph-heading" runat="server" id="divHeading">
            Create Order    
        </div>

        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="panel-body">
                    <div id="dvMessage" runat="server" style="display: none" class="alert alert-block alert-danger fade in">
                        <button data-dismiss="alert" class="close close-sm" type="button">
                            <i class="icon-remove"></i>
                        </button>
                        <asp:Literal ID="ltMessage" runat="server"></asp:Literal>
                    </div>
                    <%--        <asp:UpdatePanel ID="updGrid" runat="server" UpdateMode="Conditional">
            <ContentTemplate>--%>
                    <%--            </ContentTemplate>
        </asp:UpdatePanel>--%>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

        <div style="margin-top: -20px">
            <uc1:EditOrder ID="EditOrder" Visible="false" runat="server" />
            <uc1:ViewOrderDetails ID="ViewOrderDetail" Visible="false" runat="server" />
            <uc1:DynamicOrderDetails ID="DynamicOrderDetails" Visible="true" runat="server" />
        </div>
        <div class="panel">
            <div class="panel-body">
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
                                            <div class="modal-dialog" style="width: 1000px; height: 550px">
                                                <div class="modal-content">
                                                    <div class="modal-header">
                                                        <button runat="server" id="imgCancelViewDetails" type="button" class="close" data-dismiss="modal"
                                                            aria-hidden="true" onserverclick="btnCancelDetails_Click">
                                                            &times;</button>
                                                        <h4 class="modal-title">Message Details
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
        <div class="panel">
            <div class="panel-body">
                <asp:UpdatePanel ID="upnlActionButtons" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:Button CssClass="btn btn-info" Text="Update Order" runat="server" CausesValidation="true"
                            ValidationGroup="CreateOrderValidation" ID="btnSave" OnClick="btnSave_Click" />
                        <asp:Button CssClass="btn btn-info" Text="Back" runat="server" ID="btnBack" PostBackUrl="~/Orders/ViewOrder.aspx" />

                        <asp:Button CssClass="btn btn-info" Text="View Message" runat="server" ID="btnShowPopup" OnClick="btnShowPopup_Click" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        $('.modal-body').bind('mousewheel DOMMouseScroll', function (e) {
            var e0 = e.originalEvent,
                delta = e0.wheelDelta || -e0.detail;

            this.scrollTop += (delta < 0 ? 1 : -1) * 30;
            e.preventDefault();
        });


    </script>
</asp:Content>

