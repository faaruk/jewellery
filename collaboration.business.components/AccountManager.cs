using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Collaboration.Data;
using Collaboration.Business.Entities;
using System.Data;
using Collaboration.Business.Components.Utilities;
namespace Collaboration.Business.Components
{
    public class AccountManager
    {        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserName"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        public virtual bool ValidateUser(string UserName, string Password,out int UserID)
        {
            try
            {
                return new AccountContext().ValidateUser(UserName, Password, out UserID);
            }
            catch (Exception ex)
            {
                throw MessageUtility.GetErrorMessage(ex);
            } 
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserName"></param>
        /// <param name="Password"></param>
        /// <param name="EmailID"></param>
        /// <returns></returns>
        public bool ResetPassword(string UserName, string Password, out String EmailID)
        {
            try
            {
                return new AccountContext().ResetPassword(UserName, Password, out EmailID);
            }
            catch (Exception ex)
            {
                throw MessageUtility.GetErrorMessage(ex);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        public Boolean ChangePassword(string UserID, string Password)
        {
            try
            {
                return new AccountContext().ChangePassword(UserID, Password);
            }
            catch (Exception ex)
            {
                throw MessageUtility.GetErrorMessage(ex);
            }            
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public virtual User GetUserInfo(int UserID)
        {
            try
            {
                return new AccountContext().GetUserInfo(UserID);
            }
            catch (Exception ex)
            {
                throw MessageUtility.GetErrorMessage(ex);
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
            try
            {
               return new AccountContext().ModifyUserInfo(UserInfo);
            }
            catch (Exception ex)
            {
                throw MessageUtility.GetErrorMessage(ex);
            }            
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<User> GetUsers()
        {
            try
            {
                return new AccountContext().GetUsers();
            }
            catch (Exception ex)
            {
                throw MessageUtility.GetErrorMessage(ex);
            }             
        }
         /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool CreateUser(User user)
        {
            try
            {
                return new AccountContext().CreateUser(user);
            }
            catch (Exception ex)
            {
                throw MessageUtility.GetErrorMessage(ex);
            }           
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual List<Role> GetRoles()
        {
            try
            {
                return new AccountContext().GetRoles();
            }
            catch (Exception ex)
            {
                throw MessageUtility.GetErrorMessage(ex);
            } 
        }
         /// <summary>
       /// 
       /// </summary> 
       /// <param name="UserInfo"></param>
       /// <returns></returns>
        public virtual bool DeleteUser(int UserID)
        {
            try
            {
                return new AccountContext().DeleteUser(UserID);
            }
            catch (Exception ex)
            {
                throw MessageUtility.GetErrorMessage(ex);
            }            
        }
    }
}
