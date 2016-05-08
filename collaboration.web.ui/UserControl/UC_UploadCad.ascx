<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UC_UploadCad.ascx.cs"
    Inherits="Collaboration.Web.UI.UserControl.UC_UploadCad" %>

<script type="text/javascript">
    //File Upload response from the server
    Dropzone.options.dropzoneForm = {
        acceptedFiles: 'image/*',
        maxFiles: 1,
        url: "hn_SimpeFileUploader.ashx",
        //url: "../UserControl/UC_UploadCad.ascx",

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
<div class="jumbotron">
    <div class="dropzone" id="dropzoneForm">
        <div class="fallback">
            <input name="file" type="file" multiple />
            <input type="submit" value="Upload" />
        </div>
    </div>
</div>

