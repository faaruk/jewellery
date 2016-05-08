<%@ Page Title="Customer Management" EnableSessionState="ReadOnly" Language="C#" AutoEventWireup="true" 
    CodeBehind="--Customers.aspx.cs" MasterPageFile="~/DasbhoardMaster.Master" Inherits="Collaboration.Web.UI.Admin.Customers" Theme="Office2010Silver" %>
<%@ Register Assembly="DevExpress.Web.v14.2, Version=14.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">

<%--<script type="text/javascript">

   
     function uploadError(sender, args) {
         var errmsg = args.get_errorMessage();
         if (errmsg == "")
             $get("<%=dvMessage.ClientID%>").innerHTML = "There was some error in uploading the file";
         else
             $get("<%=dvMessage.ClientID%>").innerHTML = "Error : " + errmsg;

         $get("<%=dvMessage.ClientID%>").style.display = "block";
     }

     function uploadStarted(sender, args) {
         $get("<%=dvMessage.ClientID%>").innerHTML = "";
         $get("<%=dvMessage.ClientID%>").style.display = "none";

         var fileName = args.get_fileName();
         var fileExt = fileName.substring(fileName.lastIndexOf(".") + 1).toLowerCase();
         //var filesizeuploaded = sender._inputFile.files[0].size;

         if (fileExt == "xls" ) {
             return true;
         } else {
             //To cancel the upload, throw an error, it will fire OnClientUploadError
             var err = new Error();
             err.name = "Upload Error";
             err.message = "Please upload only Valid Image files ('.xls') with Image size upto 5 MB";
             throw (err);

             return false;
         }
     }
    </script>--%>




<div class="panel">
    <div class="bio-graph-heading">
        Customers
    </div>    
    <div class="panel-body">
    <!-- page start-->
        <div class="row">

        
            <div id="dvMessageMain" runat="server" style="display: none" class="alert alert-block alert-danger fade in">
                <button data-dismiss="alert" class="close close-sm" type="button">
                    <i class="icon-remove"></i>
                </button>
                <asp:Literal ID="ltMessageMain" runat="server"></asp:Literal>
            </div>
             <div id="dvMessage" runat="server" style="display: none" class="alert alert-block alert-danger fade in">
                                                    <button data-dismiss="alert" class="close close-sm" type="button">
                                                        <i class="icon-remove"></i>
                                                    </button>
                                                    <asp:Literal ID="ltMessage" runat="server"></asp:Literal>
                                                </div>
            <div class="form-group">
                <label class="col-lg-3 col-sm-3 control-label">
                    Upload Customer File</label>
                <div class="col-lg-4">
                    <asp:FileUpload ID="Uploader" runat="server"  />
                  <%-- <ajax:AsyncFileUpload ID="flUploadCustomer" Width="100%" runat="server" CompleteBackColor="Green"
                        UploaderStyle="Traditional" OnClientUploadError="uploadError" ErrorBackColor="Red"
                        OnUploadedComplete="flUploadCustomer_UploadedComplete" UploadingBackColor="#66CCFF"
                        OnClientUploadStarted="uploadStarted" />--%>
                    <asp:RegularExpressionValidator ID="rexp" ValidationGroup="vg" CssClass="red" runat="server" ControlToValidate="Uploader" ErrorMessage="Only .xls files are allowed" ValidationExpression="(.*\.([xX][lL][sS])$)"></asp:RegularExpressionValidator>
                </div>
                <div class="col-lg-4">
                    <asp:Button runat="server" Text="Import" CssClass="btn btn-info" ID="btnImport" OnClick="btnImport_Click" ValidationGroup="vg">
                    </asp:Button>
                </div>
            </div>
        </div>
        <!-- page end-->
    


    <div class="row">
    <dx:ASPxGridView ID="CustomersGrid" runat="server" AutoGenerateColumns="False" DataSourceID="CustomersDataSource" KeyFieldName="CustomerID" Width="100%">
        <Columns>            
            <dx:GridViewDataTextColumn FieldName="CustomerCode" Caption="Code" Width="150px">
            </dx:GridViewDataTextColumn>            
            <dx:GridViewDataTextColumn FieldName="CustomerName" Caption="Customer" Width="300px">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="CustomerEmail" Caption="Email" Width="300px">
            </dx:GridViewDataTextColumn>            
            <dx:GridViewCommandColumn ShowEditButton="true" ShowDeleteButton="true" ShowClearFilterButton="true" Width="100px"></dx:GridViewCommandColumn>
            <dx:GridViewDataColumn>
                <EditItemTemplate></EditItemTemplate>
            </dx:GridViewDataColumn>
        </Columns>
    </dx:ASPxGridView>
    <asp:ObjectDataSource ID="CustomersDataSource" runat="server" TypeName="Collaboration.Business.Components.AdminManager" SelectMethod="GetCustomers" UpdateMethod="ModifyCustomer" DeleteMethod="DeleteCustomer">
        <UpdateParameters>
            <asp:Parameter Name="CustomerID" Type="Int32" />
            <asp:Parameter Name="CustomerCode" Type="String" />
            <asp:Parameter Name="CustomerName" Type="String" />
            <asp:Parameter Name="CustomerEmail" Type="String" />            
        </UpdateParameters>
        <DeleteParameters>
            <asp:Parameter Name="customerID" Type="Int32" />
        </DeleteParameters>
    </asp:ObjectDataSource>
    </div>
    

    </div>
</div>
</asp:Content>
<asp:Content ID="Content3" runat="server" ContentPlaceHolderID="ScriptsPlaceHolder">
</asp:Content>