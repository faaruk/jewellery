<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UC_ChangePassword.ascx.cs"
    Inherits="Collaboration.Web.UI.UserControl.UC_ChangePassword" %>
<div class="panel panel-primary">
    <div class="bio-graph-heading">
        Change Password</div>
    <div id="dvMessage" runat="server" style="display: none" class="alert alert-block alert-danger fade in">
       <%-- <button data-dismiss="alert" class="close close-sm" type="button">
            <i class="icon-remove"></i>
        </button>--%>
        <asp:Literal ID="ltMessage" runat="server"></asp:Literal>
    </div>
    <div class="panel-body bio-graph-info form-horizontal">
        <div class="form-group">
            <label class="col-lg-4 control-label">
                New Password</label>
            <div class="col-lg-6">
                <asp:TextBox runat="server" TextMode="Password" CssClass="form-control" ID="txtNewPassword"></asp:TextBox>
                <asp:RequiredFieldValidator Display="Dynamic" SetFocusOnError="true" ID="NewPasswordRequired"
                    runat="server" ControlToValidate="txtNewPassword" CssClass="failureNotification"
                    ForeColor="Red" ErrorMessage="This field is required." ValidationGroup="ChangePwdValidationGroup"
                    InitialValue=""></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="form-group">
            <label class="col-lg-4 control-label">
                Confirm New Password</label>
            <div class="col-lg-6">
                <asp:TextBox runat="server" TextMode="Password" CssClass="form-control" ID="txtConfirmNewPassword"></asp:TextBox>
                <asp:RequiredFieldValidator Display="Dynamic" SetFocusOnError="true" ID="ConfirmPasswordRequired"
                    runat="server" ControlToValidate="txtConfirmNewPassword" CssClass="failureNotification"
                    ForeColor="Red" ErrorMessage="This field is required." ValidationGroup="ChangePwdValidationGroup"
                    InitialValue=""></asp:RequiredFieldValidator>
                <asp:CompareValidator Display="Dynamic" runat="server" ID="PwdCompareValdator" ForeColor="Red"
                    ErrorMessage="Confirm Password should match with the new Password" ControlToCompare="txtNewPassword"
                    ControlToValidate="txtConfirmNewPassword" ValidationGroup="ChangePwdValidationGroup"
                    InitialValue="">
                </asp:CompareValidator>
            </div>
        </div>
        <div class="form-group">
            <div class="col-lg-offset-4 col-lg-10">
                <asp:Button runat="server" Text="Save" ValidationGroup="ChangePwdValidationGroup"
                    CssClass="btn btn-primary" CausesValidation="true" ID="btnUpdatePassword" OnClick="btnUpdatePassword_Click">
                </asp:Button>
                <asp:Button runat="server" Text="Cancel" Visible="false" CssClass="btn btn-default"
                    ID="btnCancel"></asp:Button>
            </div>
        </div>
    </div>
</div>
