<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UC_EditOrder.ascx.cs"
    Inherits="Collaboration.Web.UI.UserControl.UC_EditOrder" %>


<%@ Import Namespace="Collaboration.Web.UI" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Assembly="CodeCarvings.Piczard" Namespace="CodeCarvings.Piczard.Web"
    TagPrefix="ccPiczard" %>
<script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.2/jquery.min.js"></script>
<%--<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.11.0/jquery.min.js"></script>--%>

<%--<script src="../dist/js/lightbox-plus-jquery.min.js"></script>--%>

<link href="../js/chosen/docsupport/style.css" rel="stylesheet" type="text/css" />
<link href="../js/chosen/docsupport/prism.css" rel="stylesheet" type="text/css" />
<link href="../js/chosen/chosen.css" rel="stylesheet" type="text/css" />
<link rel="stylesheet" href="../dist/css/lightbox.css">
<style type="text/css">
    .fileUpload {
        /*width: 255px;*/
        font-size: 11px;
        color: #000000;
        border: solid;
        border-width: 1px;
        border-color: #7f9db9;
        height: auto;
    }

    .imgpopovers {
        background-color: #fff;
        border-color: #41CAC0;
        color: #41CAC0;
        padding: 2px 8px;
        font-weight: bold;
    }

        .imgpopovers:hover {
            background-color: #41CAC0;
            border-color: #41CAC0;
            color: #FFFFFF;
        }
</style>

<link href="../Scripts/dropzone/css/basic.css" rel="stylesheet" />
<link href="../Scripts/dropzone/css/dropzone.css" rel="stylesheet" />
<style type="text/css">
    .dz-max-files-reached {
        background-color: red;
    }
</style>
<script src="../Scripts/dropzone/dropzone.min.js"></script>
<script type="text/javascript">
    //File Upload response from the server
    Dropzone.options.dropzoneForm = {
        acceptedFiles: 'image/*',
        maxFiles: 1,
        url: "hn_SimpeFileUploaderNewOrder.ashx",

        init: function () {
            this.on("maxfilesexceeded", function (data) {
                var res = eval('(' + data.xhr.responseText + ')');

            });
            this.on("addedfile", function (file) {

                // Create the remove button
                var removeButton = Dropzone.createElement("<button>Remove file</button>");


                // Capture the Dropzone instance as closure.
                var _this = this;

                // Listen to the click event
                removeButton.addEventListener("click", function (e) {
                    // Make sure the button click doesn't submit the form:
                    e.preventDefault();
                    e.stopPropagation();
                    // Remove the file preview.
                    _this.removeFile(file);
                    // If you want to the delete the file on the server as well,
                    // you can do the AJAX request here.
                });

                // Add the button to the file preview element.
                file.previewElement.appendChild(removeButton);
            });
        }
    };

    </script>
<script language="javascript" type="text/javascript">

    function AddMoreImages() {
        if (!document.getElementById && !document.createElement)
            return false;
        var fileUploadarea = document.getElementById("fileUploadarea");
        if (!fileUploadarea)
            return false;
        var newLine = document.createElement("br");
        fileUploadarea.appendChild(newLine);
        var newFile = document.createElement("input");
        newFile.type = "file";
        newFile.setAttribute("class", "fileUpload");

        if (!AddMoreImages.lastAssignedId)
            AddMoreImages.lastAssignedId = 100;
        newFile.setAttribute("id", "FileUpload" + AddMoreImages.lastAssignedId);
        newFile.setAttribute("name", "FileUpload" + AddMoreImages.lastAssignedId);
        if (AddMoreImages.lastAssignedId > 104) {
            alert("You can't add more than 6 images with a single puzzle");
            return false;
        }
        var div = document.createElement("div");
        div.appendChild(newFile);
        div.setAttribute("id", "div" + AddMoreImages.lastAssignedId);
        fileUploadarea.appendChild(div);
        AddMoreImages.lastAssignedId++;
    }


</script>
<script type="text/javascript">
    $(document).ready(function () {
        var j = $.noConflict();

        j(document).on("click", ".ajaxRemoveButton", function () {
            var rmButton = j(this);
            var filename = rmButton.parent().find('span.filename').text();
            j.post(document.URL, { ac: "deletefile", fn: filename }).done(function (data) {
                rmButton.parent().remove();
            });
            console.log(filename);
        });

        j('#datepicker').Zebra_DatePicker({
            direction: 1,
            format: '<%=Resource.FormatDateUI%>',

            onSelect: function (dateText, inst) {
                var dateAsString = dateText; //the first parameter of this function                
                j('#MainContent_EditOrder_hdnShippingDate').val(dateAsString);
            }
        });

        //Set date to input
        j("#datepicker").val(j('#MainContent_EditOrder_hdnShippingDate').val());

    });

</script>
<script type="text/javascript">
    function ddltest() {
        var value = $("#MainContent_EditOrder_ddlCustomers").val();

        $("#MainContent_EditOrder_ddlCustomerEmail").val(value);
        var email;

        try {
            email = $("#MainContent_EditOrder_ddlCustomerEmail option:selected").text();
        } catch (e) {
            email = 'Email not available';
        }

        if (email.length > 0) {
            $("#MainContent_EditOrder_txtCustomerEmail").val(email);
        } else {
            $("#MainContent_EditOrder_txtCustomerEmail").val('Email not available');
        }
        /*
        var value = $("#MainContent_EditOrder_ddlCustomers option:selected").val();
        var ok = $('#MainContent_EditOrder_ddlCustomerEmail').eq(parseInt(value)).text();
        var list = document.getElementById("MainContent_EditOrder_ddlCustomerEmail");

        var email = $.trim(list.options[parseInt(value)].text);
        if (email.length > 0) {
           $("#MainContent_EditOrder_txtCustomerEmail").val(email); 
        } else {
            $("#MainContent_EditOrder_txtCustomerEmail").val('Email not available');
        }
        */
    }

    $(function () {
        $("#MainContent_EditOrder_ddlCustomers").change(function () {
            var text = $("#MainContent_EditOrder_ddlCustomers option:selected").text();
            var value = $("#MainContent_EditOrder_ddlCustomers option:selected").val();
            alert("Selected text=" + text + " Selected value= " + value);
        });
    });
