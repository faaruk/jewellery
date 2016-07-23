<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UC_ViewOrderDetails.ascx.cs"
    Inherits="Collaboration.Web.UI.UserControl.UC_ViewOrderDetails" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<div class="row">
    <div class="col-lg-6">
        <div class="panel">
            <div class="subHeading alert-info fade in">
                General Details
            </div>
            <div class="panel-body">
                <div class="form-horizontal custome_form_stl">
                    <div class="form-group">
                        <label class="col-lg-4 col-sm-3">
                            Customer Name
                        </label>
                        <div class="col-lg-8">
                            : &nbsp;
                            <asp:Label ID="lblCustomerName" runat="server">
                            </asp:Label>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-lg-4 col-sm-3">
                            Customer Email
                        </label>
                        <div class="col-lg-8">
                            : &nbsp;
                            <asp:Label ID="lblCustomerEMail" runat="server">
                            </asp:Label>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-lg-4 col-sm-3">
                            Expected Shipping</label>
                        <div class="col-lg-8">
                            : &nbsp;
                            <asp:Label ID="lblExpectedShippingDate" runat="server">
                            </asp:Label>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-lg-4 col-sm-3">
                            Serial Number</label>
                        <div class="col-lg-8">
                            : &nbsp;
                            <asp:Label ID="lblSerialNumber" runat="server">
                            </asp:Label>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-lg-4 col-sm-3">
                            Model Type
                        </label>
                        <div class="col-lg-8">
                            : &nbsp;
                            <asp:Label ID="lblModelType" runat="server">
                            </asp:Label>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="inputPassword1" class="col-lg-4 col-sm-3">
                            Model Number</label>
                        <div class="col-lg-8">
                            : &nbsp;
                            <asp:Label ID="lblModelNumber" runat="server">
                            </asp:Label>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-lg-4 col-sm-3">
                            Process Type
                        </label>
                        <div class="col-lg-8">
                            : &nbsp;
                            <asp:Label ID="lblProcessType" runat="server">
                            </asp:Label>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-lg-4 col-sm-3">
                            Priority
                        </label>
                        <div class="col-lg-8">
                            : &nbsp;
                            <asp:Label ID="lblPriority" runat="server">
                            </asp:Label>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-lg-4 col-sm-3">
                            Metal
                        </label>
                        <div class="col-lg-8">
                            : &nbsp;
                            <asp:Label ID="lblMetal" runat="server">
                            </asp:Label>
                        </div>
                    </div>
                    <div class="form-group" id="divMetalother" runat="server">
                        <label class="col-lg-4 col-sm-3">
                           <asp:Label ID="lblMetalOtherText" runat="server" Text="Metal Other"></asp:Label>
                        </label>
                        <div class="col-lg-8">
                            
                            <asp:Label ID="lblMetalOther" runat="server">
                            </asp:Label>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-lg-4 col-sm-3">
                            Quantity
                        </label>
                        <div class="col-lg-8">
                             : &nbsp;
                            <asp:Label ID="lblQuantity" runat="server">
                            </asp:Label>
                        </div>
                    </div>
                    <div class="form-group" runat="server" id="divQuantityOther">
                        <label class="col-lg-4 col-sm-3">
                           <asp:Label ID="lblQuantityOtherText" runat="server" Text="Quantity"></asp:Label>
                        </label>
                        <div class="col-lg-8">
                           
                            <asp:Label ID="lblQuantityOther" runat="server">
                            </asp:Label>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="exampleInputFile" class="col-lg-4 col-sm-3">
                            Image Instructions</label>
                        <div class="col-lg-8">
                            :  <asp:DataList Visible="false" ID="dlImages" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"
                        BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" OnItemDataBound="dlImages_RowDataBound"
                        CellPadding="3">                      
                        <ItemTemplate>
                            <table border="0" style="border-bottom-color: #60BAEA; border-top-color: #60BAEA;
                                border-left-color: #60BAEA; border-left-color: #60BAEA;" cellspacing="5">
                                <tr>
                                    <td align="center">
                                        <asp:HyperLink runat="server" ID="imgLink" rel="img-group" CssClass="img-popup">
                                        <asp:Image ID="imgSpecimen" runat="server" align="center" BorderStyle="Solid" BorderColor="#e0ddd7"
                                            BorderWidth="2" Height="120"  
                                            Width="120" />
                                        </asp:HyperLink>
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>                     
                    </asp:DataList>
                        </div>
                    </div>
                    <div class="form-group" >
                        <label class="col-lg-4 col-sm-3">
                            Make Exact Copies</label>
                        <div class="col-lg-8">
                            : &nbsp;
                            <asp:Label ID="lblMakeExactCopies" runat="server">
                            </asp:Label>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!--Left Side End-->
    <!--Right Side-->
    <div class="col-lg-6">
        <div class="panel">
            <div class="subHeading alert-info fade in">
                Advance Details
            </div>
            <div class="panel-body">
                <div class="form-horizontal">
                    <div id="divFingerSize" runat="server" visible="false">
                        <div class="form-group" id="divFingerSizeLabel" runat="server">
                            <label class="col-lg-4 col-sm-3">
                                Finger Size</label>
                            <div class="col-lg-8">
                                : &nbsp;
                                <asp:Label ID="lblFingerSize" runat="server">
                                </asp:Label>
                            </div>
                        </div>
                        <div class="form-group" id="divFingerSizeOtherLabel" runat="server">
                            <label class="col-lg-4 col-sm-3">
                                Finger Size Other</label>
                            <div class="col-lg-8">
                                : &nbsp;
                                <asp:Label ID="lblFingerSizeOther" runat="server">
                                </asp:Label>
                            </div>
                        </div>
                    </div>
                    <div id="divGeneral" runat="server"  visible="false">
                        <div class="form-group" id="divModelSubType" runat="server">
                            <b>
                                <asp:Label CssClass="col-lg-4 col-sm-3" ID="lblModelSubTypeText" runat="server">                                
                                </asp:Label></b>
                            <div class="col-lg-8">
                                : &nbsp;
                                <asp:Label ID="lblModelSubTypeValue" runat="server">
                                </asp:Label>
                            </div>
                        </div>
                        <div class="form-group" id="divHeadsize" runat="server">
                            <label class="col-lg-4 col-sm-3">
                                Headsize
                            </label>
                            <div class="col-lg-8">
                                : &nbsp;
                                <asp:Label ID="lblHeadSizeGeneral" runat="server">
                                </asp:Label>
                            </div>
                        </div>
                        <div id="divMatchJacketModel" runat="server" visible="false">
                            <label class="col-lg-4 col-sm-3">
                              Earring Model To Match
                            </label>
                            <div class="col-lg-8">
                                : &nbsp;
                                <asp:Label ID="lblMatchJacketModel" runat="server">
                                </asp:Label>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-lg-4 col-sm-3">
                                Existing Model</label>
                            <div class="col-lg-8">
                                : &nbsp;
                                <asp:Label ID="lblIsExistingGeneral" runat="server">
                                </asp:Label>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-lg-4 col-sm-3">
                                Additional Info
                            </label>
                            <div class="col-lg-8">
                                : &nbsp;
                                <asp:Label ID="lblAdditionalInfoGeneral" runat="server">
                                </asp:Label>
                            </div>
                        </div>
                        <div class="form-group" id="divLength" runat="server" visible="false">
                            <label class="col-lg-4 col-sm-3">
                                Length
                            </label>
                            <div class="col-lg-8">
                                : &nbsp;
                                <asp:Label ID="lblLength" runat="server">
                                </asp:Label>
                            </div>
                        </div>
                    </div>
                    <div id="divRingType" runat="server" visible="false">
                        <div class="form-group">
                            <label class="col-lg-4 col-sm-3">
                                Ring Type
                            </label>
                            <div class="col-lg-8">
                                : &nbsp;
                                <asp:Label ID="lblRingType" runat="server">
                                </asp:Label>
                            </div>
                        </div>
                        <div id="divRingMountType" runat="server" visible="false">
                            <div class="form-group" id="Ext_chk">
                                <label class="col-lg-4 col-sm-3">
                                    Existing Model</label>
                                <div class="col-lg-8">
                                    : &nbsp;
                                    <asp:Label ID="lblIsExisting" runat="server">
                                    </asp:Label>
                                </div>
                            </div>
                            <div class="form-group" id="pf_chk">
                                <label class="col-lg-4 col-sm-3">
                                    PF</label>
                                <div class="col-lg-8">
                                    : &nbsp;
                                    <asp:Label ID="lblIsPF" runat="server">
                                    </asp:Label>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-lg-4 col-sm-3">
                                    Headsize
                                </label>
                                <div class="col-lg-8">
                                    : &nbsp;
                                    <asp:Label ID="lblHeadSizeModel" runat="server">
                                    </asp:Label>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-lg-4 col-sm-3">
                                    Additional Info
                                </label>
                                <div class="col-lg-8">
                                    : &nbsp;
                                    <asp:Label ID="lblAdditionalInfoMount" runat="server">
                                    </asp:Label>
                                </div>
                            </div>
                        </div>
                        <div id="divMatchingETType" runat="server" visible="false">
                         <div class="form-group">
                            <label class="col-lg-4 col-sm-3">
                                Headsize
                            </label>
                            <div class="col-lg-8">
                                : &nbsp;
                                <asp:Label ID="lblHeadSizET" runat="server">
                                </asp:Label>
                            </div>
                            </div>
                            <div class="form-group">
                                <label class="col-lg-4 col-sm-3">
                                    Model Number To Match
                                </label>
                                <div class="col-lg-8">
                                    : &nbsp;
                                    <asp:Label ID="lblMatchModelNumber" runat="server">
                                    </asp:Label>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-lg-4 col-sm-3">
                                    Curve Type
                                </label>
                                <div class="col-lg-8">
                                    : &nbsp;
                                    <asp:Label ID="lblCurveType" runat="server">
                                    </asp:Label>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-lg-4 col-sm-3">
                                    Tailored Type
                                </label>
                                <div class="col-lg-8">
                                    : &nbsp;
                                    <asp:Label ID="lblTailoredType" runat="server">
                                    </asp:Label>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-lg-4 col-sm-3">
                                    Finish at the same point
                                </label>
                                <div class="col-lg-8">
                                    : &nbsp;
                                    <asp:Label ID="lblFinishAtSomePoint" runat="server">
                                    </asp:Label>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-lg-4 col-sm-3">
                                    Additional Info
                                </label>
                                <div class="col-lg-8">
                                    : &nbsp;
                                    <asp:Label ID="lblAdditionalInfoET" runat="server">
                                    </asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="form-group" id="Div4">
                        <label class="col-lg-4 col-sm-3">
                            CAD Requested
                        </label>
                        <div class="col-lg-8">
                            : &nbsp;
                            <asp:Label ID="lblCADRequested" runat="server">
                            </asp:Label>
                        </div>
                    </div>
                    <div class="form-group" id="Div7">
                        <label class="col-lg-4 col-sm-3">
                            Sample Provided</label>
                        <div class="col-lg-8">
                            : &nbsp;
                            <asp:Label ID="lblSampleProvided" runat="server">
                            </asp:Label>
                        </div>
                    </div>
                    <div id="divSampleSerialNumber" runat="server" visible="false">
                        <div class="form-group">
                            <label class="col-lg-4 col-sm-3">
                                No Of Samples</label>
                            <div class="col-lg-8">
                                : &nbsp;
                                <asp:Label ID="lblNoOfSamples" runat="server">
                                </asp:Label>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-lg-4 col-sm-3">
                                Sample Serial Number's</label>
                            <div class="col-lg-8">
                                : &nbsp;
                                <asp:Label ID="lblSampleSerialNumber" runat="server">
                                </asp:Label>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-lg-4 col-sm-3">
                                Make exact copy of samples</label>
                            <div class="col-lg-8">
                                : &nbsp;
                                <asp:Label ID="lblMakeExistingModelSample" runat="server">
                                </asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="form-group" >
                        <label class="col-lg-4 col-sm-3">
                            Stone Provided</label>
                        <div class="col-lg-8">
                            : &nbsp;
                            <asp:Label ID="lblStoneProvided" runat="server">
                            </asp:Label>
                        </div>
                    </div>
                    <div id="divStoneProvided" runat="server" visible="false">
                        <div class="form-group" id="Div8">
                            <label class="col-lg-4 col-sm-3">
                                Stone Description</label>
                            <div class="col-lg-8">
                                : &nbsp;
                                <asp:Label ID="lblStoneDescription" runat="server">
                                </asp:Label>
                            </div>
                        </div>
                        <div class="form-group" id="Div8">
                            <label class="col-lg-4 col-sm-3">
                                Setting Instructions</label>
                            <div class="col-lg-8">
                                : &nbsp;
                                <asp:Label ID="lblSettingInstructions" runat="server">
                                </asp:Label>
                            </div>
                        </div>                       
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="col-lg-6">
        <div class="panel">
            <div class="subHeading alert-info">
                Advance Details
            </div>
            <div class="panel-body">
                <div class="form-horizontal" role="form">
                    <div class="form-group" id="Div9">
                        <label class="col-lg-3 col-sm-4">
                            Team Member</label>
                        <div class="col-lg-8">
                            : &nbsp;
                            <asp:Label ID="lblTMUserName" runat="server">
                            </asp:Label>
                        </div>
                    </div>
                    <div class="form-group" id="Div10">
                        <label class="col-lg-3 col-sm-4">
                            Assignee</label>
                        <div class="col-lg-9">
                            : &nbsp;
                            <asp:Label ID="lblAssigneeUserName" runat="server">
                            </asp:Label>
                        </div>
                    </div>
                    <div class="form-group" id="Div11">
                        <label class="col-lg-3 col-sm-4">
                            Remark</label>
                        <div class="col-lg-9">
                            : &nbsp;
                            <asp:Label ID="lblRemarks" runat="server">
                            </asp:Label>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
