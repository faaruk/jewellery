using System;
using Collaboration.Business.Components;
using System.IO;
using DevExpress.Web;
using System.Configuration;
using System.Web;
using Collaboration.Web.UI.Utilities;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;


namespace Collaboration.Web.UI.Admin
{
    public partial class Customers : BasePage
    {
        Collaboration.Business.Components.AdminManager _adminManager = new Business.Components.AdminManager();
        //static string AttachmentLocation = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnImport_Click(object sender, EventArgs e)
        {
            if (Uploader.HasFile && Path.GetExtension(Uploader.PostedFile.FileName) == ".xls")
            {

                // var FilePath = AppDomain.CurrentDomain.BaseDirectory + "Uploads\\" + Uploader.PostedFile.FileName;

                string strFileName = DateTime.Now.ToString("ddMMyyyy_HHmmss");
                string strFileType = System.IO.Path.GetExtension(Uploader.FileName).ToString().ToLower();

                // var FilePath = ConfigurationManager.AppSettings["CustomerFilePath"] + strFileName + strFileType;

                //string attachmentURL = ConfigurationManager.AppSettings[Common.APPSETTINGS_CUSTOMERIMPORTURL];

                bool exists = System.IO.Directory.Exists(Server.MapPath("~/UploadedExcel/"));

                if (!exists)
                    System.IO.Directory.CreateDirectory(Server.MapPath("~/UploadedExcel/"));

                string FilePath = Server.MapPath("~/UploadedExcel/" + strFileName + strFileType);

                //string FilePath = attachmentURL + @"\" + Uploader.PostedFile.FileName;

                Uploader.SaveAs(FilePath);

                //int count = new AdminManager().ImportCustomers(FilePath);

                //File.Delete(FilePath);

                //Response.Redirect(Request.Url.OriginalString);


                string extension = Path.GetExtension(FilePath);
                string sSourceConstr = string.Empty;
                switch (extension)
                {
                    case ".xls": //Excel 97-03
                        //sSourceConstr = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
                        //sSourceConstr = "Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" + FilePath + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
                        sSourceConstr = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + FilePath + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                        break;
                    case ".xlsx": //Excel 07 or higher
                        // sSourceConstr = ConfigurationManager.ConnectionStrings["Excel07ConString"].ConnectionString;
                        sSourceConstr = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + FilePath + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                        break;

                }

                // sSourceConstr = string.Format(sSourceConstr, filePath);
                string sDestConstr = ConfigurationManager.ConnectionStrings["RiverMountConnectionString"].ConnectionString;
                OleDbConnection sSourceConnection = new OleDbConnection(sSourceConstr);
                using (sSourceConnection)
                {
                    sSourceConnection.Open();
                    var sheetNames = sSourceConnection.GetSchema("Tables");
                    string name = "";
                    foreach (DataRow row in sheetNames.Rows)
                    {
                        name = row["TABLE_NAME"].ToString();
                        //select from this sheet
                        //do whatever else
                    }
                    string sql = string.Format("Select [Customer Code],[Name 1],[Email] FROM [{0}]", name);
                    OleDbCommand command = new OleDbCommand(sql, sSourceConnection);

                    using (OleDbDataReader dr = command.ExecuteReader())
                    {
                        int a = dr.RecordsAffected;
                        using (SqlBulkCopy bulkCopy = new SqlBulkCopy(sDestConstr))
                        {
                            bulkCopy.DestinationTableName = "Customer_Temp";
                            bulkCopy.ColumnMappings.Add("Customer Code", "CustomerCode");
                            //bulkCopy.ColumnMappings.Add("Ref. Code", "ReferenceCode");
                            bulkCopy.ColumnMappings.Add("Name 1", "CustomerName");
                            bulkCopy.ColumnMappings.Add("Email", "CustomerEmail");
                            bulkCopy.WriteToServer(dr);
                        }
                    }
                }


                var sqlCon = new SqlConnection(ConfigurationManager.ConnectionStrings["RiverMountConnectionString"].ConnectionString.ToString());
                sqlCon.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = sqlCon;
                cmd.CommandText = "Import_Customer";

                var ok = cmd.ExecuteNonQuery();

                //int count = new AdminManager().ImportCustomers(FilePath);




                //using (var context = new CollaborationDBContext())
                //{
                //    ObjectResult<int?> iReturnValue = context.ImportCustomer();
                //    return Convert.ToInt32(iReturnValue.SingleOrDefault());
                //}



                File.Delete(FilePath);

                Response.Redirect(Request.Url.OriginalString);


            }

            //if (AttachmentLocation != string.Empty)
            //{
            //    try
            //    {
            //        int count = new AdminManager().ImportCustomers(Server.MapPath(AttachmentLocation));
            //        MessageUtility.ShowMessage(dvMessageMain, ltMessageMain, Convert.ToInt16(Common.MessageTypes.Success), string.Format(Resource.InfoRecordsProcessed, count.ToString()));

            //        File.Delete(AttachmentLocation);
            //        AttachmentLocation = string.Empty;

            //        Response.Redirect(Request.Url.OriginalString);
            //    }
            //    catch (BLLException exception)
            //    {
            //        MessageUtility.ShowMessage(dvMessageMain, ltMessageMain, Convert.ToInt16(Common.MessageTypes.Error), exception.ErrorMessage);
            //        ExceptionUtility.LogException(exception, Common.INFO_PROCEDURE + exception.ProcedureName);

            //    }
            //}
            //else
            //    MessageUtility.ShowMessage(dvMessageMain, ltMessageMain, Convert.ToInt16(Common.MessageTypes.Success), Resource.ErrRequiredUploadFile);

        }


        //protected void flUploadCustomer_UploadedComplete(object sender, AjaxControlToolkit.AsyncFileUploadEventArgs e)
        //{
        //    try
        //    {
        //        if (flUploadCustomer.PostedFile != null)
        //        {
        //            string fileName = flUploadCustomer.PostedFile.FileName;
        //            int contentLength = flUploadCustomer.PostedFile.ContentLength;

                   
        //                string attachmentURL = ConfigurationManager.AppSettings[Common.APPSETTINGS_CUSTOMERIMPORTURL];

        //                bool exists = System.IO.Directory.Exists(Server.MapPath(attachmentURL));

        //                if (!exists)
        //                    System.IO.Directory.CreateDirectory(Server.MapPath(attachmentURL));

        //                AttachmentLocation = attachmentURL + @"\" + fileName;
        //                flUploadCustomer.SaveAs(Server.MapPath(AttachmentLocation));
                    
        //        }
        //    }
        //    catch (Exception)
        //    { }
        //}     
  
    }
}