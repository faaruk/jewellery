<%@ Page Title="Profile" Language="C#" MasterPageFile="~/DasbhoardMaster.Master"
    AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="Collaboration.Web.UI.Account.Profile" %>

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
            </div>
            <asp:LinkButton PostBackUrl="~/Account/EditProfile.aspx" class="btn btn-success btn-lg btn-block"
                ID="btnViewProfile" runat="server" Text="Edit Profile" />
        </div>
        <div class="profile-info col-lg-9">
            <div class="panel">
                <div class="bio-graph-heading">
                    Profile Info
                </div>
                <div class="panel-body bio-graph-info">
                    <div class="row">
                        <div  class="form-group">
                            <label class="col-lg-4 control-label">
                                First Name</label>
                            <asp:Label ID="lblFirstName" runat="server"></asp:Label>
                        </div>
                        <div  class="form-group">
                            <label class="col-lg-4 control-label">
                                Last Name</label>
                            <asp:Label ID="lblLastName" runat="server"></asp:Label>
                        </div>
                        <div  class="form-group">
                            <label class="col-lg-4 control-label">
                                Email</label>
                            <asp:Label ID="lblEmail" runat="server"></asp:Label>
                        </div>
                        <div  class="form-group">
                            <label class="col-lg-4 control-label">
                                Mobile</label>
                            <asp:Label ID="lblMobile" runat="server"></asp:Label>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- page end-->
</asp:Content>
