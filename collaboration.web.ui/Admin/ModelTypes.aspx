<%@ Page Title="Model Types" Language="C#" AutoEventWireup="true" CodeBehind="ModelTypes.aspx.cs" MasterPageFile="~/DasbhoardMaster.Master" Inherits="Collaboration.Web.UI.Admin.ModelTypes" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">

    <!-- page start-->
    <div class="panel">
        <div class="bio-graph-heading">
            Model Types
        </div>
        <div class="panel-body">
            <div class="adv-table editable-table ">
                <div class="clearfix">
                    <div>
                        <asp:UpdatePanel ID="updAdd" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <ajaxToolkit:ModalPopupExtender BehaviorID="ModalBehaviour" ID="mpAdd" BackgroundCssClass="modalBackground"
                                    runat="server" TargetControlID="ldlDummy" PopupControlID="pnl" Y="100" CancelControlID="btnCancelModelType">
                                </ajaxToolkit:ModalPopupExtender>
                                <asp:Panel Style="display: none;" ID="pnl" runat="server" BorderColor="ActiveBorder" BorderStyle="Solid" BorderWidth="0px">
                                    <div id="myModal">
                                        <div class="modal-dialog">
                                            <div class="modal-content" style="width: 550px;">
                                                <div class="modal-header">
                                                    <button runat="server" id="imgCancel" type="button" class="close" data-dismiss="modal"
                                                        aria-hidden="true" onclick="$find('ModalBehaviour').hide();EditableTable.init();">
                                                        &times;</button>
                                                    <h4 class="modal-title">
                                                        Add/Edit ModelType
                                                    </h4>
                                                </div>
                                                <div id="dvMessage" runat="server" style="display: none" class="alert alert-block alert-danger fade in">
                                                    <button data-dismiss="alert" class="close close-sm" type="button">
                                                        <i class="icon-remove"></i>
                                                    </button>
                                                    <asp:Literal ID="ltMessage" runat="server"></asp:Literal>
                                                </div>
                                                <div class="panel-body bio-graph-info form-horizontal">
                                                    <div class="form-group">
                                                        <label class="col-lg-4 control-label">
                                                            Model Code</label>
                                                        <div class="col-lg-6">
                                                            <asp:TextBox ID="txtModelCode" runat="server" CssClass="form-control" MaxLength="50" AutoPostBack="true" OnTextChanged="txtModelCode_TextChanged"></asp:TextBox>
                                                            <asp:RequiredFieldValidator Display="Dynamic" SetFocusOnError="true" ID="txtModelCodeRequired"
                                                                runat="server" ControlToValidate="txtModelCode" CssClass="failureNotification"
                                                                ForeColor="Red" ErrorMessage="Model Code is required." InitialValue="" ValidationGroup="EditModelTypeValidationGroup"></asp:RequiredFieldValidator>
                                                            <asp:CustomValidator ID="UniqModelValidator" runat="server" ForeColor="Red" ErrorMessage="This model is already exists" Display="Dynamic" ControlToValidate="txtModelCode" ValidationGroup="EditModelTypeValidationGroup"></asp:CustomValidator>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-lg-4 control-label">
                                                            SortOrder</label>
                                                        <div class="col-lg-6">
                                                            <asp:TextBox ID="txtSortOrder" runat="server" CssClass="form-control" MaxLength="2"></asp:TextBox>
                                                            <asp:RequiredFieldValidator Display="Dynamic" SetFocusOnError="true" ID="txtSortOrderRequired"
                                                                runat="server" ControlToValidate="txtSortOrder" CssClass="failureNotification"
                                                                ForeColor="Red" ErrorMessage="Sort Order is required." InitialValue="" ValidationGroup="EditModelTypeValidationGroup"></asp:RequiredFieldValidator>
                                                            <asp:RegularExpressionValidator Display="Dynamic" ID="rgFrequency" runat="server"
                                                                ErrorMessage="Please enter only numeric Sort Order" ValidationGroup="EditPriorityValidationGroup"
                                                                ControlToValidate="txtSortOrder" CssClass="failureNotification" ForeColor="Red"
                                                                SetFocusOnError="true" InitialValue="" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-lg-4 control-label">
                                                            Description</label>
                                                        <div class="col-lg-6">
                                                            <asp:TextBox ID="txtDescription" runat="server" CssClass="form-control" MaxLength="100"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <div class="col-lg-offset-4 col-lg-10">
                                                            <asp:Button runat="server" Text="Save" CssClass="btn btn-primary" ID="btnUpdate"
                                                                OnClick="btnUpdate_Click" ValidationGroup="EditModelTypeValidationGroup"></asp:Button>
                                                            <asp:Button data-dismiss="modal" runat="server" ID="btnCancelModelType" Text="Close"
                                                                OnClientClick="javascript:EditableTable.init();" CssClass="btn btn-default">
                                                            </asp:Button>
                                                        </div>
                                                    </div>
                                                </div>
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
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="gvTable" />
                                <asp:AsyncPostBackTrigger ControlID="btnAddNew" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>
                <div class="space15">
                </div>
                <asp:UpdatePanel ID="updGrid" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:GridView ID="gvTable" runat="server" AutoGenerateColumns="False" DataKeyNames="ModelTypeID"
                            AllowSorting="True" Width="100%" OnRowCommand="gvTable_RowCommand" OnPageIndexChanging="gvTable_PageIndexChanging"
                            OnSorting="gvTable_Sorting" EmptyDataText="No Recods Found" OnRowDeleting="gvTable_RowDeleting"
                            OnRowEditing="gvTable_RowEditing" CssClass="table table-striped table-hover table-bordered dataTable myTable"
                            OnPreRender="gvTable_PreRender">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:TemplateField HeaderText="Model Code" HeaderStyle-ForeColor="#797979">
                                    <ItemTemplate>
                                        <asp:Label ID="lblModelCode" runat="server" Text='<%# Bind("ModelCode") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sort Order" HeaderStyle-ForeColor="#797979">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSortOrder" runat="server" Text='<%# Bind("SortOrder") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Description" HeaderStyle-ForeColor="#797979">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDescription" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkEdit" runat="server" Text="Edit" CommandName="Edit" ToolTip="Edit"
                                            CommandArgument='<%# Eval("ModelTypeID") %>'></asp:LinkButton>
                                        &nbsp; | &nbsp;
                                        <asp:LinkButton ID="lnkDelete" runat="server" CommandName="Delete" Text="Delete"
                                            CommandArgument='<%# Eval("ModelTypeID") %>' OnClick="btnDelete_Click"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="White" Font-Bold="True" ForeColor="Black" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#EFF3FB" />
                            <SelectedRowStyle BackColor="Black" Font-Bold="True" ForeColor="#333333" />
                            <SortedAscendingCellStyle BackColor="#F5F7FB" />
                            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                            <SortedDescendingCellStyle BackColor="#E9EBEF" />
                            <SortedDescendingHeaderStyle BackColor="#4870BE" />
                        </asp:GridView>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnYes" />
                        <asp:AsyncPostBackTrigger ControlID="btnConfCancel" />
                    </Triggers>
                </asp:UpdatePanel>
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
        </div>
        <!-- page end-->
    </div>
</asp:Content>
