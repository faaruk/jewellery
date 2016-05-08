<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UC_HeaderN.ascx.cs" Inherits="Collaboration.Web.UI.UserControl.UC_Header" %>

<%@ Import Namespace="System.Configuration" %>
<asp:Timer runat='server' ID='timer1' Enabled='true' Interval='5000'></asp:Timer>
<script type="text/javascript" language="javascript">

    function OnMessageClick(filterId, threadId) {

        document.getElementById("hdnMessageThredID").value = threadId;
        document.getElementById("hdnFilterID").value = filterId;

        document.getElementById("btnOpenMessage").click();

    }

</script>
<%--<script type="text/javascript" language="javascript">
    setTimeout(function () {
        window.location.reload(1);
    }, 5000);

    </script>--%>
<div class="header white-bg">
    <div class="sidebar-toggle-box">
        <div data-original-title="Toggle Navigation" data-placement="right" class="icon-reorder tooltips">
        </div>
    </div>
    <!--logo start-->
    <a href="<%= ConfigurationManager.AppSettings["RootPath"]%>/Default.aspx" class="logo">
        <img src="<%= ConfigurationManager.AppSettings["RootPath"]%>/img/logo_river.png" alt="River Mounts" /></a>
    <!--logo end-->
    <div class="nav notify-row" id="top_menu">
        <asp:HiddenField ID="hdnMessageThredID" runat="server" ClientIDMode="Static" />
        <asp:HiddenField ID="hdnFilterID" runat="server" ClientIDMode="Static"/>
        <asp:Button ID="btnOpenMessage" runat="server" style="display:none" OnClick="btnOpenMessage_Click"  ClientIDMode="Static"/>
        <!--  notification start -->
        <ul class="nav top-menu">
            <!-- settings start -->
            <li class="dropdown"><a data-toggle="dropdown" class="dropdown-toggle" href="#"><i
                class="icon-tasks"></i><span class="badge bg-success">
                    <asp:Label ID="lblMessagesAssigned" runat="server"></asp:Label></span> </a>
                <ul class="dropdown-menu extended tasks-bar">
                    <div class="notify-arrow notify-arrow-green">
                    </div>
                    <li>
                        <p class="green">
                            You have
                            <asp:Label ID="lblMessagesAssigned1" runat="server"></asp:Label>
                            Messages Assigned</p>
                    </li>
                    <asp:ListView Style="margin-top: 0px;" ID="lstMessagesAssigned" runat="server" DataKeyNames="MessageID"
                        AllowSorting="True" EmptyDataText="No Recods Found" >
                        <ItemTemplate>
                            <li><a  href="javascript:OnMessageClick(5,<%# Eval("MessageThreadID") %>)"><span class="from">&nbsp;<b> <%# Eval("MessageText").ToString().Take(15).Aggregate("", (x, y) => x + y)%> </b></span><span class="small">
                                &nbsp; <%# Eval("SentFromUserName")%> </span> <span class="small italic">&nbsp;<%# Eval("CreateDate", "{0:g}")%></span> </a>
                            </li>
                           
                        </ItemTemplate>
                        
                    </asp:ListView>                   
                    <li class="external"><a href="<%= ResolveUrl("~/Messages/Messages.aspx?FilterID=5") %>">View All</a></li>
                </ul>
            </li>
            <!-- settings end -->
            <!-- inbox dropdown start-->
            <li id="header_inbox_bar" class="dropdown"><a data-toggle="dropdown" class="dropdown-toggle"
                href="#"><i class="icon-envelope-alt"></i><span class="badge bg-important">
                    <asp:Label ID="lblUnreadMessages" runat="server"></asp:Label></span> </a>
                <ul class="dropdown-menu extended inbox">
                    <div class="notify-arrow notify-arrow-red">
                    </div>
                    <li>
                        <p class="red">
                            You have <asp:Label ID="lblUnreadMessages1" runat="server"></asp:Label> new messages</p>
                    </li>
                    <asp:ListView Style="margin-top: 0px;" ID="lstUnReadMessages" runat="server" DataKeyNames="MessageID"
                        AllowSorting="True" EmptyDataText="No Recods Found" >
                        <ItemTemplate>
                            <li><a href="javascript:OnMessageClick(5,<%# Eval("MessageThreadID") %>)"><span class="from">&nbsp;<b> <%# Eval("MessageText").ToString().Take(15).Aggregate("", (x, y) => x + y)%> </b></span><span class="small">
                                &nbsp; <%# Eval("SentFromUserName")%> </span> <span class="small italic">&nbsp;<%# Eval("CreateDate", "{0:g}")%> </span> </a>
                            </li>
                        </ItemTemplate>
                    </asp:ListView>
                    <li><a href="<%= ResolveUrl("~/Messages/Messages.aspx?FilterID=6") %>">View All</a> </li>
                </ul>
            </li>
            <!-- inbox dropdown end -->
            <!-- notification dropdown start-->
            <li id="header_notification_bar" class="dropdown"><a data-toggle="dropdown" class="dropdown-toggle"
                href="#"><i class="icon-bell-alt"></i><span class="badge bg-warning">
                    <asp:Label ID="lblOrdersAssigned" runat="server"></asp:Label></span> </a>
                <ul class="dropdown-menu extended notification">
                    <div class="notify-arrow notify-arrow-yellow">
                    </div>
                    <li>
                        <p class="yellow">
                            You have <asp:Label ID="lblOrdersAssigned1" runat="server"></asp:Label> orders assigned</p>
                    </li>
                    <asp:ListView Style="margin-top: 0px;" ID="lstOrdersAssigned" runat="server" DataKeyNames="OrderID"
                        AllowSorting="True" EmptyDataText="No Recods Found" >
                        <ItemTemplate>
                            <li><a href='<%# String.Format("../Orders/ViewOrderDetails.aspx?OrderID={0}", Eval("OrderID"))%>'><span class="from">&nbsp;<b> <%# Eval("SerialNumber") %> </b></span><span class="small">
                                &nbsp; <%# Eval("CreateByUserName")%> </span> <span class="small italic">&nbsp; <%# Eval("CreateDate", "{0:g}")%> </span> </a>
                            </li>
                        </ItemTemplate>
                    </asp:ListView>
                    <li><a href="<%= ResolveUrl("~/Orders/ViewOrder.aspx?FilterID=5") %>">View All</a> </li>
                </ul>
            </li>
            <!-- notification dropdown end -->
             <!-- notification dropdown start-->
            <li id="header_tikets_bar" class="dropdown"><a data-toggle="dropdown" class="dropdown-toggle"
                href="#"><i class="icon-ticket"></i><span class="badge bg-warning">
                    <asp:Label ID="lblTicketsAssigned" runat="server"></asp:Label></span> </a>
                <ul class="dropdown-menu extended notification">
                    <div class="notify-arrow notify-arrow-yellow">
                    </div>
                    <li>
                        <p class="yellow">
                            You have <asp:Label ID="lblTicketsAssigned1" runat="server"></asp:Label> tickets assigned</p>
                    </li>
                    <asp:ListView Style="margin-top: 0px;" ID="lstTicketsAssigned" runat="server" DataKeyNames="TicketThreadID"
                        AllowSorting="True" EmptyDataText="No Recods Found" >
                        <ItemTemplate>
                            <li><a>
                                <span class="from">&nbsp;<b> <%# Eval("TicketText") %> </b></span><span class="small">
                                &nbsp; <%# Eval("AssignedFromUserName")%> </span> <span class="small italic">&nbsp; <%# Eval("CreateDate", "{0:g}")%> </span>
                                </a>
                            </li>
                        </ItemTemplate>
                    </asp:ListView>
                    <li><a href="<%= ResolveUrl("~/Tickets/Tickets.aspx") %>">View All</a> </li>
                </ul>
            </li>
            <!-- notification dropdown end -->
        </ul>
        <!--  notification end -->
    </div>
    <div class="top-nav ">
        <!--search & user info start-->
        <ul class="nav pull-right top-menu">
            <!-- user login dropdown start-->
            <li  class="dropdown"><a data-toggle="dropdown" class="dropdown-toggle" href="#">
                <asp:Image ID="ProfileImage" Height="30" Width="30" runat="server" ImageUrl="~/Account/ProfileImage.aspx" />
                <span class="username">
                    <asp:Label ID="lblUserName" runat="server"></asp:Label>
                </span><b class="caret"></b></a>
                <ul  class="dropdown-menu extended logout">
                    <div class="log-arrow-up">
                    </div>
                    <li><a href="<%= ResolveUrl("~/Account/Profile.aspx") %>"><i class="icon-suitcase"></i>
                        Profile</a></li>
                    <li>
                        <%--<asp:LinkButton ID="btnLogout" runat="server" CssClass="icon-key" Text="Logout" OnClick="btnLogout_Click" /></li>--%>
                        <asp:LinkButton ID="btnLogout" runat="server" Text="Logout" OnClick="btnLogout_Click" /></li>
                </ul>
            </li>
            <!-- user login dropdown end -->
        </ul>
        <!--search & user info end-->
    </div>
</div>
<!--header end-->
