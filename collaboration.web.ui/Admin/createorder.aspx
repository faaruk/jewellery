<%@ Page Title="" Language="C#" MasterPageFile="~/DasbhoardMaster.Master" AutoEventWireup="true"
    CodeBehind="createorder.aspx.cs" Inherits="Collaboration.Web.UI.Messages.createorder" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.2/jquery.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#types').change(function () {
                if ($('#types').val() == 'Other') {
                    $('#other').show();
                }
                else {
                    $('#other').hide();
                }
            });
        });     
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#quantity').change(function () {
                if ($('#quantity').val() == 'Other') {
                    $('#quanityOther').show();
                }
                else {
                    $('#quanityOther').hide();
                }
            });
        });
    </script>
      <script type="text/javascript">
          $(document).ready(function () {
              $('#quantity1').change(function () {
                  if ($('#quantity1').val() == 'Other') {
                      $('#quanityOther').show();
                  }
                  else {
                      $('#quanityOther').hide();
                  }
              });
          });
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#fingersize').change(function () {
                if ($('#fingersize').val() == 'Other') {
                    $('#fingersizeOther').show();
                }
                else {
                    $('#fingersizeOther').hide();
                }
            });
        });
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#NoOfSamples').change(function () {
                if ($('#NoOfSamples').val() == 'Other') {
                    $('#divSampleOther').show();
                }
                else {
                    $('#divSampleOther').hide();
                }
            });
        });
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#model').change(function () {
                $('#Match_no').hide();
                if ($('#model').val() === 'RX') {
                    $('#ring_type').show();
                }
                else {
                    $('#ring_type').hide();
                }
            });
        });     
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#model').change(function () {
                if ($('#model').val() === 'PX') {
                    $('#pendant_type').show();
                }
                else {
                    $('#pendant_type').hide();
                }
            });
        });     
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#model').change(function () {
                if ($('#model').val() === 'EX') {
                    $('#earring_type').show();
                }
                else {
                    $('#earring_type').hide();
                }
            });
        });     
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#model').change(function () {
                if ($('#model').val() === 'BX') {
                    $('#bracelets_type').show();
                }
                else {
                    $('#bracelets_type').hide();
                }
            });
        });     
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#model').change(function () {
                if ($('#model').val() === 'NX') {
                    $('#necklace_type').show();
                }
                else {
                    $('#necklace_type').hide();
                }
            });
        });     
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#model').change(function () {
                if ($('#model').val() === 'JX') {
                    $('#jackets_type').show();
                }
                else {

                    $('#jackets_type').hide();
                }
            });
        });     
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#model').change(function () {
                if ($('#model').val() === 'CX') {
                    $('#chain_type').show();
                }
                else {

                    $('#chain_type').hide();
                }
            });
        });     
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('.models').change(function () {

                if ($('.models').val() === 'CX' || $('.models').val() === 'BX' || $('.models').val() === 'BX1') {
                    $('.length').show();
                }
                else {

                    $('.length').hide();
                }
            });
        });     
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('.models').change(function () {

                if ($('.models').val() === 'RX' || $('.models').val() === 'PX' || $('.models').val() === 'BX' || $('.models').val() === 'BX1' || $('.models').val() === 'NX' || $('.models').val() === 'CX') {
                    $('#item').show();
                }
                else {

                    $('#item').hide();
                }
            });
        });     
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('.models').change(function () {

                if ($('.models').val() === 'EX' || $('.models').val() === 'JX') {
                    $('#piece').show();
                }
                else {

                    $('#piece').hide();
                }
            });
        });     
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#mount_type').change(function () {
                if ($('#mount_type').val() === 'mount') {
                    $('#pf_chk').show();
                }
                else {
                    $('#pf_chk').hide();
                }
            });
        });     
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#p_mount_type').change(function () {
                if ($('#p_mount_type').val() === 'p-mount') {
                    $('#p_option').show();
                }
                else {
                    $('#p_option').hide();
                }
            });
        });     
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#e_mount_type').change(function () {
                if ($('#e_mount_type').val() === 'e-mount') {
                    $('#e_option').show();
                }
                else {
                    $('#e_option').hide();
                }
            });
        });     
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#b_mount_type').change(function () {
                if ($('#b_mount_type').val() === 'b-mount') {
                    $('#b_option').show();
                }
                else {
                    $('#b_option').hide();
                }
            });
        });     
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#n_mount_type').change(function () {
                if ($('#n_mount_type').val() === 'n-mount') {
                    $('#n_option').show();
                }
                else {
                    $('#n_option').hide();
                }
            });
        });     
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#mount_type').change(function () {
                if ($('#mount_type').val() === 'match') {
                    $('#Match_no').show();
                }
                else {
                    $('#Match_no').hide();
                }
            });
        });     
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('input[type="radio"]').click(function () {
                if ($(this).attr('id') == 'existingModelYes') {
                    $("#divPF").hide();
                }
            });
        });
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('input[type="radio"]').click(function () {
                if ($(this).attr('id') == 'existingModelNo') {
                    $("#divPF").show();
                }
            });
        });
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('input[type="radio"]').click(function () {
                if ($(this).attr('id') == 'StoneProvidedYes') {
                    $("#divStone").show();
                }
            });
        });
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('input[type="radio"]').click(function () {
                if ($(this).attr('id') == 'StoneProvidedNo') {
                    $("#divStone").hide();
                }
            });
        });
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('input[type="radio"]').click(function () {
                if ($(this).attr('id') == 'watch-me' || $(this).attr('id') == 'Curve1' || $(this).attr('id') == 'Curve2') {
                    $('#show-me').show();
                }

                else {
                    $('#show-me').hide();
                }
            });
        });
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('input[type="radio"]').click(function () {
                if ($(this).attr('id') == 'SampleProvidedYes') {
                    $('#sample-me').show();
                }
            });
        });
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('input[type="radio"]').click(function () {
                if ($(this).attr('id') == 'SampleProvidedNo') {
                    $('#sample-me').hide();
                }
            });
        });
    </script>
    <script type="text/javascript">

        $(document).ready(function () {
            function setring_size(model, process) {
                if (model == 'RX') {
                    $('#RX').show();
                }
                else {
                    $('#RX').hide();
                }
            }
            $('#model').change(function () {
                setring_size($(this).val(), $('#process').val());
            });

        });     
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <!--Left Side-->
    <div class="panel">
        <div class="bio-graph-heading">
            Create Order
        </div>
        <div class="panel-body">
            <div class="row">
                <div class="col-lg-6">
                    <div class="panel">
                        <div class="bio-graph-heading" style="color: #468847; background-color: #dff0d8;
                            font-size: 14px;">
                            General Setting
                        </div>
                        <div class="panel-body">
                            <div class="form-horizontal custome_form_stl" role="form">
                                <div class="form-group">
                                    <label for="inputEmail1" class="col-lg-4 col-sm-4 control-label">
                                        Customer Name
                                    </label>
                                    <div class="col-lg-8">
                                        <select class="form-control m-bot15" placeholder="Customer Name" style="margin-bottom: 0px;">
                                            <option selected="selected">--Select--</option>
                                            <option>Peter Born</option>
                                            <option>Steven Jhon</option>
                                            <option>Mack Jason</option>
                                        </select>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="inputPassword1" class="col-lg-4 col-sm-4 control-label">
                                        Customer Email
                                    </label>
                                    <div class="col-lg-8">
                                        <input type="text" class="form-control" id="Password1" placeholder="Customer Email"
                                            readonly="readonly">
                                    </div>
                                </div>
                                <%--<div class="form-group">
                                      <label for="inputPassword1" class="col-lg-3 col-sm-3 control-label">Serial Number</label>
                                      <div class="col-lg-8">
                                          <input type="text" class="form-control" id="Password2" placeholder="" readonly="readonly">
                                          
                                      </div>
                                  </div>--%>
                                <div class="form-group">
                                    <label for="inputEmail1" class="col-lg-4 col-sm-4 control-label">
                                        Model Type
                                    </label>
                                    <div class="col-lg-8">
                                        <select class="form-control m-bot15 models" placeholder="Customer Name" style="margin-bottom: 0px;"
                                            id="model" name="model">
                                            <option selected="selected">--Select--</option>
                                            <option value="RX">Rings (RX)</option>
                                            <option value="PX">Pendants (PX)</option>
                                            <option value="EX">Earrings (EX)</option>
                                            <option value="JX">Jackets (JX)</option>
                                            <option value="BX">Bracelets (BX)</option>
                                            <option value="BX1">Bangles (BX)</option>
                                            <option value="NX">Necklaces (NX)</option>
                                            <option value="CX">Chains (CX)</option>
                                        </select>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="inputPassword1" class="col-lg-4 col-sm-4 control-label">
                                        Model Number</label>
                                    <div class="col-lg-8">
                                        <input type="text" class="form-control" id="Password3" placeholder="Model Number">
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="inputEmail1" class="col-lg-4 col-sm-4 control-label">
                                        Process Type
                                    </label>
                                    <div class="col-lg-8">
                                        <select class="form-control m-bot15" placeholder="Customer Name" style="margin-bottom: 0px;"
                                            id="process" name="process">
                                            <option selected="selected">--Select--</option>
                                            <option>X</option>
                                            <option value="NSM">NSM</option>
                                        </select>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="inputEmail1" class="col-lg-4 col-sm-4 control-label">
                                        Priority
                                    </label>
                                    <div class="col-lg-8">
                                        <select class="form-control m-bot15" placeholder="Customer Name" style="margin-bottom: 0px;">
                                            <option selected="selected">--Select--</option>
                                            <option>Urgent (U)</option>
                                            <option>Quite Urgent (Q)</option>
                                            <option>Normal (N)</option>
                                        </select>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="inputEmail1" class="col-lg-4 col-sm-4 control-label">
                                        Metal
                                    </label>
                                    <div class="col-lg-8">
                                        <select class="form-control m-bot15" placeholder="Customer Name" style="margin-bottom: 0px;"
                                            id="types" name="types">
                                            <option selected="selected">--Select--</option>
                                            <option>PT Cob</option>
                                            <option>18k White</option>
                                            <option>18k Yellow</option>
                                            <option>18k Rose</option>
                                            <option>18k TT (YG) - Yellow Shank, White Head</option>
                                            <option>18k TT (RG) - Rose Shank, White Head</option>
                                            <option>Wax First</option>
                                            <option value="Other">Other</option>
                                        </select>
                                    </div>
                                </div>
                                <div class="form-group" id="other" style="display: none;" name="other">
                                    <label for="inputEmail1" class="col-lg-4 col-sm-4 control-label">
                                        Add Metal
                                    </label>
                                    <div class="col-lg-8">
                                        <input type="text" class="form-control" id="Password5" placeholder="Add Metal">
                                    </div>
                                </div>
                                <div class="form-group" id="RX" style="display: none;">
                                    <label for="inputEmail1" class="col-lg-4 col-sm-4 control-label">
                                        Finger Size <span class="label label-default">RX</span></label>
                                    <div class="col-lg-8">
                                        <select class="form-control m-bot15" placeholder="Customer Name" style="margin-bottom: 0px;"
                                            id="fingersize">
                                            <option selected="selected">--Select--</option>
                                            <option>E</option>
                                            <option>E 1/2</option>
                                            <option>F</option>
                                            <option>F 1/2</option>
                                            <option>G</option>
                                            <option>G 1/2</option>
                                            <option>H</option>
                                            <option>H 1/2</option>
                                            <option>I</option>
                                            <option>I 1/2</option>
                                            <option>J</option>
                                            <option>J 1/2</option>
                                            <option>K</option>
                                            <option>K 1/2</option>
                                            <option>L</option>
                                            <option>L 1/2</option>
                                            <option>M</option>
                                            <option>M 1/2</option>
                                            <option>N</option>
                                            <option>N 1/2</option>
                                            <option>O</option>
                                            <option>O 1/2</option>
                                            <option>P</option>
                                            <option>P 1/2</option>
                                            <option>Q</option>
                                            <option>Q 1/2</option>
                                            <option>R</option>
                                            <option>R 1/2</option>
                                            <option>S</option>
                                            <option>S 1/2</option>
                                            <option>T</option>
                                            <option>T 1/2</option>
                                            <option>U</option>
                                            <option>U 1/2</option>
                                            <option>V</option>
                                            <option>V 1/2</option>
                                            <option>W</option>
                                            <option>W 1/2</option>
                                            <option>X</option>
                                            <option>X 1/2</option>
                                            <option>Y</option>
                                            <option>Y 1/2</option>
                                            <option>Z</option>
                                            <option>Z 1/2</option>
                                            <option>Z+1</option>
                                            <option>Z+2</option>
                                            <option>Z+3</option>
                                            <option>Z+4</option>
                                            <option>Z+5</option>
                                            <option>Z+6</option>
                                            <option value="Other">Other</option>
                                        </select>
                                    </div>
                                </div>
                                <div class="form-group" id="fingersizeOther" style="display: none;">
                                    <label class="col-lg-4 col-sm-4 control-label">
                                        Add Finger Size
                                    </label>
                                    <div class="col-lg-8">
                                        <input type="text" class="form-control" id="TextFingerSize" placeholder="" value="">
                                    </div>
                                </div>
                                <div class="form-group length" style="display: none; margin-bottom: 0px;" name="length">
                                    <label for="inputEmail1" class="col-lg-4 col-sm-4 control-label">
                                        Length
                                    </label>
                                    <div class="col-lg-4">
                                        <input type="text" class="form-control" id="Text1" placeholder="" value="">
                                    </div>
                                    <div class="col-lg-4">
                                        <select class="form-control m-bot15" placeholder="Length">
                                            <option selected="selected">--Select--</option>
                                            <option>CM</option>
                                            <option>Inch</option>
                                        </select>
                                    </div>
                                </div>
                                <div class="form-group" style="margin-bottom: 0px; display: none;" id="item">
                                    <label for="inputEmail1" class="col-lg-4 col-sm-4 control-label">
                                        Quantity</label>
                                    <div class="col-lg-8">
                                        <select class="form-control m-bot15" placeholder="Quantity" id="quantity">
                                            <option selected="selected">--Select--</option>
                                            <option>1</option>
                                            <option>2</option>
                                            <option>3</option>
                                            <option>4</option>
                                            <option>5</option>
                                            <option>6</option>
                                            <option value="Other">Other</option>
                                        </select>
                                    </div>
                                </div>
                                <div class="form-group" style="margin-bottom: 0px; display: none;" id="piece">
                                    <label for="inputEmail1" class="col-lg-4 col-sm-4 control-label">
                                        Quantity</label>
                                    <div class="col-lg-8">
                                        <select class="form-control m-bot15" placeholder="" id="quantity1">
                                            <option selected="selected">--Select--</option>
                                            <option>1</option>
                                            <option>2</option>
                                            <option>3</option>
                                            <option>4</option>
                                            <option>5</option>
                                            <option>6</option>
                                            <option>1 piece only</option>
                                            <option value="Other">Other</option>
                                        </select>
                                    </div>
                                </div>
                                <div class="form-group" id="quanityOther" style="display: none;">
                                    <label class="col-lg-4 col-sm-4 control-label">
                                        Add Quantity
                                    </label>
                                    <div class="col-lg-8">
                                        <input type="text" class="form-control" id="TextFingerSize" placeholder="" value="">
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="exampleInputFile" class="col-lg-4 col-sm-4 control-label">
                                        Image Instructions</label>
                                    <div class="col-lg-3">
                                        <input type="file" id="exampleInputFile">
                                    </div>
                                    <div class="col-lg-5">
                                        <div class="btn-group">
                                            <a href="javascript:;" class="btn mini btn-primary" style="padding: 0px 7px;"><i
                                                class="icon-plus"><span style="font-family: 'Open Sans', sans-serif;">&nbsp; Add more
                                                    Images</span> </i></a>
                                        </div>
                                        <%-- <span class="badge badge-sm label-danger">Add more than Images</span>--%>
                                        <%--<button data-content="Add more than Images" data-placement="right" data-trigger="hover" class="btn btn-round btn-danger popovers" style="padding:0px 7px;">?</button>--%>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-lg-4 col-sm-4 control-label">
                                        Make exact copy of photos
                                    </label>
                                    <div class="col-lg-8" style="margin-left: -15px;">
                                        <label class="label_radio r_on col-lg-2 col-sm-2" for="radio-01" style="padding-bottom: 0px;
                                            font-weight: normal!important;">
                                            <input name="sample-radio1" value="1" type="radio" class="curve_sel1">
                                            Yes
                                        </label>
                                        <label class="label_radio r_off col-lg-2 col-sm-2" for="radio-02" style="padding-bottom: 0px;
                                            font-weight: normal!important;">
                                            <input name="sample-radio1" value="1" type="radio">
                                            No
                                        </label>
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
                        <div class="bio-graph-heading" style="color: #468847; background-color: #dff0d8;
                            font-size: 14px;">
                            Advance Setting
                        </div>
                        <div class="panel-body">
                            <div class="form-horizontal" role="form">
                                <div class="form-group" id="ring_type" style="display: none; margin-bottom: 10px;"
                                    name="ring_type">
                                    <label for="inputEmail1" class="col-lg-4 col-sm-4 control-label">
                                        Ring Type</label>
                                    <div class="col-lg-8">
                                        <select class="form-control m-bot15" placeholder="Customer Name" style="margin-bottom: 0px;"
                                            id="mount_type" name="mount_type">
                                            <option selected="selected">--Select--</option>
                                            <option>Dress Ring</option>
                                            <option value="match">Matching ET</option>
                                            <option value="mount">Ring Mount</option>
                                        </select>
                                    </div>
                                </div>
                                <div class="form-group" id="pendant_type" style="display: none;" name="pendant_type">
                                    <label for="inputEmail1" class="col-lg-4 col-sm-4 control-label">
                                        Pendant Type</label>
                                    <div class="col-lg-8">
                                        <select class="form-control m-bot15" placeholder="Customer Name" style="margin-bottom: 0px;"
                                            id="p_mount_type" name="p_mount_type">
                                            <option selected="selected">--Select--</option>
                                            <option>Finished</option>
                                            <option value="p-mount">Mount</option>
                                        </select>
                                    </div>
                                    <div class="clear" style="margin-bottom: 15px;">
                                    </div>
                                    <!--Pendant Mount-->
                                    <div id="p_option" style="display: none;" name="p_option">
                                        <label for="inputEmail1" class="col-lg-4 col-sm-2 control-label">
                                            Head size</label>
                                        <label class="col-lg-8" style="margin-bottom: 10px;">
                                            <input type="text" class="form-control" placeholder="" value="">
                                        </label>
                                        <div class="clear">
                                        </div>
                                        <label for="inputEmail1" class="col-lg-4 col-sm-4 control-label">
                                            Existing Model</label>
                                        <div class="radios" style="padding-top: 8px;">
                                            <label class="label_radio r_on col-lg-2 col-sm-2" for="radio-01" style="padding-bottom: 0px;">
                                                <input name="sample-radio1" value="1" type="radio" class="curve_sel1" />
                                                Yes
                                            </label>
                                            <label class="label_radio r_off col-lg-2 col-sm-2" for="radio-02" style="padding-bottom: 0px;">
                                                <input name="sample-radio1" value="1" type="radio" />
                                                No
                                            </label>
                                        </div>
                                        <div class="clear">
                                        </div>
                                        <label for="inputEmail1" class="col-lg-4 col-sm-4 control-label">
                                            Additonal Info</label>
                                        <div class="col-lg-8">
                                            <textarea class="form-control" cols="60" rows="3"></textarea>
                                        </div>
                                    </div>
                                    <!--Pendant Mount End-->
                                </div>
                                <div class="form-group" id="earring_type" style="display: none;" name="earring_type">
                                    <label for="inputEmail1" class="col-lg-4 col-sm-4 control-label">
                                        Earring Type</label>
                                    <div class="col-lg-8">
                                        <select class="form-control m-bot15" placeholder="Earring Type" style="margin-bottom: 0px;"
                                            id="e_mount_type" name="e_mount_type">
                                            <option selected="selected">--Select--</option>
                                            <option>Finished</option>
                                            <option value="e-mount">Mount</option>
                                        </select>
                                    </div>
                                    <div class="clear" style="margin-bottom: 15px;">
                                    </div>
                                    <!--Earring Mount-->
                                    <div id="e_option" style="display: none;" name="e_option">
                                        <label for="inputEmail1" class="col-lg-4 col-sm-2 control-label">
                                            Head size</label>
                                        <label class="col-lg-8" style="margin-bottom: 10px;">
                                            <input type="text" class="form-control" placeholder="" value="">
                                        </label>
                                        <div class="clear">
                                        </div>
                                        <label for="inputEmail1" class="col-lg-4 col-sm-4 control-label">
                                            Existing Model</label>
                                        <div class="radios" style="padding-top: 8px;">
                                            <label class="label_radio r_on col-lg-2 col-sm-2" for="radio-01" style="padding-bottom: 0px;">
                                                <input name="sample-radio1" value="1" type="radio" class="curve_sel1" />
                                                Yes
                                            </label>
                                            <label class="label_radio r_off col-lg-2 col-sm-2" for="radio-02" style="padding-bottom: 0px;">
                                                <input name="sample-radio1" value="1" type="radio" />
                                                No
                                            </label>
                                        </div>
                                        <div class="clear">
                                        </div>
                                        <label for="inputEmail1" class="col-lg-4 col-sm-4 control-label">
                                            Additonal Info</label>
                                        <div class="col-lg-8">
                                            <textarea class="form-control" cols="60" rows="3"></textarea>
                                        </div>
                                    </div>
                                    <!--Earring Mount End-->
                                </div>
                                <div class="form-group" id="bracelets_type" style="display: none;" name="bracelets_type">
                                    <label for="inputEmail1" class="col-lg-4 col-sm-4 control-label">
                                        Bracelets Type</label>
                                    <div class="col-lg-8">
                                        <select class="form-control m-bot15" placeholder="Bracelets Type" style="margin-bottom: 0px;"
                                            id="b_mount_type" name="b_mount_type">
                                            <option selected="selected">--Select--</option>
                                            <option>Finished</option>
                                            <option value="b-mount">Mount</option>
                                        </select>
                                    </div>
                                    <div class="clear" style="margin-bottom: 15px;">
                                    </div>
                                    <!--Bracelet Mount-->
                                    <div id="b_option" style="display: none;" name="e_option">
                                        <label for="inputEmail1" class="col-lg-4 col-sm-2 control-label">
                                            Head size</label>
                                        <label class="col-lg-8" style="margin-bottom: 10px;">
                                            <input type="text" class="form-control" placeholder="" value="">
                                        </label>
                                        <div class="clear">
                                        </div>
                                        <label for="inputEmail1" class="col-lg-4 col-sm-4 control-label">
                                            Existing Model</label>
                                        <div class="radios" style="padding-top: 8px;">
                                            <label class="label_radio r_on col-lg-2 col-sm-2" for="radio-01" style="padding-bottom: 0px;">
                                                <input name="sample-radio1" value="1" type="radio" class="curve_sel1" />
                                                Yes
                                            </label>
                                            <label class="label_radio r_off col-lg-2 col-sm-2" for="radio-02" style="padding-bottom: 0px;">
                                                <input name="sample-radio1" value="1" type="radio" />
                                                No
                                            </label>
                                        </div>
                                        <div class="clear">
                                        </div>
                                        <label for="inputEmail1" class="col-lg-4 col-sm-4 control-label">
                                            Additonal Info</label>
                                        <div class="col-lg-8">
                                            <textarea class="form-control" cols="60" rows="3" style="margin-bottom: 15px;"></textarea>
                                        </div>
                                        <%--<label for="inputEmail1" class="col-lg-4 col-sm-2 control-label">Bracelets Length</label>
                                  <label class="col-lg-8" style="margin-bottom:10px;">
                                  <input type="text" class="form-control" placeholder="" value="">
                                  </label>
                                        --%>
                                    </div>
                                    <!--Bracelet Mount End-->
                                </div>
                                <div class="form-group" id="necklace_type" style="display: none;" name="necklace_type">
                                    <label for="inputEmail1" class="col-lg-4 col-sm-4 control-label">
                                        Necklace Type</label>
                                    <div class="col-lg-8">
                                        <select class="form-control m-bot15" placeholder="Necklace Type" style="margin-bottom: 0px;"
                                            id="n_mount_type" name="n_mount_type">
                                            <option selected="selected">--Select--</option>
                                            <option>Finished</option>
                                            <option value="n-mount">Mount</option>
                                        </select>
                                    </div>
                                    <div class="clear" style="margin-bottom: 15px;">
                                    </div>
                                    <!--Necklace Mount-->
                                    <div id="n_option" style="display: none;" name="e_option">
                                        <label for="inputEmail1" class="col-lg-4 col-sm-2 control-label">
                                            Head size</label>
                                        <label class="col-lg-8" style="margin-bottom: 10px;">
                                            <input type="text" class="form-control" placeholder="" value="">
                                        </label>
                                        <div class="clear">
                                        </div>
                                        <label for="inputEmail1" class="col-lg-4 col-sm-4 control-label">
                                            Existing Model</label>
                                        <div class="radios" style="padding-top: 8px;">
                                            <label class="label_radio r_on col-lg-2 col-sm-2" for="radio-01" style="padding-bottom: 0px;">
                                                <input name="sample-radio1" value="1" type="radio" class="curve_sel1" />
                                                Yes
                                            </label>
                                            <label class="label_radio r_off col-lg-2 col-sm-2" for="radio-02" style="padding-bottom: 0px;">
                                                <input name="sample-radio1" value="1" type="radio" />
                                                No
                                            </label>
                                        </div>
                                        <div class="clear">
                                        </div>
                                        <label for="inputEmail1" class="col-lg-4 col-sm-4 control-label">
                                            Additonal Info</label>
                                        <div class="col-lg-8">
                                            <textarea class="form-control" cols="60" rows="3"></textarea>
                                        </div>
                                    </div>
                                    <!--Necklace Mount End-->
                                </div>
                                <div class="form-group" id="pf_chk" style="display: none;" name="pf_chk">
                                    <label for="inputEmail1" class="col-lg-4 col-sm-2 control-label" style="margin-bottom: 20px;">
                                        Head size</label>
                                    <label class="col-lg-8">
                                        <input type="text" class="form-control" placeholder="" value="">
                                    </label>
                                    <div class="clear">
                                    </div>
                                    <label for="inputEmail1" class="col-lg-4 col-sm-4 control-label">
                                        Existing Model</label>
                                    <label class="label_radio r_on col-lg-2 col-sm-2" for="radio-01" style="padding-bottom: 0px;">
                                        <input name="sample-radio1" value="1" type="radio" id="existingModelYes" class="curve_sel1" />
                                        Yes
                                    </label>
                                    <label class="label_radio r_off col-lg-2 col-sm-2" for="radio-02" style="padding-bottom: 0px;">
                                        <input name="sample-radio1" id="existingModelNo" value="1" type="radio" />
                                        No
                                    </label>
                                    <div class="clear">
                                    </div>
                                    <div id="divPF" style="display: none; margin-bottom: 20px;">
                                        <label for="inputEmail1" class="col-lg-4 col-sm-2 control-label">
                                            PF</label>
                                        <div class="radios" style="padding-top: 8px;">
                                            <label class="label_radio r_on col-lg-2 col-sm-2" for="radio-01" style="padding-bottom: 0px;">
                                                <input name="sample-radio1" value="1" type="radio" class="curve_sel1" />
                                                Yes
                                            </label>
                                            <label class="label_radio r_off col-lg-2 col-sm-2" for="radio-02" style="padding-bottom: 0px;">
                                                <input name="sample-radio1" value="1" type="radio" />
                                                No
                                            </label>
                                        </div>
                                    </div>
                                    <div class="clear">
                                    </div>
                                    <label for="inputEmail1" class="col-lg-4 col-sm-4 control-label">
                                        Additonal Info</label>
                                    <div class="col-lg-8">
                                        <textarea class="form-control" cols="60" rows="3"></textarea>
                                    </div>
                                </div>
                                <!--Jackets Mount-->
                                <div class="form-group" id="jackets_type" style="display: none;" name="jackets_type">
                                    <label for="inputEmail1" class="col-lg-4 col-sm-2 control-label">
                                        Head size</label>
                                    <label class="col-lg-8" style="margin-bottom: 10px;">
                                        <input type="text" class="form-control" placeholder="" value="" />
                                    </label>
                                    <div class="clear">
                                    </div>
                                    <label for="inputEmail1" class="col-lg-4 col-sm-4 control-label">
                                        Existing Model</label>
                                    <label class="label_radio r_on col-lg-2 col-sm-2" for="radio-01" style="padding-bottom: 0px;">
                                        <input name="e-radio1" value="1" type="radio" class="curve_sel1" />
                                        Yes
                                    </label>
                                    <label class="label_radio r_off col-lg-2 col-sm-2" for="radio-02" style="padding-bottom: 0px;">
                                        <input name="e-radio1" value="1" type="radio" />
                                        No
                                    </label>
                                    <div class="clear">
                                    </div>
                                    <label for="inputEmail1" class="col-lg-4 col-sm-2 control-label">
                                        Match Earring Model</label>
                                    <label class="col-lg-8" style="margin-bottom: 10px;">
                                        <input type="text" class="form-control" placeholder="" value="">
                                    </label>
                                    <label for="inputEmail1" class="col-lg-4 col-sm-4 control-label">
                                        Additonal Info</label>
                                    <div class="col-lg-8">
                                        <textarea class="form-control" cols="60" rows="3"></textarea>
                                    </div>
                                </div>
                                <!--Jackets End-->
                                <!--Chain Mount-->
                                <div class="form-group" id="chain_type" style="display: none;" name="chain_type">
                                    <label for="inputEmail1" class="col-lg-4 col-sm-4 control-label">
                                        Additonal Info</label>
                                    <div class="col-lg-8">
                                        <textarea class="form-control" cols="60" rows="3" style="margin-bottom: 15px;"></textarea>
                                    </div>
                                    <%--<label for="inputEmail1" class="col-lg-4 col-sm-2 control-label">Chain Length</label>
                                  <label class="col-lg-8" style="margin-bottom:10px;">
                                  <input type="text" class="form-control" placeholder="" value="">
                                  </label>--%>
                                </div>
                                <!--Chain End-->
                                <div class="form-group" id="Match_no" style="display: none;" name="Match_no">
                                    <label for="inputEmail1" class="col-lg-4 col-sm-4 control-label" style="margin-bottom: 15px;">
                                        Model Number</label>
                                    <div class="col-lg-8" style="margin-bottom: 15px;">
                                        <input type="text" class="form-control" id="Password6" placeholder="Model Number" />
                                    </div>
                                    <div class="clear">
                                    </div>
                                    <label for="inputEmail1" class="col-lg-4 col-sm-2 control-label">
                                        Head size</label>
                                    <label class="col-lg-8" style="margin-bottom: 5px;">
                                        <input type="text" class="form-control" placeholder="" value="" />
                                    </label>
                                    <div class="clear">
                                    </div>
                                    <label for="inputEmail1" class="col-lg-4 col-sm-4 control-label" style="margin-bottom: 15px;
                                        margin-top: 10px;">
                                        Finish at the same point</label>
                                    <div class="col-lg-8" style="margin-top: 17px; margin-left: -15px;">
                                        <label class="label_radio r_on col-lg-2 col-sm-2" for="radio-01" style="padding-bottom: 0px;
                                            font-weight: normal;">
                                            <input name="finish-radio1" value="1" type="radio" class="curve_sel1" />
                                            Yes
                                        </label>
                                        <label class="label_radio r_off col-lg-2 col-sm-2" for="radio-02" style="padding-bottom: 0px;
                                            font-weight: normal;">
                                            <input name="finish-radio1" value="1" type="radio" />
                                            No
                                        </label>
                                    </div>
                                    <div class="clear">
                                    </div>
                                    <label for="inputEmail1" class="col-lg-4 col-sm-4 control-label" style="margin-bottom: 15px;">
                                        Match</label>
                                    <div class="radios" style="padding-top: 8px;">
                                        <label class="label_radio r_on col-lg-2 col-sm-2" for="radio-01" style="padding-bottom: 0px;">
                                            <input name='test' id="Radio3" value="b" type="radio">Straight
                                        </label>
                                        <label class="label_radio r_off col-lg-2 col-sm-2" for="radio-02" style="padding-bottom: 0px;">
                                            <input name='test' value="a" type="radio" class="curve_sel">
                                            Curved
                                        </label>
                                        <label class="label_radio r_off col-lg-2 col-sm-2" for="radio-02" style="padding-bottom: 0px;">
                                            <input name='test' value="a" type="radio" id="watch-me" class="curve_sel">
                                            Tailored
                                        </label>
                                    </div>
                                    <div class="clear">
                                    </div>
                                    <div id="show-me" style="display: none; margin-bottom: 15px; padding-top: 12px;">
                                        <label for="inputEmail1" class="col-lg-4 col-sm-4 control-label">
                                            Curve Type</label>
                                        <div class="col-lg-8">
                                            <div class="radios" style="padding-top: 8px;">
                                                <label class="label_radio r_on col-lg-6 col-sm-3" for="radio-01" style="padding-bottom: 0px;
                                                    padding-left: 0px;">
                                                    <input name="Curve-radio" id="Curve1" value="1" type="radio" />Follows the shape
                                                    of mount
                                                    <button data-content="Image will be placed here" data-placement="right" data-trigger="hover"
                                                        class="btn btn-round btn-info popovers">
                                                        ?</button>
                                                </label>
                                                <label class="label_radio r_off col-lg-6 col-sm-3" for="radio-02" style="padding-bottom: 0px;">
                                                    <input name="Curve-radio" id="Curve1" value="1" type="radio" />Curved to Fit
                                                    <button data-content="Image will be placed here" data-placement="right" data-trigger="hover"
                                                        class="btn  btn-round small btn-info popovers">
                                                        ?</button>
                                                </label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="clear">
                                    </div>
                                    <label for="inputEmail1" class="col-lg-4 col-sm-4 control-label">
                                        Additonal Info</label>
                                    <div class="col-lg-8" style="margin-top: 7px;">
                                        <textarea class="form-control" cols="60" rows="3"></textarea>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="inputEmail1" class="col-lg-4 col-sm-4 control-label">
                                        CAD Requested
                                        <button data-content="Has customer requested CAD? If customer has not requested CAD, you can approve without reverting to customer."
                                            data-placement="right" data-trigger="hover" class="btn btn-round btn-info popovers">
                                            ?</button></label>
                                    <div class="col-lg-8">
                                        <div class="radios" style="padding-top: 8px;">
                                            <label class="label_radio r_on col-lg-2 col-sm-2" for="radio-01" style="padding-bottom: 0px;">
                                                <input name="cad-radio" id="radio-01" value="1" type="radio">
                                                Yes
                                            </label>
                                            <label class="label_radio r_off col-lg-2 col-sm-2" for="radio-02" style="padding-bottom: 0px;">
                                                <input name="cad-radio" id="radio-02" value="1" type="radio">
                                                No
                                            </label>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="inputEmail1" class="col-lg-4 col-sm-4 control-label">
                                        Sample Provided</label>
                                    <div class="col-lg-8">
                                        <div class="radios" style="padding-top: 8px;">
                                            <label class="label_radio r_on col-lg-2 col-sm-2" for="radio-01" style="padding-bottom: 0px;">
                                                <input name="sample-radio1" value="1" type="radio" id="SampleProvidedYes" class="curve_sel1" />
                                                Yes
                                            </label>
                                            <label class="label_radio r_off col-lg-2 col-sm-2" for="radio-02" style="padding-bottom: 0px;">
                                                <input name="sample-radio1" id="SampleProvidedNo" value="1" type="radio" />
                                                No
                                            </label>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group" id="sample-me" style="display: none;">
                                    <label class="col-lg-4 col-sm-4 control-label">
                                        Make exact copy of samples
                                    </label>
                                    <div class="col-lg-8">
                                        <div class="radios" style="padding-top: 8px;">
                                            <label class="label_radio r_on col-lg-2 col-sm-2" for="radio-01" style="padding-bottom: 0px;">
                                                <input name="copy-radio1" value="1" type="radio" class="curve_sel1" />
                                                Yes
                                            </label>
                                            <label class="label_radio r_off col-lg-2 col-sm-2" for="radio-02" style="padding-bottom: 0px;">
                                                <input name="copy-radio1" value="1" type="radio" />
                                                No
                                            </label>
                                        </div>
                                    </div>
                                    <label for="inputEmail1" class="col-lg-4 col-sm-4 control-label">
                                        No of Samples</label>
                                    <div class="col-lg-8">
                                        <select class="form-control" id="NoOfSamples">
                                            <option selected="selected">--Select--</option>
                                            <option>1</option>
                                            <option>2</option>
                                            <option>3</option>
                                            <option value="Other">Other</option>
                                        </select>
                                    </div>
                                </div>
                                <div class="form-group" id="divSampleOther" style="display: none;">
                                    <label class="col-lg-4 col-sm-2 control-label">
                                        Add No Of Samples
                                    </label>
                                    <div class="col-lg-8">
                                        <input type="text" class="form-control" id="Password5">
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="inputEmail1" class="col-lg-4 col-sm-4 control-label">
                                        Stone Provided</label>
                                    <div class="col-lg-8">
                                        <div class="radios" style="padding-top: 8px;">
                                            <label class="label_radio r_on col-lg-2 col-sm-2" for="radio-01" style="padding-bottom: 0px;">
                                                <input name="stone-radio1" value="1" type="radio" id="StoneProvidedYes" class="curve_sel1" />
                                                Yes
                                            </label>
                                            <label class="label_radio r_off col-lg-2 col-sm-2" for="radio-02" style="padding-bottom: 0px;">
                                                <input name="stone-radio1" id="StoneProvidedNo" value="1" type="radio" />
                                                No
                                            </label>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group" id="divStone" style="display: none; margin-bottom: 20px;">
                                    <label for="inputEmail1" class="col-lg-4 col-sm-4 control-label" style="margin-bottom: 20px;">
                                        Stone Description</label>
                                    <label class="col-lg-8">
                                        <input type="text" class="form-control" />
                                    </label>
                                    <div class="clear">
                                    </div>
                                    <label for="inputEmail1" class="col-lg-4 col-sm-4 control-label" style="margin-bottom: 20px;">
                                        Setting Instructions</label>
                                    <label class="col-lg-8">
                                        <input type="text" class="form-control" />
                                    </label>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="panel">
                        <div class="panel-body">
                            <div class="form-horizontal" role="form">
                                <div class="form-group">
                                    <label for="inputEmail1" class="col-lg-4 col-sm-4 control-label">
                                        Team Member</label>
                                    <div class="col-lg-8">
                                        <select class="form-control m-bot15" placeholder="Customer Name" style="margin-bottom: 0px;">
                                            <option>Order 1</option>
                                            <option>Order 2</option>
                                            <option>Order 3</option>
                                        </select>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="inputEmail1" class="col-lg-4 col-sm-4 control-label">
                                        Assignee
                                    </label>
                                    <div class="col-lg-8">
                                        <select class="form-control m-bot15" placeholder="Customer Name" style="margin-bottom: 0px;">
                                            <option>Name 1</option>
                                            <option>Name 2</option>
                                            <option>Name 3</option>
                                        </select>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-lg-4 col-sm-4 control-label">
                                        Remarks</label>
                                    <div class="col-lg-8">
                                        <textarea class="form-control" cols="60" rows="3"></textarea>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="panel">
        <div class="panel-body ">
            <div class="form-group ">
                <div class="col-lg-8">
                    <button type="submit" class="btn btn-info">
                        Place Order</button>
                    &nbsp;
                    <button type="submit" class="btn btn-info">
                        Reset</button>
                </div>
            </div>
        </div>
    </div>
    <!--right Side End-->
    <!--script for this page-->
    <script src="js/form-component.js"></script>
</asp:Content>