</script>
<script type="text/javascript">

    $(document).ready(function () {





        var DB_RingID = ('<% = Collaboration.Web.UI.Resource.DB_RingID%>');
        var DB_PedantID = ('<% = Collaboration.Web.UI.Resource.DB_PedantID%>');
        var DB_EarringsID = ('<% = Collaboration.Web.UI.Resource.DB_EarringsID%>');
        var DB_JacketsID = ('<% = Collaboration.Web.UI.Resource.DB_JacketsID%>');
        var DB_BraceletID = ('<% = Collaboration.Web.UI.Resource.DB_BraceletID%>');
        var DB_NecklaceID = ('<% = Collaboration.Web.UI.Resource.DB_NecklaceID%>');
        var DB_BanglesID = ('<% = Collaboration.Web.UI.Resource.DB_BanglesID%>');
        var DB_ChainID = ('<% = Collaboration.Web.UI.Resource.DB_ChainID%>');
        var DB_SubTypeMountID = ('<% = Collaboration.Web.UI.Resource.DB_SubTypeMountID%>');
        var DB_SubTypeFinishedID = ('<% = Collaboration.Web.UI.Resource.DB_SubTypeFinishedID%>');
        var DB_IsMatchingETID = ('<% = Collaboration.Web.UI.Resource.DB_IsMatchingETID%>');
        var DB_RingMountID = ('<% = Collaboration.Web.UI.Resource.DB_RingMountID%>');
        var DropDown_Others_Text = ('<% = Collaboration.Web.UI.Common.DROPDOWN_OTHERS_VALUE%>');
        HideDropBox();
        HideSelectButton();
        SetMetal();
        SetQuantity();
        SetSamples();
        SetCustomer();
        SetFingerSize();
        SetRingType();
        SetSubType();
        SetExistingModel();
        SetExistingModelNotProvided();
        SetCurveType();
        SetStraightType();
        SetSampleProvided();
        SetSampleNotProvided();
        SetStoneProvided();
        SetStoneNotProvided();
        SetModelType();
        function HideDropBox() {
            $('.ajax__fileupload_dropzone').css('display', 'none');
        }
        function HideSelectButton() {
            var hv = $("[id$=hd]").val();
            if (hv == "1")
                $('.ajax__fileupload').css("display", "none");
        }
        $("[id$=ddlMetals]").change(function () {
            SetMetal();
        });

        function SetMetal() {
            var validator = document.getElementById("<%= txtMetalOtherRequired.ClientID %>");
            if ($("[id$=ddlMetals]").val() == DropDown_Others_Text) {
                $("[id$=divMetalOther]").show();
                ValidatorEnable(validator, true);
            }
            else {
                $("[id$=divMetalOther]").hide();
                ValidatorEnable(validator, false);
            }
        }
        $("[id$=ddlQuantity]").change(function () {
            SetQuantity();
        });

        function SetQuantity() {
            var validator = document.getElementById("<%= txtQuantityOtherRequired.ClientID %>");
            if ($("[id$=ddlQuantity]").val() == DropDown_Others_Text) {
                $("[id$=divQuantityOther]").show();
                ValidatorEnable(validator, true);
            }
            else {
                $("[id$=divQuantityOther]").hide();
                ValidatorEnable(validator, false);
            }
        }
        $("[id$=ddlFingerSize]").change(function () {
            SetFingerSize();
        });

        function SetFingerSize() {
            var validator = document.getElementById("<%= FingerSizeOtherRequired.ClientID %>");
            if ($("[id$=ddlFingerSize]").val() == DropDown_Others_Text) {
                $("[id$=divFingerSizeOther]").show();
                ValidatorEnable(validator, true);
            }
            else {
                $("[id$=divFingerSizeOther]").hide();
                ValidatorEnable(validator, false);
            }
        }

        $("[id$=ddlNoOfSamples]").change(function () {
            SetSamples();
        });

        function SetSamples() {
            var validator = document.getElementById("<%= txtSampleOtherRequired.ClientID %>");
            var rgValidator = document.getElementById("<%= rgNoOfSamples.ClientID %>");
            var rangeNoOfSamples = document.getElementById("<%= rangeNoOfSamples.ClientID %>");


            if ($("[id$=ddlNoOfSamples]").val() == DropDown_Others_Text) {
                $("[id$=divSampleOther]").show();
                ValidatorEnable(validator, true);
                ValidatorEnable(rgValidator, true);
                ValidatorEnable(rangeNoOfSamples, true);
            }
            else {
                $("[id$=divSampleOther]").hide();
                ValidatorEnable(validator, false);
                ValidatorEnable(rgValidator, false);
                ValidatorEnable(rangeNoOfSamples, false);
            }
        }
        /* Customer */
        $("#MainContent_EditOrder_ddlCustomers").change(function () {
            alert();
            SetCustomer();
        });

        function OnCustomerChange() {

            SetCustomer();
        }

        function SetCustomer() {

            $("[id$=ddlCustomerEmail]").val($("[id$=ddlCustomers]").val());
            $("[id$=txtCustomerEmail]").val($("[id$=ddlCustomerEmail] option:selected").text());
        }
        /* End Customer */

        /* ModelType */
        $("[id$=ddlModelTypes]").change(function () {
            $("[id$=ddlModelSubType]").val("0");
            $("[id$=ddlQuantity]").val("0");
            SetModelType();
        });

        function SetModelType() {
            var validatorRingType = document.getElementById("<%= RingTypeRequired.ClientID %>");
            var validatorFingerSize = document.getElementById("<%= FingerSizeRequired.ClientID %>");
            var FingerSizeOtherRequired = document.getElementById("<%= FingerSizeOtherRequired.ClientID %>");
            var LengthRequired = document.getElementById("<%= LengthRequired.ClientID %>");
            var LengthValueRequired = document.getElementById("<%= LengthValueRequired.ClientID %>");
            var LengthValid = document.getElementById("<%= LengthValid.ClientID %>");
            var ModelSubTypeRequired = document.getElementById("<%= ModelSubTypeRequired.ClientID %>");
            var GeneralHeadSizeRequired = document.getElementById("<%= HeadSizeGeneralRequired.ClientID %>");
            var selectedValue = $("[id$=ddlModelTypes]").val();

            $("[id$=divFingerSize]").hide();
            $("[id$=divFingerSizeOther]").hide();

            $("[id$=divRingType]").hide();
            $("[id$=divRingMount]").hide();
            $("[id$=divMatchingET]").hide();

            $("[id$=divModelSubType]").hide();

            $("[id$=divMatchJacketsModel]").hide();
            $("[id$=divHeadSize]").show();
            $("[id$=divLength]").hide();

            $("[id$=ddlQuantity] option[value='1P']").attr("disabled", true);

            //$("[id$=ddlQuantity] option[value='P']").remove();
            $("[id$=divSubTypeDetails]").hide();

            ValidatorEnable(validatorRingType, false);
            ValidatorEnable(validatorFingerSize, false);
            ValidatorEnable(FingerSizeOtherRequired, false);
            ValidatorEnable(LengthRequired, false);
            ValidatorEnable(GeneralHeadSizeRequired, false);
            ValidatorEnable(LengthValueRequired, false);
            ValidatorEnable(LengthValid, false);
            ValidatorEnable(FingerSizeOtherRequired, false);
            ValidatorEnable(ModelSubTypeRequired, false);

            if ($("[id$=ddlModelTypes]").val() != '') {

                if ($("[id$=ddlModelTypes]").val() == DB_RingID) {
                    $("[id$=divFingerSize]").show();
                    ValidatorEnable(validatorFingerSize, true);
                    $("[id$=divRingType]").show();
                    ValidatorEnable(validatorRingType, true);
                }
                else if ($("[id$=ddlModelTypes]").val() == DB_PedantID || $("[id$=ddlModelTypes]").val() == DB_EarringsID || $("[id$=ddlModelTypes]").val() == DB_BraceletID
                    || $("[id$=ddlModelTypes]").val() == DB_NecklaceID) {
                    $("[id$=divModelSubType]").show();
                    ValidatorEnable(ModelSubTypeRequired, true);
                    if ($("[id$=ddlModelTypes]").val() == DB_EarringsID)
                        $("[id$=ddlQuantity] option[value='1P']").attr("disabled", false);
                    if ($("[id$=ddlModelTypes]").val() == DB_BraceletID || $("[id$=ddlModelTypes]").val() == DB_NecklaceID) {
                        $("[id$=divLength]").show();
                        ValidatorEnable(LengthRequired, true);
                        ValidatorEnable(LengthValueRequired, true);
                        ValidatorEnable(LengthValid, true);
                    }
                }
                else if ($("[id$=ddlModelTypes]").val() == DB_JacketsID) {
                    $("[id$=divSubTypeDetails]").show();
                    $("[id$=divMatchJacketsModel]").show();
                    $("[id$=divExistingModelGeneral]").show();
                    ValidatorEnable(GeneralHeadSizeRequired, true);
                    $("[id$=ddlQuantity] option[value='1P']").attr("disabled", false);
                }
                else if ($("[id$=ddlModelTypes]").val() == DB_ChainID) {
                    $("[id$=divSubTypeDetails]").show();
                    $("[id$=divHeadSize]").hide();
                    $("[id$=divLength]").show();
                    $("[id$=divExistingModelGeneral]").hide();
                    ValidatorEnable(LengthRequired, true);
                    ValidatorEnable(LengthValueRequired, true);
                    ValidatorEnable(LengthValid, true);
                }
            }

        }
        /* End ModelType */

        /* Ring Type */
        $("[id$=ddlRingType]").change(function () {
            SetRingType();
        });
        function SetRingType() {
            var HeadSizeModelRequired = document.getElementById("<%= HeadSizeModelRequired.ClientID %>");
            var HeadSizeETRequired = document.getElementById("<%= HeadSizeETRequired.ClientID %>");

            $("[id$=divRingMount]").hide();
            $("[id$=divMatchingET]").hide();
            ValidatorEnable(HeadSizeModelRequired, false);
            ValidatorEnable(HeadSizeETRequired, false);

            $("[id$=divMatchingET]").hide();

            if ($("[id$=ddlRingType]").val() == DB_RingMountID) {
                $("[id$=divRingMount]").show();
                ValidatorEnable(HeadSizeModelRequired, true);
            }
            else if ($("[id$=ddlRingType]").val() == DB_IsMatchingETID) {
                $("[id$=divMatchingET]").show();
                ValidatorEnable(HeadSizeETRequired, true);
            }
        }
        /* End Ring Type */
        $("[id$=ddlModelSubType]").change(function () {
            SetSubType();
        });
        function SetSubType() {
            var GeneralHeadSizeRequired = document.getElementById("<%= HeadSizeGeneralRequired.ClientID %>");
            if ($("[id$=ddlModelSubType]").val() == DB_SubTypeMountID) {
                $("[id$=divSubTypeDetails]").show();
                ValidatorEnable(GeneralHeadSizeRequired, true);
            }
            else {
                $("[id$=divSubTypeDetails]").hide();
                ValidatorEnable(GeneralHeadSizeRequired, false);
            }
            $("[id$=ddlModelSubType]").focus();
        }

        /* Existing Model */

        $("[id$=rdExistingModelYes]").click(function () {
            SetExistingModel();
        });
        function SetExistingModel() {
            if ($("[id$=rdExistingModelYes]").is(":checked")) {
                $("[id$=divPF]").hide();
            }
        }

        $("[id$=rdExistingModelNo]").click(function () {
            SetExistingModelNotProvided();
        });
        function SetExistingModelNotProvided() {
            if ($("[id$=rdExistingModelNo]").is(":checked")) {
                $("[id$=divPF]").show();
            }
        }



        $("[id$=rdCurve]").click(function () {
            SetCurveType();
        });
        function SetCurveType() {
            if ($("[id$=rdCurve]").is(":checked")) {
                $("[id$=divCurveType]").hide();
            }
        }

        $("[id$=rdStraight]").click(function () {
            SetStraightType();
        });
        function SetStraightType() {
            if ($("[id$=rdStraight]").is(":checked")) {
                $("[id$=divCurveType]").hide();
            }
        }
        $("[id$=rdTailor]").click(function () {
            SetTailoredType();
        });
        function SetTailoredType() {
            if ($("[id$=rdTailor]").is(":checked")) {
                $("[id$=divCurveType]").show();
            }
        }


        $("[id$=rdSampleProvidedYes]").click(function () {
            SetSampleProvided();
        });
        function SetSampleProvided() {
            if ($("[id$=rdSampleProvidedYes]").is(":checked")) {
                $("[id$=divNoOfSamples]").show();
            }
        }

        $("[id$=rdSampleProvidedNo]").click(function () {
            SetSampleNotProvided();
        });
        function SetSampleNotProvided() {
            if ($("[id$=rdSampleProvidedNo]").is(":checked")) {
                $("[id$=divNoOfSamples]").hide();
            }
        }

        $("[id$=rdStoneProvidedYes]").click(function () {
            SetStoneProvided();
        });
        function SetStoneProvided() {
            var StoneDescriptionRequired = document.getElementById("<%= StoneDescriptionRequired.ClientID %>");
            var SettingInstructionsRequired = document.getElementById("<%= SettingInstructionsRequired.ClientID %>");

            if ($("[id$=rdStoneProvidedYes]").is(":checked")) {

                $("[id$=divStoneProvided]").show();
                ValidatorEnable(StoneDescriptionRequired, true);
                ValidatorEnable(SettingInstructionsRequired, true);
            }
        }

        $("[id$=rdStoneProvidedNo]").click(function () {
            SetStoneNotProvided();
        });




        function SetStoneNotProvided() {
            var StoneDescriptionRequired = document.getElementById("<%= StoneDescriptionRequired.ClientID %>");
            var SettingInstructionsRequired = document.getElementById("<%= SettingInstructionsRequired.ClientID %>");
            if ($("[id$=rdStoneProvidedNo]").is(":checked")) {
                ValidatorEnable(StoneDescriptionRequired, false);
                ValidatorEnable(SettingInstructionsRequired, false);
                $("[id$=divStoneProvided]").hide();
            }
        }


    });