<%--    <ajaxToolkit:ModalPopupExtender BehaviorID="ModalBehaviourSpecimenImages" ID="mpSpecimenImages" BackgroundCssClass="modalBackground"
        runat="server" TargetControlID="lblDummy" PopupControlID="pnlSpecimenImages" Y="10"
        CancelControlID="btnCancel">
    </ajaxToolkit:ModalPopupExtender>
    <asp:Panel ID="pnlSpecimenImages" runat="server" Style="display: none">
        <div class="modal-dialog" style="width: 700; height: 500;">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true" onclick="$find('ModalBehaviourSpecimenImages').hide()">
                        &times;</button>
                    <h4 class="modal-title">
                        Specimen</h4>
                </div>
                <div class="modal-body">
                    <asp:DataList ID="dlImages" runat="server" RepeatColumns="3" RepeatDirection="Horizontal"
                        BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" OnItemDataBound="dlImages_RowDataBound"
                        CellPadding="3">                      
                        <ItemTemplate>
                            <table border="0" style="border-bottom-color: #60BAEA; border-top-color: #60BAEA;
                                border-left-color: #60BAEA; border-left-color: #60BAEA;" cellspacing="5">
                                <tr>
                                    <td align="center">
                                        <asp:Image ID="imgSpecimen" runat="server" align="center" BorderStyle="Solid" BorderColor="#e0ddd7"
                                            BorderWidth="2" Height="120"  
                                            Width="120" />
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>                     
                    </asp:DataList>
                </div>
                <div class="modal-footer">
                    <asp:Button ClientIDMode="Static" data-dismiss="modal" runat="server" ID="btnCancel"
                        Text="Close" CssClass="btn btn-default"></asp:Button>
                </div>
            </div>
        </div>
    </asp:Panel>
    <asp:Label Text="" ID="lblDummy" runat="server"></asp:Label>--%>
</div>
