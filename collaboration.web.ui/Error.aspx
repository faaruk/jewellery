<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="Collaboration.Web.UI.Error" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
 <title>Error</title>


    <!-- Bootstrap core CSS -->
    <link href="~/Styles//bootstrap.min.css" rel="stylesheet" />
    <link href="~/Styles//bootstrap-reset.css" rel="stylesheet" />
    <!--external css-->
    <link href="~/Styles///font-awesome.css" rel="stylesheet" />
    <!-- Custom styles for this template -->
    <link href="~/Styles//style.css" rel="stylesheet" />
    <link href="~/Styles//style-responsive.css" rel="stylesheet" />

    <!-- HTML5 shim and Respond.js IE8 support of HTML5 tooltipss and media queries -->
    <!--[if lt IE 9]>
    <script src="js/html5shiv.js"></script>
    <script src="js/respond.min.js"></script>
    <![endif]-->
</head>


  <body class="body-500">
  <form id="Form1" class="form-Master" runat="server"> 
    <div class="container">

      <div class="error-wrapper">
          <i class="icon-500"></i>
          <h1>Ouch!</h1>
          <h2>500 Page Error</h2>
          <asp:label id="lblError" runat="server"></asp:label>
          <p class="page-500">Looks like Something went wrong. <a href="Default.aspx">Return Home</a></p>
      </div>

    </div>
    </form>
    </body>

 
</html>
