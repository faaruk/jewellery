<%@ Page Title="" Language="C#" MasterPageFile="~/DasbhoardMaster.Master" AutoEventWireup="true" CodeBehind="ReturnedSamples.aspx.cs" Inherits="Collaboration.Web.UI.Tracking.ReturnedSamples" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="panel">
        <div class="bio-graph-heading">
            Returned Samples
        </div>
        <div class="panel-body">
            <div class="adv-table editable-table ">
            
                <div class="space15">
                </div>
                <asp:UpdatePanel ID="updGrid" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:GridView ID="gvTable" runat="server" AutoGenerateColumns="False" DataKeyNames="SampleTrackID"
                            AllowSorting="True" Width="100%"  OnPageIndexChanging="gvTable_PageIndexChanging"
                            OnSorting="gvTable_Sorting" EmptyDataText="No Recods Found" 
                            CssClass="table table-striped table-hover table-bordered dataTable myTable"
                            OnPreRender="gvTable_PreRender" >
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:TemplateField HeaderText="Sample Serial Number" HeaderStyle-ForeColor="#797979">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSampleSerialNumber" runat="server" Text='<%# Eval("SampleSerialNumber") %>'></asp:Label>
                                       
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Sample Status" HeaderStyle-ForeColor="#797979">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSampleStatusName" runat="server" Text='<%# Eval("SampleStatusName") %>'></asp:Label>
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
                
                </asp:UpdatePanel>
            </div>
            
      
            
        </div>
        
    </div>
</asp:Content>