</script>
<script type="text/javascript">

    $(".ajax__fileupload_dropzone").css("visibility", "hidden");

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
        var filesizeuploaded = sender._inputFile.files[0].size;

        if ((fileExt == "jpeg" || fileExt == "jpg" || fileExt == "png" || fileExt == "gif") && (filesizeuploaded <= 5242880)) {
            return true;
        } else {
            //To cancel the upload, throw an error, it will fire OnClientUploadError
            var err = new Error();
            err.name = "Upload Error";
            err.message = "Please upload only Valid Image files (.jpg,.jpeg,'.gif','.png') with Image size upto 5 MB";
            throw (err);

            return false;
        }
    }
    function uploadComplete(sender, e) {
        if (sender._filesInQueue[sender._filesInQueue.length - 1]._isUploaded) {
            var btn1 = document.getElementById("<%= btn1.ClientID %>");
            //btn1.click();
        }
        //$(".removeButton").css("visibility", "visible");
        $(".removeButton").each(function () {
            if (!$(this).parent().find(".ajaxRemoveButton").length) {

                $('<div class="ajaxRemoveButton">Remove</div>').insertAfter($(this));
            }
        });
        //$(".removeButton").addClass("ajaxRemoveButton").removeClass("removeButton");

    }


</script>
<div class="row">
    <div class="col-lg-6">
        <div class="panel">
            <div class="subHeading alert-info fade in">
                General Settings
            </div>
            <div class="panel-body">
                <div id="dvMessage" runat="server" style="display: none" class="alert alert-block alert-danger fade in">
                    <button data-dismiss="alert" class="close close-sm" type="button">
                        <i class="icon-remove"></i>
                    </button>
                    <asp:Literal ID="ltMessage" runat="server"></asp:Literal>
                </div>
                <div class="form-horizontal custome_form_stl" role="form">
                    <div class="form-group">
                        <div id="divSerialNumber" runat="server" visible="false">
                            <label class="col-lg-3 col-sm-3 control-label">
                                Serial Number</label>
                            <div class="col-lg-8">
                                <asp:Label ID="lblSerialNumber" runat="server"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-lg-3 col-sm-3 control-label">
                            Customer Name
                        </label>
                        <div class="col-lg-8">
                            <%--<asp:TextBox type="text" runat="server" ID="txtCustomer" class="form-control" placeholder="Customer Name or Code"></asp:TextBox>
                            <ajax:AutoCompleteExtender ID="aceCustomer" runat="server" EnableCaching="true" TargetControlID="txtCustomer"
                                CompletionSetCount="10" CompletionInterval="100" ServicePath="~/Orders/CreateOrder.aspx"
                                ServiceMethod="GetCustomers" MinimumPrefixLength="1" OnClientItemSelected="OnCustomerSelected"
                                CompletionListCssClass="autocomplete_completionListElement" CompletionListItemCssClass="autocomplete_listItem"
                                CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem">
                            </ajax:AutoCompleteExtender>--%>
                            <asp:DropDownList ID="ddlCustomers" runat="server" CssClass="form-control m-bot15 chosen-select"
                                onchange="ddltest();">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator Display="Dynamic" ID="CustomerRequired"
                                runat="server" ControlToValidate="ddlCustomers" CssClass="failureNotification"
                                ForeColor="Red" ErrorMessage="This field is required" InitialValue="" ValidationGroup="CreateOrderValidation"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-lg-3 col-sm-3 control-label">
                            Customer Email
                        </label>
                        <div class="col-lg-8">
                            <input type="text" class="form-control" runat="server" id="txtCustomerEmail" placeholder="Customer Email"
                                readonly="readonly" />
                            <asp:DropDownList Enabled="false" Style="display: none" ID="ddlCustomerEmail" runat="server"
                                DataTextField="CustomerEmail" CssClass="form-control m-bot15" DataValueField="CustomerId">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-lg-3 col-sm-3 control-label">
                            Expected Shipping Date
                        </label>
                        <div class="col-lg-8">
                            <asp:TextBox ID="hdnShippingDate" runat="server" CssClass="hidden" />
                            <input type="text" id="datepicker" />
                            <asp:RequiredFieldValidator Display="Dynamic" ID="hdnShippingDateValidator"
                                runat="server" ControlToValidate="hdnShippingDate" CssClass="failureNotification"
                                ForeColor="Red" ErrorMessage="This field is required" InitialValue="" ValidationGroup="CreateOrderValidation"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-lg-3 col-sm-3 control-label">
                            Model Type
                        </label>
                        <div class="col-lg-8">
                            <asp:DropDownList ID="ddlModelTypes" runat="server" DataTextField="ModelCode" CssClass="form-control m-bot15"
                                DataValueField="ModelTypeID">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator Display="Dynamic" ID="ModelTypeRequired"
                                runat="server" ControlToValidate="ddlModelTypes" CssClass="failureNotification"
                                ForeColor="Red" ErrorMessage="This field is required" InitialValue="" ValidationGroup="CreateOrderValidation"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-lg-3 col-sm-3 control-label">
                            Model Number</label>
                        <div class="col-lg-8">
                            <asp:TextBox ID="txtModelNumber" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator Display="Dynamic" ID="ModelNumberRequired"
                                runat="server" ControlToValidate="txtModelNumber" CssClass="failureNotification"
                                ForeColor="Red" ErrorMessage="This field is required" InitialValue="" ValidationGroup="CreateOrderValidation"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-lg-3 col-sm-3 control-label">
                            Quantity
                        </label>
                        <div class="col-lg-8">
                            <asp:DropDownList ID="ddlQuantity" runat="server" CssClass="form-control m-bot15"
                                DataTextField="Text" DataValueField="Value">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldQuantity"
                                runat="server" ControlToValidate="ddlQuantity" CssClass="failureNotification"
                                ForeColor="Red" ErrorMessage="This field is required" InitialValue="" ValidationGroup="CreateOrderValidation"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="form-group" id="divQuantityOther" style="display: none;">
                        <label class="col-lg-3 col-sm-3 control-label">
                            Add Quantity
                        </label>
                        <div class="col-lg-8">
                            <asp:TextBox CssClass="form-control" ID="txtQuantityOther" runat="server" MaxLength="20"></asp:TextBox>
                            <asp:RequiredFieldValidator Display="Dynamic" ID="txtQuantityOtherRequired"
                                runat="server" ControlToValidate="txtQuantityOther" CssClass="failureNotification"
                                Enabled="false" ForeColor="Red" ErrorMessage="This field is required" InitialValue=""
                                ValidationGroup="CreateOrderValidation"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-lg-3 col-sm-3 control-label">
                            Process Type
                        </label>
                        <div class="col-lg-8">
                            <asp:DropDownList ID="ddlProcessTypes" runat="server" DataTextField="Type" CssClass="form-control m-bot15"
                                DataValueField="ProcessTypeID">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator Display="Dynamic" ID="ProcessTypesRequired"
                                runat="server" ControlToValidate="ddlProcessTypes" CssClass="failureNotification"
                                ForeColor="Red" ErrorMessage="This field is required" InitialValue="" ValidationGroup="CreateOrderValidation"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-lg-3 col-sm-3 control-label">
                            Priority
                        </label>
                        <div class="col-lg-8">
                            <asp:DropDownList ID="ddlPriority" runat="server" DataTextField="Name" CssClass="form-control m-bot15"
                                DataValueField="PriorityID">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator Display="Dynamic" ID="PriorityRequired"
                                runat="server" ControlToValidate="ddlPriority" CssClass="failureNotification"
                                ForeColor="Red" ErrorMessage="This field is required" InitialValue="" ValidationGroup="CreateOrderValidation"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-lg-3 col-sm-3 control-label">
                            Metal
                        </label>
                        <div class="col-lg-8">
                            <asp:DropDownList ID="ddlMetals" runat="server" DataTextField="MetalName" CssClass="form-control m-bot15"
                                DataValueField="MetalID">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator Display="Dynamic" ID="MetalsRequired"
                                runat="server" ControlToValidate="ddlMetals" CssClass="failureNotification" ForeColor="Red"
                                ErrorMessage="This field is required" InitialValue="" ValidationGroup="CreateOrderValidation"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="form-group" id="divMetalOther" style="display: none;">
                        <label class="col-lg-3 col-sm-3 control-label">
                            Add Metal
                        </label>
                        <div class="col-lg-8">
                            <asp:TextBox CssClass="form-control" ID="txtMetalOther" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator Display="Dynamic" ID="txtMetalOtherRequired"
                                runat="server" ControlToValidate="txtMetalOther" CssClass="failureNotification"
                                Enabled="false" ForeColor="Red" ErrorMessage="This field is required" InitialValue=""
                                ValidationGroup="CreateOrderValidation"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="form-group" id="divFingerSize" style="display: none;">
                        <label class="col-lg-3 col-sm-3 control-label">
                            Finger Size <span class="label label-default">RX</span>
                        </label>
                        <div class="col-lg-8">
                            <asp:DropDownList ID="ddlFingerSize" runat="server" DataTextField="Size" CssClass="form-control m-bot15"
                                DataValueField="FingerSizeID">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator Display="Dynamic" ID="FingerSizeRequired"
                                runat="server" ControlToValidate="ddlFingerSize" CssClass="failureNotification"
                                Enabled="false" ForeColor="Red" ErrorMessage="This field is required" InitialValue=""
                                ValidationGroup="CreateOrderValidation"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="form-group" id="divFingerSizeOther" style="display: none;">
                        <label class="col-lg-3 col-sm-3 control-label">
                            Add Finger Size
                        </label>
                        <div class="col-lg-8">
                            <asp:TextBox CssClass="form-control" ID="txtFingerSizeOther" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator Display="Dynamic" ID="FingerSizeOtherRequired"
                                runat="server" ControlToValidate="txtFingerSizeOther" CssClass="failureNotification"
                                Enabled="false" ForeColor="Red" ErrorMessage="This field is required" InitialValue=""
                                ValidationGroup="CreateOrderValidation"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="form-group length" style="display: none; margin-bottom: 0px;" id="divLength">
                        <label for="inputEmail1" class="col-lg-3 col-sm-3 control-label">
                            Length
                        </label>
                        <div class="col-lg-4">
                            <asp:TextBox ID="txtLength" runat="server" CssClass="form-control" MaxLength="6"></asp:TextBox>
                            <asp:RequiredFieldValidator Display="Dynamic" ID="LengthValueRequired"
                                runat="server" ControlToValidate="txtLength" CssClass="failureNotification" Enabled="false"
                                ForeColor="Red" ErrorMessage="This field is required" InitialValue="" ValidationGroup="CreateOrderValidation"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator Display="Dynamic" ID="LengthValid" runat="server"
                                ErrorMessage="Please enter valid Length" ValidationGroup="CreateOrderValidation"
                                ControlToValidate="txtLength" CssClass="failureNotification" ForeColor="Red"
                                InitialValue="" ValidationExpression="^\d*\.?\d*$"></asp:RegularExpressionValidator>
                        </div>
                        <div class="col-lg-4">
                            <asp:DropDownList ID="ddlLength" runat="server" CssClass="form-control m-bot15" DataTextField="Text"
                                DataValueField="Value">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator Display="Dynamic" ID="LengthRequired"
                                runat="server" ControlToValidate="ddlLength" CssClass="failureNotification" Enabled="false"
                                ForeColor="Red" ErrorMessage="This field is required" InitialValue="" ValidationGroup="CreateOrderValidation"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="exampleInputFile" class="col-lg-3 col-sm-3 control-label">
                            Add Image Instructions</label>
                        <div class="col-lg-8">

                            <ajax:AjaxFileUpload ID="specimenFileUpload" runat="server" AllowedFileTypes="jpg,jpeg,png,gif"
                                OnUploadComplete="File_Upload" OnClientUploadComplete="uploadComplete" Style="display: none" />
                            <div>
                                <div id="fileUploadarea" runat="server">
                                    <asp:FileUpload ID="fuPuzzleImage" runat="server" CssClass="fileUpload" Style="display: none" /><br />
                                    <input type="file" class="multi {accept:'gif|jpg', max:3, STRING:{ remove:'Remover', selected:'Selecionado: $file', denied:'Invalid $ext!', duplicate:'Duplicate:\n$file!' }}" style="display: none" />
                                    <div class="jumbotron">
                                        <div class="dropzone" id="dropzoneForm">
                                            <div class="fallback">
                                                <input name="file" type="file" multiple />
                                                <input type="submit" value="Upload" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div style="display: none">
                                    <input style="display: block;" id="btnAddMoreFiles" type="button" value="Add more images" onclick="AddMoreImages();" /><br />
                                    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Upload" Style="display: none" />

                                </div>

                            </div>
                            <div class="alert alert-success alert-block fade in" id="lblFiles" runat="server"
                                visible="false">
                            </div>
                            <asp:Button ID="btn1" runat="server" OnClick="btnAddCAD_Click" Style="display: none;" />
                            <asp:HiddenField ID="hd" runat="server" />
                            <div id="divChangeInstructions" style="width: 100%; overflow-y: auto; overflow-x: auto; word-break: break-all;" runat="server">
                                <asp:DataList ID="dlImages" runat="server" RepeatColumns="3" RepeatDirection="Horizontal"
                                    BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px"
                                    OnItemDataBound="dlImages_RowDataBound" CellPadding="2" Visible="false">
                                    <ItemTemplate>
                                        <table border="0" style="border-bottom-color: #60BAEA; border-top-color: #60BAEA; border-left-color: #60BAEA; border-left-color: #60BAEA;"
                                            cellspacing="5">
                                            <tr>
                                                <td align="center">
                                                    <asp:HyperLink rel="img-group" CssClass="img-popup" runat="server" ID="imgSpecimenLink">
                                                        <asp:Image ID="imgSpecimen" runat="server" align="center" BorderStyle="Solid" BorderColor="#e0ddd7"
                                                            BorderWidth="2" Height="120" />
                                                    </asp:HyperLink>
                                                    <asp:Literal runat="server" ID="DowmloadImgLink"></asp:Literal>


                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                </asp:DataList>
                            </div>
                        </div>
                    </div>

                    <div class="form-group">
                        <label class="col-lg-4 col-sm-4 control-label" style="padding-top: 0px;">
                            Make exact copy of photos
                        </label>
                        <div class="col-lg-8">
                            <div class="radios" style="padding-top: 8px;">
                                <label class="label_radio r_on col-lg-3 col-sm-2" for="radio-01" style="padding-bottom: 0px;">
                                    <asp:RadioButton Text="Yes" ID="rdExactCopiedYes" runat="server" Checked="false"
                                        ClientIDMode="Static" GroupName="grpExactCopies" />
                                </label>
                                <label class="label_radio r_off col-lg-3 col-sm-2" for="radio-02" style="padding-bottom: 0px;">
                                    <asp:RadioButton Text="No" ID="rdExactCopiedNo" runat="server" Checked="true" GroupName="grpExactCopies"
                                        ClientIDMode="Static" />
                                </label>
                            </div>
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
                Advance Settings
            </div>
            <div class="panel-body">
                <div class="form-horizontal">
                    <div class="form-group" id="divRingType" style="display: none; margin-bottom: 10px;">
                        <label for="inputEmail1" class="col-lg-4 col-sm-4 control-label">
                            Ring Type <span class="label label-default">RX</span></label>
                        <div class="col-lg-8">
                            <asp:DropDownList ID="ddlRingType" runat="server" DataTextField="Type" CssClass="form-control m-bot15"
                                DataValueField="RingTypeID">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator Display="Dynamic" ID="RingTypeRequired"
                                runat="server" ControlToValidate="ddlRingType" CssClass="failureNotification"
                                Enabled="false" ForeColor="Red" ErrorMessage="This field is required" InitialValue=""
                                ValidationGroup="CreateOrderValidation"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="form-group" id="divModelSubType" style="display: none;">
                        <label for="inputEmail1" class="col-lg-4 col-sm-4 control-label">
                            <label id="lblSubTypeName">
                            </label>
                            Type</label>
                        <div class="col-lg-8">
                            <asp:DropDownList ID="ddlModelSubType" runat="server" CssClass="form-control m-bot15"
                                DataTextField="Text" DataValueField="Value">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator Display="Dynamic" ID="ModelSubTypeRequired"
                                runat="server" ControlToValidate="ddlModelSubType" CssClass="failureNotification"
                                Enabled="false" ForeColor="Red" ErrorMessage="This field is required" InitialValue=""
                                ValidationGroup="CreateOrderValidation"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="clear" style="margin-bottom: 15px;">
                    </div>
                    <div class="form-group" id="divSubTypeDetails" style="display: none;">
                        <div id="divHeadSize">
                            <label for="inputEmail1" class="col-lg-4 col-sm-2 control-label">
                                Head size</label>
                            <label class="col-lg-8" style="margin-bottom: 10px;">
                                <asp:TextBox runat="server" ID="txtHeadSizeGeneral" MaxLength="100" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator Display="Dynamic" ID="HeadSizeGeneralRequired"
                                    runat="server" ControlToValidate="txtHeadSizeGeneral" CssClass="failureNotification"
                                    Enabled="false" ForeColor="Red" ErrorMessage="This field is required" InitialValue=""
                                    ValidationGroup="CreateOrderValidation"></asp:RequiredFieldValidator>
                            </label>
                        </div>
                        <div class="clear">
                        </div>
                        <div id="divExistingModelGeneral">
                            <label class="col-lg-4 col-sm-4 control-label">
                                Existing Model</label>
                            <div class="col-lg-8">
                                <div class="radios" style="padding-top: 8px;">
                                    <label class="label_radio r_on col-lg-2 col-sm-2" for="radio-01" style="padding-bottom: 0px;">
                                        <asp:RadioButton Text="Yes" ID="rdExistingModelGeneralYes" runat="server" Checked="true"
                                            ClientIDMode="Static" GroupName="grpExistingModelGeneral" />
                                    </label>
                                    <label class="label_radio r_off col-lg-2 col-sm-2" for="radio-02" style="padding-bottom: 0px;">
                                        <asp:RadioButton Text="No" ID="rdExistingModelGeneralNo" runat="server" Checked="false"
                                            ClientIDMode="Static" GroupName="grpExistingModelGeneral" />
                                    </label>
                                </div>
                            </div>
                        </div>
                        <div class="clear">
                        </div>
                        <div id="divMatchJacketsModel" style="display: none;">
                            <label for="inputEmail1" class="col-lg-4 col-sm-2 control-label">
                                Match Earrings Model</label>
                            <label class="col-lg-8" style="margin-bottom: 10px;">
                                <asp:TextBox runat="server" ID="txtMatchJacketModel" MaxLength="2" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator Display="Dynamic" ID="MatchJacketModelRequired"
                                    runat="server" ControlToValidate="txtMatchJacketModel" CssClass="failureNotification"
                                    Enabled="false" ForeColor="Red" ErrorMessage="This field is required" InitialValue=""
                                    ValidationGroup="CreateOrderValidation"></asp:RequiredFieldValidator>
                            </label>
                        </div>
                        <div class="clear">
                        </div>
                        <label for="inputEmail1" class="col-lg-4 col-sm-4 control-label">
                            Additonal Info</label>
                        <div class="col-lg-8">
                            <textarea runat="server" id="txtAdditionalInfoGeneral" class="form-control" cols="60"
                                rows="3"></textarea>
                        </div>
                        <div class="clear">
                        </div>
                    </div>
                    <div class="form-group" id="divRingMount" style="display: none;">
                        <label for="inputEmail1" class="col-lg-4 col-sm-2 control-label" style="margin-bottom: 20px;">
                            Head size</label>
                        <label class="col-lg-8">
                            <asp:TextBox runat="server" ID="txtHeadSizeModel" MaxLength="20" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator Display="Dynamic" ID="HeadSizeModelRequired"
                                runat="server" ControlToValidate="txtHeadSizeModel" CssClass="failureNotification"
                                Enabled="false" ForeColor="Red" ErrorMessage="This field is required" InitialValue=""
                                ValidationGroup="CreateOrderValidation"></asp:RequiredFieldValidator>
                        </label>
                        <div class="clear">
                        </div>
                        <label class="col-lg-4 col-sm-4 control-label">
                            Existing Model</label>
                        <div class="col-lg-8">
                            <div class="radios" style="padding-top: 8px;">
                                <label class="label_radio r_on col-lg-2 col-sm-2" for="radio-01" style="padding-bottom: 0px;">
                                    <asp:RadioButton Text="Yes" ID="rdExistingModelYes" runat="server" Checked="true"
                                        ClientIDMode="Static" GroupName="grpExistingModel" />
                                </label>
                                <label class="label_radio r_off col-lg-2 col-sm-2" for="radio-02" style="padding-bottom: 0px;">
                                    <asp:RadioButton Text="No" ID="rdExistingModelNo" runat="server" Checked="false"
                                        ClientIDMode="Static" GroupName="grpExistingModel" />
                                </label>
                            </div>
                        </div>
                        <div class="clear">
                        </div>
                        <div id="divPF" style="display: none; margin-bottom: 20px;">
                            <label class="col-lg-4 col-sm-2 control-label">
                                PF</label>
                            <div class="col-lg-8">
                                <div class="radios" style="padding-top: 8px;">
                                    <label class="label_radio r_on col-lg-2 col-sm-2" for="radio-01" style="padding-bottom: 0px;">
                                        <asp:RadioButton Text="Yes" ID="rdPFYes" runat="server" Checked="true" ClientIDMode="Static"
                                            GroupName="grpPF" />
                                    </label>
                                    <label class="label_radio r_off col-lg-2 col-sm-2" for="radio-02" style="padding-bottom: 0px;">
                                        <asp:RadioButton Text="No" ID="rdPFNo" runat="server" Checked="false" ClientIDMode="Static"
                                            GroupName="grpPF" />
                                    </label>
                                </div>
                            </div>
                        </div>
                        <div class="clear">
                        </div>
                        <label class="col-lg-4 col-sm-4 control-label">
                            Additonal Info</label>
                        <div class="col-lg-8">
                            <textarea runat="server" id="txtAdditionalInfoModel" class="form-control" cols="60"
                                rows="3"></textarea>
                        </div>
                    </div>
                    <!--Chain End-->
                    <div class="form-group" id="divMatchingET" style="display: none;">
                        <label class="col-lg-4 col-sm-4 control-label" style="margin-bottom: 15px;">
                            Model Number</label>
                        <div class="col-lg-8" style="margin-bottom: 15px;">
                            <asp:TextBox ID="txtModelNumberToMatch" class="form-control" runat="server"></asp:TextBox>
                        </div>
                        <div class="clear">
                        </div>
                        <label class="col-lg-4 col-sm-2 control-label">
                            Head size</label>
                        <label class="col-lg-8" style="margin-bottom: 10px;">
                            <asp:TextBox runat="server" ID="txtHeadSizeET" MaxLength="20" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator Display="Dynamic" ID="HeadSizeETRequired"
                                runat="server" ControlToValidate="txtHeadSizeET" CssClass="failureNotification"
                                Enabled="false" ForeColor="Red" ErrorMessage="This field is required" InitialValue=""
                                ValidationGroup="CreateOrderValidation"></asp:RequiredFieldValidator>
                        </label>
                        <div class="clear">
                        </div>
                        <label for="inputEmail1" class="col-lg-4 col-sm-4 control-label" style="margin-bottom: 15px; margin-top: 10px;">
                            Finish at the same point</label>
                        <div class="col-lg-8">
                            <div class="radios" style="padding-top: 8px;">
                                <label class="label_radio r_on col-lg-2 col-sm-2" for="radio-01" style="padding-bottom: 0px;">
                                    <asp:RadioButton Text="Yes" ID="rdFinishYes" runat="server" Checked="false" ClientIDMode="Static"
                                        GroupName="grpFinish" />
                                </label>
                                <label class="label_radio r_off col-lg-2 col-sm-2" for="radio-02" style="padding-bottom: 0px;">
                                    <asp:RadioButton Text="No" ID="rdFinishNo" runat="server" Checked="true" GroupName="grpFinish"
                                        ClientIDMode="Static" />
                                    <asp:CheckBox Visible="false" ID="chkFinishAtSomePoint" runat="server" Style="margin-left: 0px; margin-top: 10px;" />
                                </label>
                            </div>
                        </div>

                        <label for="inputEmail1" class="col-lg-4 col-sm-4 control-label">
                            Match</label>
                        <div class="radios" style="padding-top: 8px;">
                            <%--<a class="example-image-link" href="../img/Straight.jpg"
                                data-lightbox="example-1" data-title="Straight">--%>
                            <label class="label_radio r_on col-lg-2 col-sm-2" for="radio-01" style="padding-bottom: 0px;">
                                <asp:RadioButton ID="rdStraight" runat="server" Text="Straight" Checked="true" GroupName="rdCurve"
                                    ClientIDMode="Static" onclick="displayDate()" />
                            </label>
                            <%--</a>--%>
                            <%--<a class="example-image-link" href="../img/Curved.jpg"
                                data-lightbox="example-2" data-title="Curve">--%>
                            <label class="label_radio r_on col-lg-2 col-sm-2" for="radio-02" style="padding-bottom: 0px;">
                                <asp:RadioButton ID="rdCurve" runat="server" Text="Curve" Checked="false" GroupName="rdCurve"
                                    ClientIDMode="Static" />
                            </label>
                            <%--</a>--%>

                            <label class="label_radio r_on col-lg-2 col-sm-2" for="radio-02" style="padding-bottom: 0px;">
                                <asp:RadioButton ID="rdTailor" runat="server" Text="Tailored" Checked="false" GroupName="rdCurve"
                                    ClientIDMode="Static" />
                            </label>

                        </div>

                        <label class="col-lg-4 col-sm-4 control-label">&nbsp;</label>
                        <div class="col-lg-8" style="margin-bottom: 10px;">
                            <label class="label_radio r_on col-lg-2 col-sm-2" for="radio-01" style="padding-bottom: 0px; padding-left: 0px;">
                                <button data-img="../img/Straight.jpg" data-placement="right" data-trigger="hover"
                                    class="btn btn-round btn-info imgpopovers" onclick="Javascript:return false;">
                                    ?</button>
                            </label>
                            <label class="label_radio r_on col-lg-3 col-sm-2" for="radio-02" style="padding-bottom: 0px; text-align: center;">
                                <button data-img="../img/Curved.jpg" data-placement="right" data-trigger="hover"
                                    class="btn  btn-round small btn-info imgpopovers" onclick="Javascript:return false;">
                                    ?</button>
                            </label>
                        </div>
                        <div class="clear">
                        </div>
                        <div id="divCurveType" style="display: none; margin-bottom: 15px; padding-top: 12px;">
                            <label class="col-lg-4 col-sm-4 control-label">
                                Curve Type</label>
                            <div class="col-lg-8">
                                <div class="radios" style="padding-top: 8px;">
                                    <label class="label_radio r_on col-lg-6 col-sm-4" for="radio-01" style="padding-bottom: 0px; padding-left: 0px;">
                                        <asp:RadioButton ID="rdFollowMountShape" runat="server" Text="Follows the shape of mount"
                                            Checked="true" GroupName="rdCurveType" ClientIDMode="Static" />

                                    </label>
                                    <label class="label_radio r_off col-lg-6 col-sm-4" for="radio-02" style="padding-bottom: 0px;">
                                        <asp:RadioButton ID="rdCurveToFit" runat="server" Text="Curve to Fit" GroupName="rdCurveType"
                                            ClientIDMode="Static" />

                                    </label>
                                </div>
                            </div>
                            <label class="col-lg-4 col-sm-4 control-label">&nbsp;</label>
                            <div class="col-lg-8" style="margin-bottom: 10px;">
                                <label class="label_radio r_on col-lg-6 col-sm-4" for="radio-01" style="padding-bottom: 0px; padding-left: 0px;">
                                    <button data-img="../img/Follows.jpg" data-placement="right" data-trigger="hover"
                                        class="btn btn-round btn-info imgpopovers" onclick="Javascript:return false;">
                                        ?</button>
                                </label>
                                <label class="label_radio r_off col-lg-6 col-sm-4" for="radio-02" style="padding-bottom: 0px;">
                                    <button data-img="../img/Curve to Fit.jpg" data-placement="right" data-trigger="hover"
                                        class="btn  btn-round small btn-info imgpopovers" onclick="Javascript:return false;">
                                        ?</button>
                                </label>

                            </div>
                        </div>
                        <div class="clear">
                        </div>
                        <label class="col-lg-4 col-sm-4 control-label">
                            Additonal Info</label>
                        <div class="col-lg-8">
                            <textarea runat="server" id="txtAdditionalInfoET" class="form-control" cols="60"
                                rows="3"></textarea>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-lg-4 col-sm-4 control-label">
                            CAD Requested
                            <button id="btnQuestionSymbol" data-content="Has customer requested CAD? If customer has not requested CAD, you can approve without reverting to customer."
                                data-placement="right" data-trigger="hover" class="btn btn-round btn-info popovers"
                                onclick="Javascript:return false;">
                                ?</button>
                        </label>
                        <div class="col-lg-8">
                            <div class="radios" style="padding-top: 8px;">
                                <label class="label_radio r_on col-lg-2 col-sm-2" for="radio-01" style="padding-bottom: 0px;">
                                    <asp:RadioButton Text="Yes" ID="rdCadRequestedYes" runat="server" Checked="false"
                                        ClientIDMode="Static" GroupName="grpCADrequested" />
                                </label>
                                <label class="label_radio r_off col-lg-2 col-sm-2" for="radio-02" style="padding-bottom: 0px;">
                                    <asp:RadioButton Text="No" ID="rdCadRequestedNo" runat="server" Checked="true" GroupName="grpCADrequested"
                                        ClientIDMode="Static" />
                                </label>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-lg-4 col-sm-4 control-label" style="padding-top: 0px;">
                            Sample Provided</label>
                        <div class="col-lg-8">
                            <div class="radios" style="padding-top: 8px;">
                                <label class="label_radio r_on col-lg-2 col-sm-2" for="radio-01" style="padding-bottom: 0px;">
                                    <asp:RadioButton Text="Yes" ID="rdSampleProvidedYes" runat="server" Checked="false"
                                        ClientIDMode="Static" GroupName="grpSampleProvided" />
                                </label>
                                <label class="label_radio r_off col-lg-2 col-sm-2" for="radio-02" style="padding-bottom: 0px;">
                                    <asp:RadioButton Text="No" ID="rdSampleProvidedNo" runat="server" Checked="true"
                                        ClientIDMode="Static" GroupName="grpSampleProvided" />
                                </label>
                            </div>
                        </div>
                    </div>
                    <div class="form-group" id="divNoOfSamples" style="display: none;">
                        <label class="col-lg-4 col-sm-4 control-label">
                            Make exact copy of samples
                        </label>
                        <div class="col-lg-8">
                            <div class="radios" style="padding-top: 8px;">
                                <label class="label_radio r_on col-lg-2 col-sm-2" for="radio-01" style="padding-bottom: 0px;">
                                    <asp:RadioButton Text="Yes" ID="rdExactCopiedSampleYes" runat="server" Checked="false"
                                        ClientIDMode="Static" GroupName="grpExactCopiesSample" />
                                </label>
                                <label class="label_radio r_off col-lg-2 col-sm-2" for="radio-02" style="padding-bottom: 0px;">
                                    <asp:RadioButton Text="No" ID="rdExactCopiedSampleNo" runat="server" Checked="true"
                                        GroupName="grpExactCopiesSample" ClientIDMode="Static" />
                                </label>
                            </div>
                        </div>
                        <label for="inputEmail1" class="col-lg-4 col-sm-4 control-label">
                            No of Samples</label>
                        <div class="col-lg-8">
                            <asp:DropDownList ID="ddlNoOfSamples" runat="server" CssClass="form-control m-bot15"
                                DataTextField="Text" DataValueField="Value">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group" id="divSampleOther" style="display: none;" runat="server">
                        <label class="col-lg-4 col-sm-2 control-label">
                            Add No Of Samples
                        </label>
                        <div class="col-lg-8">
                            <asp:TextBox CssClass="form-control" ID="txtSampleOther" runat="server" MaxLength="2"></asp:TextBox>
                            <asp:RequiredFieldValidator Display="Dynamic" ID="txtSampleOtherRequired"
                                runat="server" ControlToValidate="txtSampleOther" CssClass="failureNotification"
                                Enabled="false" ForeColor="Red" ErrorMessage="This field is required" InitialValue=""
                                ValidationGroup="CreateOrderValidation"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator Display="Dynamic" ID="rgNoOfSamples" runat="server"
                                ErrorMessage="Please enter only numeric No of Samples" ValidationGroup="CreateOrderValidation"
                                ControlToValidate="txtSampleOther" CssClass="failureNotification" ForeColor="Red"
                                InitialValue="" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                            <asp:RangeValidator Display="Dynamic" ID="rangeNoOfSamples" runat="server" ErrorMessage="Please enter No of Samples between 1-25"
                                ValidationGroup="CreateOrderValidation" ControlToValidate="txtSampleOther" CssClass="failureNotification"
                                ForeColor="Red" Type="Integer" InitialValue="" MinimumValue="1"
                                MaximumValue="25"></asp:RangeValidator>
                        </div>
                    </div>
                    <div class="form-group" id="divSampleSerialNumber" runat="server" visible="false">
                        <label class="col-lg-4 col-sm-4 control-label">
                            Sample Serial Number</label>
                        <div class="col-lg-8">
                            <asp:Label ID="lblSampleSerialNumber" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-lg-4 col-sm-4 control-label">
                            Stones Provided</label>
                        <div class="col-lg-8">
                            <div class="radios" style="padding-top: 8px;">
                                <label class="label_radio r_on col-lg-2 col-sm-2" for="radio-01" style="padding-bottom: 0px;">
                                    <asp:RadioButton Text="Yes" ID="rdStoneProvidedYes" runat="server" Checked="false"
                                        ClientIDMode="Static" GroupName="grpStoneProvided" />
                                </label>
                                <label class="label_radio r_off col-lg-2 col-sm-2" for="radio-02" style="padding-bottom: 0px;">
                                    <asp:RadioButton Text="No" ID="rdStoneProvidedNo" runat="server" Checked="true" ClientIDMode="Static"
                                        GroupName="grpStoneProvided" />
                                </label>
                            </div>
                        </div>
                    </div>
                    <div class="form-group" id="divStoneProvided" style="display: none;">
                        <label for="inputEmail1" class="col-lg-4 col-sm-2 control-label" style="margin-bottom: 20px;">
                            Stone Description</label>
                        <label class="col-lg-8">
                            <asp:TextBox runat="server" ID="txtStoneDescription" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator Display="Dynamic" ID="StoneDescriptionRequired"
                                runat="server" ControlToValidate="txtStoneDescription" CssClass="failureNotification"
                                Enabled="false" ForeColor="Red" ErrorMessage="This field is required" InitialValue=""
                                ValidationGroup="CreateOrderValidation"></asp:RequiredFieldValidator>
                        </label>
                        <div class="clear">
                        </div>
                        <label for="inputEmail1" class="col-lg-4 col-sm-2 control-label" style="margin-bottom: 20px;">
                            Setting Instructions</label>
                        <label class="col-lg-8">
                            <asp:TextBox runat="server" ID="txtSettingInstructions" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator Display="Dynamic" ID="SettingInstructionsRequired"
                                runat="server" ControlToValidate="txtSettingInstructions" CssClass="failureNotification"
                                Enabled="false" ForeColor="Red" ErrorMessage="This field is required" InitialValue=""
                                ValidationGroup="CreateOrderValidation"></asp:RequiredFieldValidator>
                        </label>
                    </div>
                </div>
            </div>
        </div>
        <div class="panel">
            <div class="panel-body">
                <div class="form-horizontal" role="form">
                    <div id="dvTeamMember" class="form-group" runat="server">
                        <label class="col-lg-4 col-sm-4 control-label">
                            Team Member</label>
                        <div class="col-lg-8">
                            <asp:DropDownList ID="ddlTemaMembers" runat="server" DataTextField="UserName" CssClass="form-control m-bot15"
                                DataValueField="UserID">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator Display="Dynamic" ID="TeamMembersRequired"
                                runat="server" ControlToValidate="ddlTemaMembers" CssClass="failureNotification"
                                ForeColor="Red" ErrorMessage="This field is required" InitialValue="" ValidationGroup="CreateOrderValidation"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-lg-4 col-sm-4 control-label">
                            Assignee
                        </label>
                        <div class="col-lg-8">
                            <asp:DropDownList ID="ddlAssignee" runat="server" DataTextField="UserName" CssClass="form-control m-bot15"
                                DataValueField="UserID">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator Display="Dynamic" ID="AssigneeRequired"
                                runat="server" ControlToValidate="ddlAssignee" CssClass="failureNotification"
                                ForeColor="Red" ErrorMessage="This field is required" InitialValue="" ValidationGroup="CreateOrderValidation"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-lg-4 col-sm-4 control-label">
                            Remarks</label>
                        <div class="col-lg-8">
                            <textarea class="form-control" cols="60" rows="3" id="txtRemarks" runat="server"></textarea>
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
                  
                </div>
                <div class="modal-footer">
                    <asp:Button ClientIDMode="Static" data-dismiss="modal" runat="server" ID="btnCancel"
                        Text="Close" CssClass="btn btn-default"></asp:Button>
                </div>
            </div>
        </div>
    </asp:Panel>
    <asp:Label Text="" ID="lblDummy" runat="server"></asp:Label>--%>
    <%--  <ccPiczard:PopupPictureTrimmer ID="ppt" runat="server" 
       OnPopupClose="PopupPictureTrimmer_PopupClose" AutoPostBackOnPopupClose="OnlyOnSave" ShowImageAdjustmentsPanel="true"  />--%>
    <%--  <asp:Button ID="btnTest" runat="server" Text="Test" onclick="btnTest_Click" />--%>
    <script src="../js/chosen/chosen.jquery.js" type="text/javascript"></script>
    <script src="../js/chosen/docsupport/prism.js" type="text/javascript"></script>
    <script type="text/javascript">
        var config = {
            '.chosen-select': {},
            '.chosen-select-deselect': { allow_single_deselect: true },
            '.chosen-select-no-single': { disable_search_threshold: 10 },
            '.chosen-select-no-results': { no_results_text: 'Oops, nothing found!' },
            '.chosen-select-width': { width: "95%" }
        }
        for (var selector in config) {
            $(selector).chosen(config[selector]);
        }

    </script>
    <script type="text/javascript">

        //      $("#MainContent_EditOrder_ddlCustomers_chosen").chosen().change(function () {
        //          alert('SaiRam');
        //      });

    </script>
    <script type="text/javascript">
        function openFcustom(imgUrl) {
            jQuery.fancybox.open([
             {
                 href: imgUrl
             }
            ], {
                padding: 0
            });
        }


        $('.imgpopovers').click(function () {
            var dataImg = $(this).data('img');
            openFcustom(dataImg);
            return false;
        });
    </script>
</div>
