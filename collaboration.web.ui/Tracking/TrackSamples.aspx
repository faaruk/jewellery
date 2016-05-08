<%@ Page Title="" Language="C#" MasterPageFile="~/DasbhoardMaster.Master" AutoEventWireup="true"
    CodeFile="TrackSamples.aspx.cs" Inherits="Collaboration.Web.UI.Tracking.Tracking_Default" %>

<%@ Register Src="~/UserControl/UC_TrackSHistory.ascx" TagName="ViewMessages" TagPrefix="uc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">

    <script language="javascript" type="text/javascript">
        function SelectAllProjects(chk) {
            $('#<%=gvTable.ClientID%>').find("input:checkbox").each(function () {
                if (this != chk) {
                    this.checked = chk.checked;
                }
            });
        }
        function ShowConfirmation() {
            var frm = document.forms[0];
            var Selected = false;
            var intcount = 0;
            for (i = 0; i < frm.elements.length; i++) {
                if (frm.elements[i].type == "checkbox") {
                    if (frm.elements[i].checked) {
                        //alert(frm.elements[i].id);
                        if (frm.elements[i].id != "MainContent_gvTable_chkAll") {
                            Selected = true;
                            intcount = intcount + 1;
                        }
                        //break;
                    }
                }
            }
            //alert(intcount);
            if (Selected == false) {
                alert("Please select atleast one sample.");
            }
            else {
                var txt;
                var r = confirm("Are you sure to update the selected " + intcount + " sample(s).");
                if (r == true) {
                    //alert("1");
                    return true;
                }
                else {
                    //alert("0");
                    return false;
                }
            }
        }
    </script>
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
                        <asp:UpdatePanel ID="updViewDetails" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <ajaxToolkit:ModalPopupExtender ID="mpViewDetails" BackgroundCssClass="modalBackground"
                                    runat="server" TargetControlID="ldlDummyViewDetails" PopupControlID="pnlViewDetails"
                                    Y="10" X="300" CancelControlID="imgCancelViewDetails">
                                </ajaxToolkit:ModalPopupExtender>
                                <asp:Panel Style="display: none;" ID="pnlViewDetails" runat="server" BorderColor="ActiveBorder"
                                    BorderStyle="Solid" BorderWidth="0px">
                                    <div id="myModal1">
                                        <div class="modal-dialog" style="width: 950px; height: 500px">
                                            <div class="modal-content">
                                                <div class="modal-header">
                                                    <button runat="server" id="imgCancelViewDetails" type="button" class="close" data-dismiss="modal"
                                                        aria-hidden="true" onserverclick="btnCancelDetails_Click">
                                                        &times;</button>
                                                    <h4 class="modal-title">Sample Status History: 
                                                        <asp:Label Text="" ID="Label1" runat="server"></asp:Label>
                                                    </h4>
                                                </div>
                                                <uc1:ViewMessages ID="ViewMessages" runat="server" />
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>
                                <asp:Label Text="" ID="ldlDummyViewDetails" runat="server"></asp:Label>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>

                <div class="space15">
                </div>
                <div class="row" style="margin-left: 5px; margin-right: 5px; margin-bottom: 5px;">
                    <div class="chat-form" style="margin-top: 10px; padding-left: 5px; padding-right: 15px">
                        <div class="pull-left" style="margin-top: 5px; padding-left: 0px">
                            <a href="javascript:;">Search by Status:</a>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:DropDownList ID="ddlSearchbyStatus" runat="server" CssClass="form-control"
                                        DataTextField="SampleStatusName" DataValueField="SampleStatusID" AutoPostBack="True"
                                        OnSelectedIndexChanged="ddlSearchbyStatus_SelectedIndexChanged">
                                    </asp:DropDownList>

                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="gvTable" />
                                    <%-- <asp:AsyncPostBackTrigger ControlID="btnAddNew" />--%>
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                    </div>
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
                                <asp:TemplateField HeaderStyle-ForeColor="#797979">
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="chkAll" runat="server" onclick="javascript:SelectAllProjects(this);"
                                            ToolTip="Select/Deselect all" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox runat="server" ID="chkRow" />
                                        <asp:TextBox runat="server" ID="txtKeyField" Text='<%#Eval("SampleID") %>' Style="display: none" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" />
                                    <HeaderStyle HorizontalAlign="left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sample Serial Number" HeaderStyle-ForeColor="#797979">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSampleSerialNumber" runat="server" Text='<%# Eval("SampleSerialNumber") %>'></asp:Label>
                                        <asp:HiddenField ID="hdnSampleTrackID" runat="server" Value='<%# Eval("SampleTrackID") %>' />
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
                                        <asp:LinkButton ID="lnkEdit" runat="server" CssClass="btn btn-info btn-xs" Text="View History"
                                            CommandName="ViewDetails" ToolTip="View Details" CommandArgument='<%# Eval("SampleID").ToString()+","+ Eval("SampleTrackID").ToString() +","+ Eval("SampleSerialNumber").ToString() %>'></asp:LinkButton>

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
                    <%--<Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddlSearchbyStatus" EventName="SelectedIndexChanged" />
                    </Triggers>--%>
                </asp:UpdatePanel>
                <div class="row" style="margin-left: 5px; margin-right: 5px; margin-bottom: 5px;">
                    <div class="chat-form" style="margin-top: 10px; padding-left: 5px; padding-right: 15px">
                        <div class="pull-left" style="margin-top: 5px; padding-left: 0px">
                            <a href="javascript:;">Change Status To:</a>
                            <asp:DropDownList ID="ddlSampleStatus" runat="server" CssClass="form-control"
                                DataTextField="SampleStatusName" DataValueField="SampleStatusID">
                            </asp:DropDownList>
                        </div>

                        <div class="form-group pull-right" style="margin-top: 18px; padding-right: 10px">
                            <div class="pull-left">
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <%-- <a href="javascript:;"><i class="icon-camera"></i></a>&nbsp;&nbsp;--%>
                                        <asp:Button ID="btnSend" runat="server" CssClass="btn btn-danger" Text="Update" OnClick="btnSend_Click" OnClientClick="return ShowConfirmation();"/>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>



        </div>

    </div>
</asp:Content>


