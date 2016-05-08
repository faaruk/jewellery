using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
namespace Collaboration.Web.UI
{
    public static class Common
    {
        #region Session Attributes
        public const string SESSION_USER = "Session_User";
        public const string SESSION_USERSLIST = "Session_UsersList";       
        public const string SESSION_MODELTYPELIST = "Session_ModelTypeList";
        public const string SESSION_METALLIST = "Session_MetalList";
        public const string SESSION_PROCESSTYPELIST = "Session_ProcessTypeList";
        public const string SESSION_FINGERSIZELIST = "Session_FingerSizeList";
        public const string SESSION_PRIORITIESLIST = "Session_PrioritiesList";
        public const string SESSION_RINGTYPESLIST = "Session_RingTypesList";
        public const string SESSION_MESSAGETHREADS = "Session_MessageThreads";
        public const string SESSION_MESSAGES = "Session_Messages";
        public const string SESSION_MESSAGESASSIGNED = "Session_MessagesAssigned";
        public const string SESSION_COUNTUNREADMESSAGES = "Session_CountUnReadMessages";
        public const string SESSION_UNREADMESSAGES = "Session_UnReadMessages";

        //By Kapil
        public const string SESSION_TICKETTHREADS = "Session_TicketThreads";
        public const string SESSION_TICKETS = "Session_Tickets";
        public const string SESSION_TICKETSASSIGNED = "Session_TicketsAssigned";
        public const string SESSION_COUNTUNREADTICKETS = "Session_CountUnReadTickets";
        public const string SESSION_UNREADTICKETS = "Session_UnReadTickets";

        public const string SESSION_ORDERASSIGNED = "Session_OrderAssigned";
        public const string SESSION_DOWNLOADFILENAME = "Session_DownloadFileName";
        public const string SESSION_DOWNLOADCONTENTTYPE = "Session_DownloadContentType";
        public const string SESSION_ORDERS = "Session_Orders";
        public const string SESSION_SPECIMENIMAGE = "Session_SpecimenImage";
        public const string SESSION_CADIMAGE = "Session_CADImage";
        public const string SESSION_CADS = "Session_Cads";
        public const string SESSION_COMAPARECADIMAGE = "Session_ComapareCADImage";
        public const string SESSION_SAMPLES = "Session_Samples";
        public const string SESSION_SPECIMENIMAGES = "Session_SpecimenImages";
        public const string SESSION_CUSTOMERSLIST = "Session_CustomersList";
        public const string SESSION_SAMPLESTATUSLIST = "Session_SampleStatusList";
        public const string SESSION_SAMPLESTRACKLIST = "Session_SamplesTrackList";
       

        #endregion Session Attributes
        #region Cookie Attributes
        public const string COOKIE_USER = "Cookie_User";
        public const string COOKIE_PASSWORD = "Cookie_Password";  
        #endregion Cookie Attributes
        #region AdditonalErrorInfo Type
        public const string INFO_SENDER = "Sender: ";
        public const string INFO_PROCEDURE = "Procedure: ";
        public const string INFO_SMTPCODE = "SMTP Error Code: ";
        public const string DROPDOWN_OTHERS_VALUE = "O";
        public const string DROPDOWN_OTHERS_TEXT = "Others";
        public const string DROPDOWN_SELECT_VALUE = "";
        public const string DROPDOWN_SELECT_TEXT = "--Select--";
        #endregion
        #region AppSettings
        public const string APPSETTINGS_SMTPSERVER = "SMTPServer";
        public const string APPSETTINGS_ENABLESSL = "EnableSsl";
        public const string APPSETTINGS_SMTPPORT = "SMTPPort";
        public const string APPSETTINGS_SMTPUSER = "SMTPUser";
        public const string APPSETTINGS_SMTPPASSWORD = "SMTPPassword";
        public const string APPSETTINGS_FROMEMAILID = "FromEmailID";
        public const string APPSETTINGS_ATTACHMENTURL = "MessageAttachmentURL";
        public const string APPSETTINGS_TICKETIMAGESURL = "TicketAttachmentURL";
        public const string APPSETTINGS_PROFILEIMAGESURL = "ProfileImagesURL";
        public const string APPSETTINGS_SPECIMENIMAGESURL = "SpecimenImagesURL";
        public const string APPSETTINGS_CADIMAGESURL = "CADImagesURL";
        public const string APPSETTINGS_CUSTOMERIMPORTURL = "CustomerImportURL";
        
