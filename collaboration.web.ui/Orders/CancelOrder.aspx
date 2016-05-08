<%@ Page Title="" Language="C#" MasterPageFile="~/DasbhoardMaster.Master" AutoEventWireup="true" CodeBehind="CancelOrder.aspx.cs" Inherits="Collaboration.Web.UI.Orders.CancelOrders" Theme="Office2010Silver" %>
<%@ Register Assembly="DevExpress.Web.v14.2, Version=14.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <div class="panel">
        <div class="bio-graph-heading">
            Orders
        </div>
        <div class="panel-body">

            <dx:ASPxGridView ID="gvTable" runat="server" AutoGenerateColumns="False" KeyFieldName="OrderID" Width="100%" DataSourceID="OrdersDataSource" >
                <Columns>
                    <dx:GridViewDataTextColumn FieldName="CustomerName" Caption="Customer">
                        </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="ModelNumber" Caption="Model Number">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataComboBoxColumn FieldName="MetalID" Caption="Metal">
                        <PropertiesComboBox DataSourceID="MetalsDataSource" TextField="MetalName" ValueField="MetalID">
                            <ClearButton Visibility="True"></ClearButton>
                        </PropertiesComboBox>
                    </dx:GridViewDataComboBoxColumn>
                    <dx:GridViewDataTextColumn FieldName="SerialNumber" Caption="Serial Number">
                    </dx:GridViewDataTextColumn>                                        
                    <dx:GridViewDataTextColumn FieldName="Status" Caption="Status">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataDateColumn FieldName="ExpectedShippingDate" Caption="Expected Shipping Date">
                    </dx:GridViewDataDateColumn>
                    <dx:GridViewDataComboBoxColumn FieldName="PriorityID" Caption="Priority">
                        <PropertiesComboBox DataSourceID="PrioritiesDataSource" TextField="Name" ValueField="PriorityID">
                            <ClearButton Visibility="True"></ClearButton>
                        </PropertiesComboBox>
                    </dx:GridViewDataComboBoxColumn>                    
                    <dx:GridViewDataTextColumn FieldName="CreateByUserName" Caption="Created By">
                    </dx:GridViewDataTextColumn>                                                            
                    <dx:GridViewDataColumn Width="100px">
                        <DataItemTemplate>
                            <a href="<%# ResolveUrl(string.Format("~/Orders/ViewOrderDetails.aspx?OrderID={0}", Eval("OrderID"))) %>">View Details</a>                            
                        </DataItemTemplate>
                    </dx:GridViewDataColumn>
                    <dx:GridViewCommandColumn ShowDeleteButton="true" Width="60px"></dx:GridViewCommandColumn>
                    
                    <dx:GridViewDataColumn FieldName="OrderStatusID" Visible="false">
                    </dx:GridViewDataColumn>
                </Columns>
            </dx:ASPxGridView>
            <asp:ObjectDataSource ID="OrdersDataSource" runat="server" TypeName="Collaboration.Business.Components.OrderManager" SelectMethod="GetCancelledOrders" DeleteMethod="DeleteOrder">
                <SelectParameters>
                    <asp:Parameter Name="assignedTo" Type="Int32" DefaultValue="0" />
                    <asp:Parameter Name="status" Type="Int32" DefaultValue="0" />
                    <asp:Parameter Name="UserID" Type="Int32" />
                </SelectParameters>
                <DeleteParameters>
                    <asp:Parameter Name="OrderID" Type="Int32" />
                </DeleteParameters>
            </asp:ObjectDataSource>         
            
            <asp:ObjectDataSource ID="MetalsDataSource" runat="server" TypeName="Collaboration.Business.Components.AdminManager" SelectMethod="GetMetals" CacheDuration="60">
            </asp:ObjectDataSource>

            <asp:ObjectDataSource ID="PrioritiesDataSource" runat="server" TypeName="Collaboration.Business.Components.AdminManager" SelectMethod="GetPriorities" CacheDuration="60">
            </asp:ObjectDataSource>
        </div>
    </div>
    <!-- page end-->
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptsPlaceHolder" runat="server">
</asp:Content>
