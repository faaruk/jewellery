<%@ Page Title="" Language="C#" MasterPageFile="~/DasbhoardMaster.master" AutoEventWireup="true" 
    CodeFile="Default.aspx.cs" Inherits="Collaboration.Web.UI.Default" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
      <!--First div-->
    <div class="row">
        <div class="col-sm-6">
            <div class="panel">
                <div class="bio-graph-heading">
                    New Order <span class="badge bg-inverse">
                        <asp:Label runat="server" ID="lblInitiated"></asp:Label></span> 
                                               <a href="<%= ResolveUrl("~/Orders/ViewOrder.aspx?OrderStatusID=1") %>">
                            <span class="label label-inverse pull-right btn-xs">View All</span></a>
                </div>
                <asp:GridView Style="margin-top: 0px;" ID="gvTableNewOrders" runat="server" AutoGenerateColumns="False"
                    DataKeyNames="OrderID" AllowSorting="True" Width="100%" EmptyDataText="No Records Found"
                    CssClass="table">
                    <Columns>
                        <asp:TemplateField HeaderText="Serial Number" HeaderStyle-ForeColor="#797979">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkEdit" runat="server" CssClass="btn-info btn-xs" Text='<%# Bind("SerialNumber") %>'
                                    ToolTip="View Details" PostBackUrl='<%# String.Format("~/Orders/ViewOrderDetails.aspx?OrderID={0}", Eval("OrderID"))%>'></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Date Created" HeaderStyle-ForeColor="#797979">
                            <ItemTemplate>
                                <asp:Label ID="lblCreateDate" runat="server" Text='<%# Eval("CreateDate",Collaboration.Web.UI.Resource.FormatDateForBinders) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="CreatedBy" HeaderStyle-ForeColor="#797979">
                            <ItemTemplate>
                                <asp:Label ID="lblCreateByUserName" runat="server" Text='<%# Bind("CreateByUserName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
        <div class="col-sm-6">
            <div class="panel" id="divTmNotAssigned" runat="server" visible="false">
                <div class="bio-graph-heading" style="background: #ff6c60;">
                    New Order - TM not assigned <span class="badge bg-inverse">
                        <asp:Label runat="server" ID="lblTMNotAssigned"></asp:Label></span><a href="<%= ResolveUrl("~/Orders/ViewOrder.aspx?FilterID=4") %>">
                            <span class="label label-inverse pull-right btn-xs">View All</span></a>
                </div>
                <asp:GridView Style="margin-top: 0px;" ID="gvTableTMNotAssigned" runat="server" AutoGenerateColumns="False"
                    DataKeyNames="OrderID" AllowSorting="True" Width="100%" EmptyDataText="No Records Found"
                    CssClass="table">
                    <Columns>
                        <asp:TemplateField HeaderText="Serial Number" HeaderStyle-ForeColor="#797979">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkEdit" runat="server" CssClass="btn btn-info btn-xs" Text='<%# Bind("SerialNumber") %>'
                                    ToolTip="View Details" PostBackUrl='<%# String.Format("~/Orders/ViewOrderDetails.aspx?OrderID={0}", Eval("OrderID"))%>'></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Date Created" HeaderStyle-ForeColor="#797979">
                            <ItemTemplate>
                                <asp:Label ID="lblCreateDate1" runat="server" Text='<%# Eval("CreateDate",Collaboration.Web.UI.Resource.FormatDateForBinders) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="CreatedBy" HeaderStyle-ForeColor="#797979">
                            <ItemTemplate>
                                <asp:Label ID="lblCreateByUserName1" runat="server" Text='<%# Bind("CreateByUserName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
    <!--First div End-->
    <!--Second div-->
    <div class="row state-overview">
     <div class="panel" style="margin: 15px 15px;">
       <div class="bio-graph-heading" style="background: #324a56;">
                    Orders By Status
                </div>
       <div class="panel-body">
               <div class="col-lg-3 col-sm-6">
            <a href="<%= ResolveUrl("~/Orders/ViewOrder.aspx?OrderStatusID=1") %>">
                <div class="panel">
                    <div class="symbol dark-green link_hover">
                        <i class="icon-leaf"></i>
                    </div>
                    <div class="value">
                        <h1 class=" count1">
                            <asp:Label runat="server" ID="lblOrderInitiated"></asp:Label></h1>
                        <p>
                            Order Initiated</p>
                    </div>
                </div>
            </a>
        </div>
        <div class="col-lg-3 col-sm-6">
            <a href="<%= ResolveUrl("~/Orders/ViewOrder.aspx?OrderStatusID=2") %>">
                <div class="panel">
                    <div class="symbol red link_hover">
                        <i class="icon-shield"></i>
                    </div>
                    <div class="value">
                        <h1 class=" count2">
                            <asp:Label runat="server" ID="lblCADInProgress"></asp:Label></h1>
                        <p>
                            CAD In Progress</p>
                    </div>
                </div>
            </a>
        </div>
        <div class="col-lg-3 col-sm-6">
            <a href="<%= ResolveUrl("~/Orders/ViewOrder.aspx?OrderStatusID=3") %>">
                <div class="panel">
                    <div class="symbol yellow link_hover">
                        <i class=" icon-eye-close"></i>
                    </div>
                    <div class="value">
                        <h1 class=" count3">
                            <asp:Label runat="server" ID="lblPendingTMReview"></asp:Label></h1>
                        <p>
                            Pending TM Review</p>
                    </div>
                </div>
            </a>
        </div>
        <div class="col-lg-3 col-sm-6">
            <a href="<%= ResolveUrl("~/Orders/ViewOrder.aspx?OrderStatusID=4") %>">
                <div class="panel">
                    <div class="symbol blue link_hover">
                        <i class="icon-compass"></i>
                    </div>
                    <div class="value">
                        <h1 class=" count4">
                            <asp:Label runat="server" ID="lblChangeRequest"></asp:Label></h1>
                        <p>
                            Change Request</p>
                    </div>
                </div>
            </a>
        </div>
        <div class="col-lg-3 col-sm-6">
            <a href="<%= ResolveUrl("~/Orders/ViewOrder.aspx?OrderStatusID=5") %>">
                <div class="panel">
                    <div class="symbol terques link_hover">
                        <i class=" icon-foursquare"></i>
                    </div>
                    <div class="value">
                        <h1 class=" count4">
                            <asp:Label runat="server" ID="lblPendingCustomerConfirmation"></asp:Label></h1>
                        <p>
                            Pending Customer Confirmation</p>
                    </div>
                </div>
            </a>
        </div>
        <div class="col-lg-3 col-sm-6">
            <a href="<%= ResolveUrl("~/Orders/ViewOrder.aspx?OrderStatusID=6") %>">
                <div class="panel">
                    <div class="symbol light-blue link_hover">
                        <i class=" icon-legal"></i>
                    </div>
                    <div class="value">
                        <h1 class=" count4">
                            <asp:Label runat="server" ID="lblCADConfirmed"></asp:Label></h1>
                        <p>
                            CAD Confirmed</p>
                    </div>
                </div>
            </a>
        </div>
        <div class="col-lg-3 col-sm-6">
            <a href="<%= ResolveUrl("~/Orders/ViewOrder.aspx?OrderStatusID=7") %>">
                <div class="panel">
                    <div class="symbol green link_hover">
                        <i class=" icon-inbox"></i>
                    </div>
                    <div class="value">
                        <h1 class=" count4">
                            <asp:Label runat="server" ID="lblPrototypingBegins"></asp:Label></h1>
                        <p>
                            Prototyping Begins</p>
                    </div>
                </div>
            </a>
        </div>
        <div class="col-lg-3 col-sm-6">
            <a href="<%= ResolveUrl("~/Orders/ViewOrder.aspx?OrderStatusID=8") %>">
                <div class="panel">
                    <div class="symbol sky link_hover">
                        <i class=" icon-signal"></i>
                    </div>
                    <div class="value">
                        <h1 class=" count4">
                            <asp:Label runat="server" ID="lblProductionInProgress"></asp:Label></h1>
                        <p>
                            Production In Progress</p>
                    </div>
                </div>
            </a>
        </div>
        <div class="col-lg-3 col-sm-6">
          
            <a href="<%= ResolveUrl("~/Orders/ViewOrder.aspx?OrderStatusID=9") %>">
          
                <div class="panel">
                    <div class="symbol lime link_hover">
                        <i class=" icon-shopping-cart"></i>
                    </div>
                    <div class="value">
                        <h1 class=" count4">
                            <asp:Label runat="server" ID="lblShipped"></asp:Label></h1>
                        <p>
                            Shipped</p>
                    </div>
                </div>
            </a>
        </div>
        </div>
        </div>
        <div class="panel" style="margin: 15px 15px;">
       <div class="bio-graph-heading" style="background: #324a56;">
                    Order Alerts
                </div>
       <div class="panel-body">
        <div class="col-lg-3 col-sm-6">
            <a href="<%= ResolveUrl("~/Orders/ViewOrder.aspx?FilterID=1") %>">
                <div class="panel">
                    <div class="symbol orange link_hover">
                        <i class=" icon-beaker"></i>
                    </div>
                    <div class="value">
                        <h1 class=" count4">
                            <asp:Label runat="server" ID="lblDelayed"></asp:Label></h1>
                        <p>
                            Delayed</p>
                    </div>
                </div>
            </a>
        </div>
        <div class="col-lg-3 col-sm-6">
            <a href="<%= ResolveUrl("~/Orders/ViewOrder.aspx?FilterID=2") %>">
                <div class="panel">
                    <div class="symbol dark link_hover">
                        <i class="icon-retweet"></i>
                    </div>
                    <div class="value">
                        <h1 class=" count4">
                            <asp:Label runat="server" ID="lblSampleNotReturned"></asp:Label></h1>
                        <p>
                            Sample Not Returned</p>
                    </div>
                </div>
            </a>
        </div>
        <div class="col-lg-3 col-sm-6">
            <a href="<%= ResolveUrl("~/Orders/ViewOrder.aspx?FilterID=3") %>">
                <div class="panel">
                    <div class="symbol pink link_hover">
                        <i class="icon-sort-by-attributes-alt"></i>
                    </div>
                    <div class="value">
                        <h1 class=" count4">
                            <asp:Label runat="server" ID="lblOrdersApproachingDeadline"></asp:Label></h1>
                        <p>
                            Orders approaching deadline</p>
                    </div>
                </div>
            </a>
        </div>
        <div class="col-lg-3 col-sm-6">
            <a href="<%= ResolveUrl("~/Orders/ViewOrder.aspx?FilterID=8") %>">
                <div class="panel">
                    <div class="symbol orange link_hover">
                        <i class=" icon-beaker"></i>
                    </div>
                    <div class="value">
                        <h1 class=" count4">
                            <asp:Label runat="server" ID="lblDelayedShipping"></asp:Label></h1>
                        <p>
                            Delayed Shipping</p>
                    </div>
                </div>
            </a>
        </div>
                 <div class="col-lg-3 col-sm-6">
            <a href="<%= ResolveUrl("~/Orders/ViewOrder.aspx?FilterID=9") %>">
                <div class="panel">
                    <div class="symbol pink link_hover">
                        <i class="icon-sort-by-attributes-alt"></i>
                    </div>
                    <div class="value">
                        <h1 class=" count4">
                            <asp:Label runat="server" ID="lblOrdersApproachingShipping"></asp:Label></h1>
                        <p>
                            Orders approaching shipping</p>
                    </div>
                </div>
            </a>
        </div>

         </div>
        </div>
    </div>
    <!--Second div End-->
    <div class="clear">
    </div>
    <!--Third div-->
    <div class="row">
        <div class="col-lg-6">
            <div class="panel">
                <div class="bio-graph-heading" style="background: #324a56;">
                    Orders Approaching Deadline
                </div>
                <div class="panel-body">
                    <asp:Chart ID="chartDeadline" runat="server" Width="500px" Height="350px" ToolTip="Orders Approaching Deadline"
                        BorderlineColor="Gray" Palette="Bright">
                        <Series>
                        </Series>
                        <Legends>
                            <asp:Legend Name="Orders Approaching Deadline" Docking="Bottom" Title="Priorities"
                                TableStyle="Wide" BorderDashStyle="DashDotDot" BorderColor="#e8eaf1" TitleSeparator="Line"
                                TitleFont="TimesNewRoman" TitleSeparatorColor="#e8eaf1">
                            </asp:Legend>
                            <%--Legends denotes the representing color for each brands--%>
                            <%--It will automatically takes the names from the series names and it's associated colors or you can give legend text in series--%>
                        </Legends>
                        <ChartAreas>
                            <asp:ChartArea Name="ChartArea1">
                                <AxisX>
                                    <MajorGrid Enabled="false" />
                                </AxisX>
                            </asp:ChartArea>
                        </ChartAreas>
                    </asp:Chart>
                </div>
            </div>
        </div>
        <div class="col-lg-6">
            <div class="panel">
                <div class="bio-graph-heading" style="background: #324a56;">
                    Order By Status
                </div>
                <div class="panel-body">
                    <asp:Chart ID="chartStatus" runat="server" Width="500px" Height="350px" ToolTip="Orders Approaching Deadline"
                        BorderlineColor="Gray" Palette="Bright">
                        <Series>
                            <asp:Series LabelForeColor="White" Name="Default" ChartType="Pie" />
                        </Series>
                        <Legends>
                            <asp:Legend Name="Orders Status" Docking="Bottom" Title="Order Status" LegendStyle="Table"
                                TableStyle="Wide" BorderDashStyle="DashDotDot" BorderColor="#e8eaf1" TitleSeparator="Line"
                                TitleFont="TimesNewRoman" TitleSeparatorColor="#e8eaf1">
                            </asp:Legend>
                            <%--Legends denotes the representing color for each brands--%>
                            <%--It will automatically takes the names from the series names and it's associated colors or you can give legend text in series--%>
                        </Legends>
                        <ChartAreas>
                            <asp:ChartArea Name="ChartArea1" Area3DStyle-Enable3D="true">
                                <AxisX>
                                    <MajorGrid Enabled="false" />
                                </AxisX>
                            </asp:ChartArea>
                        </ChartAreas>
                    </asp:Chart>
                </div>
            </div>
        </div>
        <!--Third div End-->
        <div class="clear">
        </div>
        <!--script for this page-->
      <%--  <script src="../js/sparkline-chart.js"></script>
        <script src="../js/easy-pie-chart.js"></script>
        <script src="../js/count.js"></script>
        <script src="../assets/flot/jquery.flot.js"></script>
        <script src="../assets/flot/jquery.flot.resize.js"></script>
        <script src="../assets/flot/jquery.flot.pie.js"></script>
        <script src="../assets/flot/jquery.flot.stack.js"></script>
        <script src="../assets/flot/jquery.flot.crosshair.js"></script>--%>
        <!--common script for all pages-->
        <script src="js/common-scripts.js" type="text/javascript"></script>
        <!--script for this page only-->
       <%-- <script src="../js/flot-chart.js"></script>--%>
        
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ScriptsPlaceHolder" runat="server">
<script type="text/javascript">

    ////owl carousel

    //jQuery(document).ready(function () {
    //    jQuery("#owl-demo").owlCarousel({
    //        navigation: true,
    //        slideSpeed: 300,
    //        paginationSpeed: 400,
    //        singleItem: true,
    //        autoPlay: true

    //    });
    //});

    ////custom select box

    //jQuery(function () {
    //    $('select.styled').customSelect();
    //});

        </script>
</asp:Content>
