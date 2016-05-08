<%@ Page Title="Log In" Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs"
    Inherits="Collaboration.Web.UI.Account.Login" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html>
<html lang="en">
<head>
    <meta content="" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <meta name="description" content="" />
    <meta name="author" content="Mosaddek" />
    <meta name="keyword" content="FlatLab, Dashboard, Bootstrap, Admin, Template, Theme, Responsive, Fluid, Retina" />
    <title>River Mount - Collaboration System - Login</title>
    <!-- Bootstrap core CSS -->
    <link href="../Styles/bootstrap.min.css" rel="stylesheet" />
    <link href="../Styles/bootstrap-reset.css" rel="stylesheet" />
    <!--external css-->
    <link href="../Styles/font-awesome.css" rel="stylesheet" />
    <!-- Custom styles for this template -->
    <link href="../Styles/style.css" rel="stylesheet" />
    <link href="../Styles/style-responsive.css" rel="stylesheet" />
    <!-- HTML5 shim and Respond.js IE8 support of HTML5 tooltipss and media queries -->
    <!--[if lt IE 9]>
    <script src="js/html5shiv.js"></script>
    <script src="js/respond.min.js"></script>
    <![endif]-->
</head>
<body class="login-body">
    <div class="container">
        <form class="form-signin" runat="server">
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </asp:ToolkitScriptManager>

        <div class="re_logo">
       <img src="<%=ResolveUrl("~/img/logo_river.png") %>" alt="River Mounts">
       </div>

        <h2 class="form-signin-heading">
            sign in now</h2>
        <div id="dvLoginError" runat="server" style="display: none" class="alert alert-block alert-danger fade in">
            <asp:Literal ID="ltLogin" runat="server"></asp:Literal>
        </div>
        <div class="login-wrap">
            <asp:TextBox CssClass="form-control" ID="txtUserName" runat="server" placeholder="Username"></asp:TextBox>
            <asp:RequiredFieldValidator Display="Dynamic" ID="UserNameRequired" runat="server"
                ControlToValidate="txtUserName" CssClass="failureNotification" ErrorMessage="User Name is required."
                ToolTip="User Name is required." ValidationGroup="LoginUserValidationGroup" ForeColor="Red"></asp:RequiredFieldValidator>            
            <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password"  placeholder="Password"></asp:TextBox>
            <asp:RequiredFieldValidator Display="Dynamic" ID="PasswordRequired" runat="server"
                ControlToValidate="txtPassword" CssClass="failureNotification" ErrorMessage="Password is required."
                ForeColor="Red" ToolTip="Password is required." ValidationGroup="LoginUserValidationGroup"></asp:RequiredFieldValidator>
            <label class="checkbox">
            <asp:CheckBox id="chkRememberMe" runat="server" value="remember-me" Visible="true" Text= "Remember me"/>
                <span class="pull-right">
                    <asp:LinkButton runat="server" ID="lnkForgotPassword" Text="Forgot Password" OnClick="btnForgotPassword_Click"></asp:LinkButton>
                </span>
            </label>
            <asp:Button class="btn btn-lg btn-login btn-block" ID="LoginButton" runat="server"
                CommandName="Login" Text="SIgn In" ValidationGroup="LoginUserValidationGroup"
                OnClick="LoginButton_Click" />
        </div>
        <!-- ModalPopupExtender for Forgot Password -->
        <div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <ajaxToolkit:ModalPopupExtender ID="mpForgotPassword" BackgroundCssClass="modalBackground"
                        runat="server" TargetControlID="ldlDummy" PopupControlID="pnl" Y="100" CancelControlID="btnCancel">
                    </ajaxToolkit:ModalPopupExtender>
                    <asp:Panel Style="display: none;" ID="pnl" runat="server" BorderColor="ActiveBorder"
                        BorderStyle="Solid" BorderWidth="0px">
                        <div class="modal-dialog" style="width:450px">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true" onclick="$('#btnCancel').click()">
                                        &times;</button>
                                    <h4 class="modal-title">
                                        Forgot Password ?</h4>
                                </div>                                
                                <div id="dvResetPasswordInfo" runat="server" style="display: none" class="alert-block alert-danger fade in">
                                    <asp:Literal ID="ltInfoResetPassword" runat="server"></asp:Literal>
                                </div>                                
                                <div class="modal-body">                                  
                                    <asp:PlaceHolder ID="UsernameTextBoxPlaceHolder" runat="server">
                                        Enter your UserName to reset the password.
                                    <asp:TextBox CssClass="form-control placeholder-no-fix" ID="txtResetUserName" runat="server"
                                        AutoCompleteType="Disabled"></asp:TextBox>                                   
                                    <asp:RequiredFieldValidator ID="ResetUserNameRequired" runat="server" ControlToValidate="txtResetUserName"
                                        CssClass="failureNotification" ForeColor="Red" Display="Dynamic" ErrorMessage="User Name is required."
                                        ToolTip="User Name is required." InitialValue="" ValidationGroup="ResetLoginUserValidationGroup"></asp:RequiredFieldValidator>
                                    </asp:PlaceHolder>
                                </div>                                
                                <div class="modal-footer">                                    
                                    <asp:Button Text="Submit" ID="btnResetPassword" ValidationGroup="ResetLoginUserValidationGroup"
                                        runat="server" OnClick="btnResetPassword_Click" CssClass="btn btn-success" UseSubmitBehavior="true" />
                                    <asp:Button data-dismiss="modal" runat="server" ID="btnCancel" Text="Cancel" CssClass="btn btn-default">
                                    </asp:Button>                                    
                                </div>
                            </div>
                        </div>
                        <asp:Label Text="" ID="ldlDummy" runat="server"></asp:Label>
                    </asp:Panel>
                </ContentTemplate>
                <Triggers>
                <asp:AsyncPostBackTrigger ControlID="lnkForgotPassword" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
        </form>
    </div>
    <!-- js placed at the end of the document so the pages load faster -->
    <script src="../js/jquery.js" type="text/javascript"></script>
    <script src="../js/bootstrap.min.js" type="text/javascript"></script>
</body>
</html>
