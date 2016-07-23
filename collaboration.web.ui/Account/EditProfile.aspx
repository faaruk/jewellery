﻿<%@ Page Title="" Language="C#" MasterPageFile="~/DasbhoardMaster.master" AutoEventWireup="true" CodeFile="EditProfile.aspx.cs" 
    Inherits="Collaboration.Web.UI.Account.EditProfile" %>
<%@ Register Src="../UserControl/UC_ChangePassword.ascx" TagName="ChangePassword" TagPrefix="uc1" %>
<%@ Register Src="../UserControl/UC_EditProfileN.ascx" TagName="EditProfile" TagPrefix="uc1" %>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div class="row">
        <div class="profile-nav col-lg-3">
            <div class="panel">
                <div class="user-heading round">
                    <a href="#">
                        <asp:Image ID="Image1" runat="server" ImageUrl="ProfileImage.aspx" />
                    </a>
                    <h1>
                        <asp:Label ID="lblUserName" runat="server"></asp:Label></h1>
                    <p>
                        <asp:Label ID="lblEmailInitial" runat="server"></asp:Label></p>
                </div>
                <div class="btn-group btn-group-justified">
                    <asp:LinkButton CssClass="btn btn-shadow btn-info" ID="btnDeletePicture" Visible="false"
                        runat="server" Text="Delete Picture" OnClick="btnDeletePicture_Click" />
                </div>
            </div>
            <asp:LinkButton PostBackUrl="~/Account/Profile.aspx" class="btn btn-success btn-lg btn-block"
                ID="btnViewProfile" runat="server" Text="View Profile" />
        </div>
        <div class="profile-info col-lg-9">
            <div class="panel panel-primary">
                <div class="bio-graph-heading">
                    Edit Profile
                </div>
                <uc1:EditProfile ID="EditProfile1" IsCurentUser="true" runat="server" />
                <%--<uc1:EditProfile ID="UCEditProfile" IsCurentUser="true" runat="server" />--%>
            </div>
            <!-- Change Password Section -->
            <div>
                <uc1:ChangePassword ID="ChangePassword1" runat="server" />
            </div>
            <!-- Change Password Section end -->
        </div>
    </div>
    <!-- page end-->
</asp:Content>
