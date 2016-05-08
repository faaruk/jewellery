<%@ Page Title="" Language="C#" MasterPageFile="~/DasbhoardMaster.Master" AutoEventWireup="true" CodeBehind="TrackSamples.aspx.cs"
    Inherits="Collaboration.Web.UI.Tracking.TrackSamples" %>

<%@ Import Namespace="Collaboration.Web.UI" %>

<%@ Register Src="../UserControl/UC_Messages.ascx" TagName="ViewMessages" TagPrefix="uc1" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="panel">
        <div class="bio-graph-heading">
            Track Sample Status
        </div>
        <div class="panel-body">
            <div class="adv-table editable-table ">
                <div class="clearfix">
                    <div>
                        <asp:UpdatePanel ID="updAdd" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <ajaxToolkit:ModalPopupExtender BehaviorID="ModalBehaviour" ID="mpAdd" BackgroundCssClass="modalBackground" runat="server"
                                    TargetControlID="ldlDummy" PopupControlID="pnl" Y="100" CancelControlID="btnCancelSampleStatus">
                                </ajaxToolkit:ModalPopupExtender>
                                <asp:Panel Style="display: none;" ID="pnl" runat="server" BorderColor="ActiveBorder"
                                    BorderStyle="Solid" BorderWidth="0px">
                                    <div id="myModal">
                                        <div class="modal-dialog">
                                            <div class="modal-content" style="width: 550px;">
                                                <div class="modal-header">
                                                    <button runat="server" id="imgCancel" type="button" class="close" data-dismiss="modal"
                                                        aria-hidden="true" onclick="$find('ModalBehaviour').hide()">
                                                        &times;</button>
                                                    <h4 class="modal-title">Update Sample Status
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
                                                            Serial Number:</label>
                                                        <div class="col-lg-6">
                                                            <asp:Label ID="lblSampleSerialNumber" runat="server" CssClass="form-control" MaxLength="50"></asp:Label>

                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-lg-4 control-label">
                                                            Status Name:</label>
                                                        <div class="col-lg-6">
                                                            <asp:DropDownList ID="ddlSampleStatusName" runat="server" CssClass="form-control"
                                                                DataTextField="SampleStatusName" DataValueField="SampleStatusID">
                                                            </asp:DropDownList>

                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <div class="col-lg-offset-4 col-lg-10">
                                                            <asp:Button runat="server" Text="Update" CssClass="btn btn-default" ID="btnUpdate"
                                                                OnClick="btnUpdate_Click" ValidationGroup="EditSampleStatusValidationGroup"></asp:Button>
                                                            <asp:Button data-dismiss="modal" runat="server" ID="btnCancelSampleStatus" Text="Close"
                                                                CssClass="btn btn-default"></asp:Button>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>
                                <div class="btn-group">
                                    <%--  <asp:Button  class="btn btn-primary" Text="Add New" runat="server"
                                        ID="btnAddNew"></asp:Button>--%>
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
                                                <h4 class="modal-title">Confirm
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
                                <%-- <asp:AsyncPostBackTrigger ControlID="btnAddNew" />--%>
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>
                <div class="clearfix">
                    <div>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <ajaxToolkit:ModalPopupExtender BehaviorID="ModalBehaviourx" ID="ModalPopupExtender1" BackgroundCssClass="modalBackground" runat="server"
                                    TargetControlID="Label4" PopupControlID="Panel1" Y="100" CancelControlID="Button1">
                                </ajaxToolkit:ModalPopupExtender>
                                <asp:Panel Style="display: none;" ID="Panel1" runat="server" BorderColor="ActiveBorder"
                                    BorderStyle="Solid" BorderWidth="0px">
                                    <div id="myModal1">
                                        <div class="modal-dialog">
                                            <div class="modal-content" style="width: 550px;">
                                                <div class="modal-header">
                                                    <button runat="server" id="Button1" type="button" class="close" data-dismiss="modal"
                                                        aria-hidden="true" onclick="$find('ModalBehaviourx').hide()">
                                                        &times;</button>
                                                    <h4 class="modal-title">Update Sample Status
                                                    </h4>
                                                </div>
                                                fkghfkdghkfdg
                                                <br />
                                                fgfdhg
                                                <br />
                                                fdgdl;g
                                                <br />
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>

                                <asp:Label Text="" ID="Label4" runat="server"></asp:Label>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="gvTable" />
                                <%-- <asp:AsyncPostBackTrigger ControlID="btnAddNew" />--%>
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>
                <div class="space15">
                </div>
                <asp:UpdatePanel ID="updGrid" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:GridView ID="gvTable" runat="server" AutoGenerateColumns="False" DataKeyNames="SampleTrackID"
                            AllowSorting="True" Width="100%" OnRowCommand="gvTable_RowCommand" OnPageIndexChanging="gvTable_PageIndexChanging"
                            OnSorting="gvTable_Sorting" EmptyDataText="No Recods Found"
                            OnRowEditing="gvTable_RowEditing" CssClass="table table-striped table-hover table-bordered dataTable myTable"
                            OnPreRender="gvTable_PreRender" OnRowDataBound="gvTable_RowDataBound">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:TemplateField HeaderText="Sample Serial Number" HeaderStyle-ForeColor="#797979">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSampleSerialNumber" runat="server" Text='<%# Eval("SampleSerialNumber") %>'></asp:Label>
                                        <asp:HiddenField ID="hdnSampleTrackID" runat="server" Value='<%# Eval("SampleTrackID") %>' />
                                        <asp:HiddenField ID="hdnSampleID" runat="server" Value='<%# Eval("SampleID") %>' />
                                        <asp:HiddenField ID="hdnSampleStatusID" runat="server" Value='<%# Eval("SampleStatusID") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Sample Status" HeaderStyle-ForeColor="#797979">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSampleStatusName" runat="server" Text='<%# Eval("SampleStatusName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkUpdate" runat="server" Text="Edit" CommandName="Edit" ToolTip="Edit"
                                            CommandArgument='<%# Eval("SampleTrackID") %>'></asp:LinkButton>

                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkEdit" runat="server" CssClass="btn btn-info btn-xs" Text="View Details"
                                            CommandName="ViewDetails" ToolTip="View Details" CommandArgument='<%# Eval("SampleID").ToString()+","+ Eval("SampleTrackID").ToString()  %>'></asp:LinkButton>

                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="White" Font-Bold="True" ForeColor="Black" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#EFF3FB" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        </asp:GridView>
                    </ContentTemplate>
                    <%-- <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnYes" />
                        <asp:AsyncPostBackTrigger ControlID="btnConfCancel" />
                    </Triggers>--%>
                </asp:UpdatePanel>
            </div>



        </div>

    </div>
</asp:Content>
