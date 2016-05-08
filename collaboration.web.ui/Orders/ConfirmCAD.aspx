﻿<%@ Page Title="" Language="C#" MasterPageFile="~/CommonMaster.Master" AutoEventWireup="true" CodeFile="ConfirmCAD.aspx.cs" 
    Inherits="Collaboration.Web.UI.Orders.ConfirmCAD" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Src="~/UserControl/UC_CompareCADForCustomer.ascx" TagName="ConfirmCAD" TagPrefix="uc1" %>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:UpdatePanel ID="updAdd" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="panel">
                <div class="bio-graph-heading">
                    Review CAD - 
                                <asp:Label ID="lblSerialNumber" runat="server"></asp:Label>&nbsp;
                </div>
                <div class="panel-body" style="width: 1050px; margin: 10px">
                    <div id="dvMessage" runat="server" style="display: none" class="alert alert-block alert-danger fade in">
                        <asp:Literal ID="ltMessage" runat="server"></asp:Literal>
                    </div>
                    <uc1:ConfirmCAD ID="CompareCAD" runat="server" />
                    <div style="margin-left: 400px">
                        <asp:Button ClientIDMode="Static" runat="server" ID="btnApproveCAD" Text="Approve"
                            CssClass="btn btn-primary" OnClick="btnApprove_Click"></asp:Button>
                        <asp:Button ClientIDMode="Static" runat="server" ID="btnChangeRequest" Text="Request Change"
                            CssClass="btn btn-danger" OnClick="btnChangeRequest_Click"></asp:Button>
                        <asp:Button ClientIDMode="Static" data-dismiss="modal" runat="server" ID="btnDownload"
                                Text="Download" CssClass="btn btn-danger" OnClick="btnDownload_Click" style="display:none"></asp:Button>
                        <asp:Image ID="Image1" runat="server" style="display:none"/>
                        
                    </div>
                </div>
            </div>            
        </ContentTemplate>
    </asp:UpdatePanel>
    <ajaxToolkit:ModalPopupExtender BehaviorID="ModalBehaviourCAD" ID="mpAddCR" BackgroundCssClass="modalBackground"
                runat="server" TargetControlID="lblDummy" PopupControlID="pnlAddCR"
                Y="10" >
            </ajaxToolkit:ModalPopupExtender>
            <asp:Panel ID="pnlAddCR" runat="server" Style="display: none">
                <div class="modal-dialog" style="width: 700; height: 500;">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true" onclick="$find('ModalBehaviourCAD').hide()">
                                &times;</button>
                            <h4 class="modal-title">
                                Change Instructions</h4>
                        </div>
                        <div class="modal-body">
                            <textarea runat="server" id="txtChangeInstructions" class="form-control" cols="60"
                                rows="3"></textarea>
                        </div>
                        <div class="modal-footer">
                            <asp:Button ClientIDMode="Static" data-dismiss="modal" runat="server" ID="btnCancel"
                                Text="Send" CssClass="btn btn-danger" OnClick="btnSend_Click"></asp:Button>
                        </div>
                    </div>
                </div>
            </asp:Panel>
            <asp:Label Text="" ID="lblDummy" runat="server"></asp:Label>
</asp:Content>


