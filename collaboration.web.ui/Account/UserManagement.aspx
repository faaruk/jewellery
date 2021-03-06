﻿<%@ Page Title="" Language="C#" MasterPageFile="~/DasbhoardMaster.master" AutoEventWireup="true" 
    CodeFile="UserManagement.aspx.cs"  Inherits="Collaboration.Web.UI.Account.UserManagementN" %>

<%@ Register Src="~/UserControl/UC_EditProfileN.ascx" TagName="EditProfile" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <!-- page start-->
    <div class="panel">
        <div class="bio-graph-heading">
            Users
        </div>
        <div class="panel-body">
            <div class="adv-table editable-table">
                <div class="clearfix">
                    <div>
                        <asp:UpdatePanel ID="updShowUpdate" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <ajaxToolkit:ModalPopupExtender ID="mpShowUpdate" BackgroundCssClass="modalBackground"
                                    runat="server" TargetControlID="ldlDummy" PopupControlID="pnl" Y="20" CancelControlID="imgCancel">
                                </ajaxToolkit:ModalPopupExtender>
                                <asp:Panel Style="display: none;" ID="pnl" runat="server" BorderColor="ActiveBorder"
                                    BorderStyle="Solid" BorderWidth="0px">
                                    <div id="myModal">
                                        <div class="modal-dialog">
                                            <div class="modal-content" style="width: 550px;">
                                                <div class="modal-header">
                                                    <button runat="server" id="imgCancel" type="button" class="close" data-dismiss="modal"
                                                        aria-hidden="true">
                                                        &times;</button>
                                                    <h4 class="modal-title">
                                                        Add/Edit Profile
                                                    </h4>
                                                </div>
                                                <uc1:EditProfile ID="EditProfile1" runat="server" />
                                                <%--<uc1:EditProfile ID="EditProfile" runat="server" />--%>
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>
                                <div class="btn-group">
                                    <asp:Button OnClick="btnAddNew_Click" class="btn btn-primary" Text="Add New" runat="server"
                                        ID="btnAddNew"></asp:Button>
                                    <asp:Label Text="" ID="ldlDummy" runat="server"></asp:Label>
                                </div>
                                <ajaxToolkit:ModalPopupExtender ID="mpAckDelete" BackgroundCssClass="modalBackground"
                                    runat="server" TargetControlID="ldlAckDummy" PopupControlID="pnlAck" Y="100"
                                    BehaviorID="mdlAckDelete" CancelControlID="btnConfCancel">
                                </ajaxToolkit:ModalPopupExtender>
                                <asp:Panel ID="pnlAck" runat="server" Height="100px" Width="400px" Style="display: none">
                                    <div class="modal-dialog">
                                        <div class="modal-content">
                                            <div class="modal-header">
                                                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                                                    &times;</button>
                                                <h4 class="modal-title">
                                                    Confirm
                                                </h4>
                                            </div>
                                            <div class="modal-body">
                                                <asp:Label ID="lblMessage" runat="server"></asp:Label>
                                            </div>
                                            <div class="modal-footer">
                                                <asp:Button ClientIDMode="Static" data-dismiss="modal" runat="server" ID="btnConfCancel"
                                                    Text="Ok" CssClass="btn btn-default"></asp:Button>
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>
                                <asp:Label Text="" ID="ldlAckDummy" runat="server"></asp:Label>
                                <ajaxToolkit:ModalPopupExtender ID="mpProfileImage" BackgroundCssClass="modalBackground"
                                    runat="server" TargetControlID="ldlAckDummyPict" PopupControlID="pnlProfileImage"
                                    Y="100" CancelControlID="btnProfileCancel">
                                </ajaxToolkit:ModalPopupExtender>
                                <asp:Panel ID="pnlProfileImage" runat="server" Style="display: none">
                                    <div class="modal-dialog" style="width: 300px; height: 350px;">
                                        <div class="modal-content">
                                            <div class="modal-header">
                                                <button type="button" class="close" data-dismiss="modal" aria-hidden="true" onclick="$('#btnProfileCancel').click()">
                                                    &times;</button>
                                                <h4 class="modal-title">
                                                    Profile Picture</h4>
                                            </div>
                                            <div class="modal-body">
                                                <asp:Image Width="240" Height="230" ID="imgProfile" runat="server" />
                                            </div>
                                            <div class="modal-footer">
                                                <asp:Button ClientIDMode="Static" data-dismiss="modal" runat="server" ID="btnProfileCancel"
                                                    Text="Close" CssClass="btn btn-default"></asp:Button>
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>
                                <asp:Label Text="" ID="ldlAckDummyPict" runat="server"></asp:Label>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
                <div class="space15">
                </div>
                <asp:UpdatePanel ID="updGrid" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:GridView ID="gvTable" runat="server" AutoGenerateColumns="False" DataKeyNames="UserID,FirstName,LastName,EMail,Mobile"
                            AllowSorting="true" Width="100%" OnRowDataBound="gvTable_RowDataBound" OnRowCommand="gvTable_RowCommand"
                            OnPageIndexChanging="gvTable_PageIndexChanging" OnSorting="gvTable_Sorting" EmptyDataText="No Recods Found"
                            OnRowDeleting="gvTable_RowDeleting" OnRowEditing="gvTable_RowEditing" CssClass="table table-striped table-hover table-bordered dataTable myTable"
                            OnPreRender="gvTable_PreRender">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:TemplateField HeaderText="User Name" HeaderStyle-ForeColor="Black">
                                    <ItemTemplate>
                                        <asp:Label ID="lblUserID" Visible="false" runat="server" Text='<%# Bind("UserID") %>'></asp:Label>
                                        <asp:Label ID="lblUserName" runat="server" Text='<%# Bind("UserName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="First Name" HeaderStyle-ForeColor="Black">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFirstName" runat="server" Text='<%# Bind("FirstName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Last Name" HeaderStyle-ForeColor="Black">
                                    <ItemTemplate>
                                        <asp:Label ID="lblLastName" runat="server" Text='<%# Bind("LastName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Email" HeaderStyle-ForeColor="Black">
                                    <ItemTemplate>
                                        <asp:Label ID="lblEmail" runat="server" Text='<%# Bind("EMail") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Mobile" HeaderStyle-ForeColor="Black">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMobile" runat="server" Text='<%# Bind("Mobile") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Picture" HeaderStyle-ForeColor="Black">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkViewPicture" CssClass="active" runat="server" Text="View Picture"
                                            CommandName="View" ToolTip="Edit" CommandArgument='<%# Eval("UserID") %>'></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="" SortExpression="">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkEdit" runat="server" Text="Edit" CommandName="Edit" ToolTip="Edit"
                                            CommandArgument='<%# Eval("UserID") %>'></asp:LinkButton>
                                        &nbsp; | &nbsp;
                                        <asp:LinkButton ID="lnkDelete" runat="server" CommandName="Delete" Text="Delete"
                                            CommandArgument='<%# Eval("UserID") %>' OnClick="btnDelete_Click"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="White" Font-Bold="True" ForeColor="Black" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#EFF3FB" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <SortedAscendingCellStyle BackColor="#F5F7FB" />
                            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                            <SortedDescendingCellStyle BackColor="#E9EBEF" />
                            <SortedDescendingHeaderStyle BackColor="#4870BE" />
                        </asp:GridView>
                        </div> </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnYes" />
                        <asp:AsyncPostBackTrigger ControlID="btnProfileCancel" />
                        <asp:AsyncPostBackTrigger ControlID="btnConfCancel" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
            <asp:Button ID="btnShowPopup" runat="server" Style="display: none" />
            <ajax:ModalPopupExtender ID="mpConfirmDelete" runat="server" TargetControlID="btnShowPopup"
                PopupControlID="pnlpopup" CancelControlID="btnCancel" BackgroundCssClass="modalBackground"
                Y="100" BehaviorID="mdlConfirmDelete">
            </ajax:ModalPopupExtender>
            <asp:Panel ID="pnlpopup" runat="server" Height="100px" Width="400px" Style="display: none">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true" onclick="HideModalPopup('mdlConfirmDelete');">
                                &times;</button>
                            <h4 class="modal-title">
                                Confirm Deletion</h4>
                        </div>
                        <div class="modal-body">
                            Are you sure you want to delete selected Record?
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="btnYes" OnClick="btnYes_Click" runat="server" Text="Confirm" OnClientClick="$('#btnCancel').click()"
                                CssClass="btn btn-success" />
                            <asp:Button ClientIDMode="Static" data-dismiss="modal" runat="server" ID="btnCancel"
                                Text="Cancel" CssClass="btn btn-default"></asp:Button>
                        </div>
                    </div>
                </div>
            </asp:Panel>
        </div>
        <!-- page end-->
    </div>
</asp:Content>


