<%@ Page Title="About Us" Language="C#" MasterPageFile="~/DasbhoardMaster.Master" AutoEventWireup="true"
    CodeBehind="About.aspx.cs" Inherits="Collaboration.Web.UI.About" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
<script type="text/javascript">
    function popuponclick() {
        my_window = window.open("",
       "mywindow", "status=1,width=350,height=150");

        my_window.document.write('<h1>The Popup Window</h1>');
    }

    function closepopup() {
        alert('sdsdf');
        if (false == my_window.closed) {
            my_window.close();
        }
        else {
            alert('Window already closed!');
        }
    }
</script>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1//EN" "http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xml:lang="en">
<head>
    <title>Designer Test</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <script type="text/javascript" src="js/pixlr.js"></script>
    <script type="text/javascript">
        pixlr.settings.target = 'http://localhost/RM/Account/Login.aspx';
        pixlr.settings.exit = 'window.close();';
        pixlr.settings.method = 'GET';
        pixlr.settings.redirect = false;
    </script>
</head>
<body>
<h4>Click the image to edit</h4>
<br />
<b>Open image editor overlay</b><br />
<a href="javascript:pixlr.overlay.show({image:'http://seooffer.w18.wh-2.com/RiverMount/img/ProfileImage.jpg', title:'Roof Crafters', service:'express'});"><img src="img/avatar1_small.jpg" width="250" height="150" title="Edit in pixlr" /></a><br /><br />
<br /><br />
</body>
</html>
</asp:Content>
