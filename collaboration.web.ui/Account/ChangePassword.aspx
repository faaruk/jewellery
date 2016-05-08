<%@ Page Title="Change Password" Language="C#" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs"
    Inherits="Collaboration.Web.UI.Account.ChangePassword" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="../UserControl/UC_ChangePassword.ascx" TagName="ChangePassword" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta content="" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <meta name="description" content="" />
    <meta name="author" content="Mosaddek" />
    <meta name="keyword" content="FlatLab, Dashboard, Bootstrap, Admin, Template, Theme, Responsive, Fluid, Retina" />
   
    <title>FlatLab - Flat & Responsive Bootstrap Admin Template</title>
    <!-- Bootstrap core CSS -->
    <link href="<%= ResolveUrl("~/Styles/bootstrap.min.css") %>" rel="stylesheet" />
    <link href="<%= ResolveUrl("~/Styles/bootstrap-reset.css") %>" rel="stylesheet" />
    <!--external css-->
    <link href="<%= ResolveUrl("~/Styles/font-awesome.css") %>" rel="stylesheet" />
    <%--  <link href="<%= ResolveUrl("~/Styles/owl.carousel.css") %>" type="text/css")  rel="stylesheet" />
    <link href="<%= ResolveUrl("~/Styles/jquery.gritter.css") %>" rel="stylesheet" />--%>
    <!-- Custom styles for this template -->
    <link href="<%= ResolveUrl("~/Styles/style.css") %>" rel="stylesheet" />
    <link href="<%= ResolveUrl("~/Styles/style-responsive.css") %>" rel="stylesheet" />
    <!-- HTML5 shim and Respond.js IE8 support of HTML5 tooltipss and media queries -->
    <!--[if lt IE 9]>
      <script src="~/js/html5shiv.js"></script>
      <script src="~/js/respond.min.js"></script>
    <![endif]-->
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div>
            <div id="myModalChangePassword">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <uc1:ChangePassword ID="ChangePassword1" runat="server" />
                    </div>
                </div>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