        #endregion AppSettings
        #region Request Attributes
        public const string REQUEST_RETURNURL = "ReturnUrl";      
        public const string REQUEST_PAGE = "Page";
        public const string REQUEST_FILTERID = "FilterID";
        public const string REQUEST_PRIORITY = "Priority";
        public const string REQUEST_DAYS = "Days"; 
        
        #endregion Request Attributes
        
        #region enums
        /// <summary>
        /// Roles
        /// </summary>
        public enum Roles
        {
            Admin = 1,
            Team_Member = 2,
            Factory = 3
        }
        /// <summary>
        /// MessageTypes
        /// </summary>
        public enum MessageTypes
        {
            Success = 1,
            Warning  = 2,
            Error = 3,
            Other = 4
        }
        /// <summary>
        /// MessageTypes
        /// </summary>
        public enum Response
        {
            Yes = 'Y',
            No = 'N'
        }
        /// <summary>
        /// MessageTypes
        /// </summary>
        public enum ActionType
        {
            Add ,
            Edit,
            EditSelf,
            ViewDetails,
            Compare
        }
        public enum MailType
        {
            ResetPassword,
            ChangePassword,
            UserRegistration,
            CADConfirmation,
            CADResponse,
            CADThreeRequestsIsue
        }
        /// <summary>
        /// MessageTypes
        /// </summary>
        public enum IsActive
        {
            Yes = 1,
            No = 0
        }
        /// <summary>
        /// MessageTypes
        /// </summary>
        public enum CaseStatus
        {
            Open = 1,
            Closed = 2
        }
        /// <summary>
        /// MessageTypes
        /// </summary>
        public enum FilterType
        {
            Delayed = 1,
            SampleNotReturned = 2,
            ApproachingDeadline = 3,
            TMNotAssigned = 4,
            AssignedTo = 5,
            UnRead = 6,
            ApproachingDeadlineCustomize = 7,
            DelayedShipping = 8,
            ApproachingShippingDeadline = 9,
        }
        /// <summary>
        /// 
        /// </summary>
        public enum SubType
        {            
            Mount = 1,
            Finished = 2
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public static string EnumValue(this SubType e)
        {
            switch (e)
            {
                case SubType.Mount:
                    return "Mount";
                case SubType.Finished:
                    return "Finished";              
            }
            return "Others";
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public static string GetSubType(int? subTypeID)
        {
            switch (subTypeID)
            {
                case 1:
                    return Convert.ToString(SubType.Mount);
                case 2:
                    return Convert.ToString(SubType.Finished);
            }
            return "Others";
        }
        /// <summary>
        /// 
        /// </summary>
        public enum Length
        {
            CM,
            Inches
        }
        /// <summary>
        /// 
        /// </summary>
        public enum Quantity
        {
            One,
            Two,
            Three,
            Four,
            Five,
            Six,
            OnePieceOnly, 
            Seven,
            Eight,
            Nine,
            Ten
            
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public static string EnumText(this Quantity e)
        {
            switch (e)
            {
                case Quantity.One:
                    return "1";
                case Quantity.Two:
                    return "2";
                case Quantity.Three:
                    return "3";
                case Quantity.Four:
                    return "4";
                case Quantity.Five:
                    return "5";
                case Quantity.Six:
                    return "6";
                case Quantity.OnePieceOnly:
                    return "1 piece Only";
            }
            return "1 piece Only";
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public static string EnumValue(this Quantity e)
        {
            switch (e)
            {
                case Quantity.One:
                    return "1";
                case Quantity.Two:
                    return "2";
                case Quantity.Three:
                    return "3";
                case Quantity.Four:
                    return "4";
                case Quantity.Five:
                    return "5";
                case Quantity.Six:
                    return "6";
                case Quantity.OnePieceOnly:
                    return "1P";
            }
            return "Others";
        }
        /// <summary>
        /// 
        /// </summary>
        public enum ModelType
        {
            Ring = 1,
            Pendant = 2,
            Earring = 3,
            Jacket = 4,
            Bracelet = 5,
            Necklace = 6,
            Bangle = 7,
            Chain = 8
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public static string GetModelTypeName(int modelTypeID,string Suffix =" Type")
        {
            switch (modelTypeID)
            {
                case 1:
                    return Convert.ToString(ModelType.Ring) + Suffix;
                case 2:
                    return Convert.ToString(ModelType.Pendant) + Suffix;
                case 3:
                    return Convert.ToString(ModelType.Earring) + Suffix;
                case 4:
                    return Convert.ToString(ModelType.Jacket) + Suffix;
                case 5:
                    return Convert.ToString(ModelType.Bracelet) + Suffix;
                case 6:
                    return Convert.ToString(ModelType.Necklace) + Suffix;
                case 7:
                    return Convert.ToString(ModelType.Bangle) + Suffix;
                case 8:
                    return Convert.ToString(ModelType.Chain) + Suffix;
            }
            return "Others";
        }
        /// <summary>
        /// 
        /// </summary>
        public enum CurveType
        {
            Straight,
            Curved,
            Tailored
        }
        /// <summary>
        /// 
        /// </summary>
        public enum TailoredType
        {
            FollowsMountShape,
            CurveToFit
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public static string EnumText(this TailoredType e)
        {
            switch (e)
            {
                case TailoredType.FollowsMountShape:
                    return "Follows the shape of mount";
                case TailoredType.CurveToFit:
                    return "Curve to Fit";                
            }
            return "Others";
        }
        #endregion enums

        #region UIAttributes
        /// <summary>
        /// 
        /// </summary>
        public struct UIAttributes
        {
            public const string ATTRIBUTE_DISPLAY = "display";
            public const string ATTRIBUTE_VISIBILITY = "visibility";
            public const string DISPLAY_NONE = "none";
            public const string DISPLAY_BLOCK = "block";
            public const string ATTRIBUTE_CLASS = "class";
        }
        public struct EntityAttributes
        {
            public const string USERID = "UserID";
            public const string MODELTYPEID = "ModelTypeID";
            public const string METALID = "MetalID";
            public const string PROCESSTYPEID = "ProcessTypeID";
            public const string FINGERSIZEID = "FingerSizeID";
            public const string PRIORITYID = "PriorityID";
            public const string RINGTYPEID = "RingTypeID";
            public const string CADID = "CADID";
            public const string ORDERID = "OrderID";
            public const string ORDERSTATUSID = "OrderStatusID";
            public const string SPECIMENID = "SpecimenID";
            public const string CUSTOMERID = "CustomerID";
            public const string SAMPLESTATUSID = "SampleStatusID";
            

        }
        public struct Pages
        {
            public const string DEFAULT = "Default.aspx";
            public const string USERMANAGEMENT = "UserManagement.aspx";
            public const string MODELTYPES = "ModelTypes.aspx";
            public const string METALS = "Metals.aspx";
            public const string PROCESSTYPES = "ProcessTypes.aspx";
            public const string FINGERMEASUREMENTS = "FingerMeasurements.aspx";
            public const string CREATEORDER = "CreateOrder.aspx";
            public const string MESSAGETHREADS = "Messages.aspx";
            public const string TICKETTHREADS = "Tickets.aspx";
            public const string MESSAGES = "Messages.aspx";
            public const string PRIORITIES = "Priorities.aspx";
            public const string RINGTYPES = "RingTypes.aspx";
            public const string CUSTOMER = "Customers.aspx";
            public const string SAMPLESTATUS = "SampleStatus.aspx";
            public const string VIEWORDER = "ViewOrder.aspx";
            public const string VIEWORDERDETAILS = "ViewOrderDetails.aspx";
            public const string CANCELORDER = "CancelOrder.aspx";
        }
        public struct ImageRequestPages
        {
            public const string PAGE = "Page";
            public const string EDITORDER = "EditOrder";
            public const string VIEWORDER = "ViewOrder";
            public const string DYNAMICORDER = "DynamicOrder";
            public const string COMPARECAD = "CompareCAD";
            
        }
        public struct ProductSubTypes
        {
            public int SubTypeID;
            public string SubTypeName;
        }
        public struct ProductTypes
        {
            public const string PENDANTS = "Pendant";
            public const string EARRINGS = "Earrings";
            public const string JACKETS = "Jackets";
            public const string BRACELETS = "Bracelets";
            public const string NECKLACE = "Necklace";
            public const string BANGLES = "Bangles";
            public const string CHAINS = "Chains";
        }
        #endregion UIAttributes
    }
}