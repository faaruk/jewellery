<%@ Page Title="" Language="C#" MasterPageFile="~/DasbhoardMaster.master" 
    AutoEventWireup="true" CodeFile="CreateOrder.aspx.cs" Inherits="Collaboration.Web.UI.Orders.CreateOrderN" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Src="../UserControl/UC_EditOrder.ascx" TagName="EditOrder" TagPrefix="uc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">


    <!--Left Side-->
    <div>
        <div class="bio-graph-heading">
            Create Order
        </div>
        <div class="panel-body">
            <div id="dvMessage" runat="server" style="display: none" class="alert alert-block alert-danger fade in">
                <button data-dismiss="alert" class="close close-sm" type="button">
                    <i class="icon-remove"></i>
                </button>
                <asp:Literal ID="ltMessage" runat="server"></asp:Literal>
            </div>
             <div class="alert alert-block alert-danger fade in" id="divCustomerEmail" runat="server" visible="false">
                 <button data-dismiss="alert" class="close close-sm" type="button">
                        <i class="icon-remove"></i>
                    </button>
                    <strong>Alert!</strong> Please update customer E-mail otherwise CAD cannot be sent
                    to customer for Confirmation.
                </div>
            <%--        <asp:UpdatePanel ID="updGrid" runat="server" UpdateMode="Conditional">
            <ContentTemplate>--%>
            <%--            </ContentTemplate>
        </asp:UpdatePanel>--%>
        </div>
        <div style="margin-top: -20px">
            <uc1:EditOrder ID="EditOrder" runat="server" />
        </div>
        <div class="panel">
            <div class="panel-body">
                <asp:Button CssClass="btn btn-info" Text="Place Order" runat="server" CausesValidation="true"
                    ValidationGroup="CreateOrderValidation" ID="btnSave" OnClick="btnSave_Click" />
                &nbsp;
                <asp:Button CssClass="btn btn-info" Text="Place Another Order" runat="server"
                    Visible="false" ID="btnPlaceNewOrder" OnClick="btnPlaceNewOrder_Click" />&nbsp;
                <asp:Button CssClass="btn btn-info" Text="Reset" runat="server" CausesValidation="false"
                    ID="btnReset" OnClick="btnReset_Click" />
            </div>
        </div>
    </div>
</asp:Content>

