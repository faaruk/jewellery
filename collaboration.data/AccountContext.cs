using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Collaboration.Business.Entities;
using System.Data;
using System.Data.Objects;
using System.Data.SqlClient;
namespace Collaboration.Data
{
    public class AccountContext
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserName"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        public bool ValidateUser(string UserName, string Password, out int UserID)
        {
                UserID = 0;
                using (var context = new CollaborationDBContext())
                {
                    //var clientIdParameter = new SqlParameter("@ClientId", 4);

                    System.Nullable<int> iReturnValue = context.ValidateUser(UserName, Password).SingleOrDefault();
                    if (iReturnValue.HasValue)
                    {
                        bool result = (Convert.ToInt32(iReturnValue.Value) == 0 ? false : true);
                        UserID = Convert.ToInt32(iReturnValue.Value);
                        return result;
                    }
                    else
                        return false;
                }           
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserName"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        public Boolean ResetPassword(string UserName, string Password, out String EmailID)
        {           
                using (var context = new CollaborationDBContext())
                {
                    //var clientIdParameter = new SqlParameter("@ClientId", 4);

                    EmailID = context.ChangePassword(UserName, Password).SingleOrDefault();
                    if (EmailID != String.Empty)
                        return true;
                    else
                        return false;
                } 
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserName"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        public bool ChangePassword(string UserName, string Password)
        {         
            using (var context = new CollaborationDBContext())
            {
                string emailID = string.Empty;
                emailID = context.ChangePassword(UserName, Password).SingleOrDefault();
                if (emailID != String.Empty)
                    return true;
                else
                    return false;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<User> GetUsers()
        {
            using (var context = new CollaborationDBContext())
            {
                var userInfo = context.GetUserInfo(0);
                return userInfo.ToList();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserName"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        public User GetUserInfo(int UserID)
        {
            using (var context = new CollaborationDBContext())
            {
                var userInfo = context.GetUserInfo(UserID).First();
                //userInfo = context.Users.Where(x => x.UserID == UserID && x.IsActive=="Y").First();
                return userInfo;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool CreateUser(User UserInfo)
        {
            using (var context = new CollaborationDBContext())
            {
                var iReturnValue = context.CreateUser(UserInfo.UserName, UserInfo.FirstName, UserInfo.LastName,UserInfo.StringPassword, UserInfo.EMail, UserInfo.Mobile, UserInfo.ImageLocationURL, UserInfo.IsActive, UserInfo.RoleID, UserInfo.DefaultPasswordChanged);
                return true;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserName"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        public bool ModifyUserInfo(User UserInfo)
        {
            using (var context = new CollaborationDBContext())
            {
                var iReturnValue = context.ModifyUser(UserInfo.UserID, UserInfo.StringPassword, UserInfo.FirstName, UserInfo.LastName, UserInfo.EMail, UserInfo.Mobile, UserInfo.ImageLocationURL, UserInfo.IsActive, UserInfo.RoleID, UserInfo.DefaultPasswordChanged);                
                return true;
            }
        }
       /// <summary>
       /// 
       /// </summary>
       /// <param name="UserInfo"></param>
       /// <returns></returns>
        public bool DeleteUser(int UserID)
        {
            using (var context = new CollaborationDBContext())
            {
                var iReturnValue = context.DeleteUser(UserID);
                return true;
            }
        }
 
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Role> GetRoles()
        {
            using (var context = new CollaborationDBContext())
            {
                var roles = context.Roles.Where(x => x.IsActive == true);
                return roles.ToList();
            }
        }
    }
}
