using System;
using System.Collections.Generic;
using System.Linq;
using WebApplication3.Entity;
using System.Web;
using System.Data.SqlClient;

namespace WebApplication3.Models
{
    public class AccountModel
    {

        public bool CheckAccount(AccountEntity account)
        {
            AccountEntities db = new AccountEntities();
            //int count = db.Accounts.Where(u => u.AccountName == txtUserName && u.AccountPassword == txtPassword).Count();
            string query = "Select count(*) as count from [User] where UserName = @username and Password = @password";
            int count = db.Database.SqlQuery<int>(query, new SqlParameter("username", account.AccountName), new SqlParameter("password",account.AccountPass)).FirstOrDefault();

            if (count == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool AddAccount(AccountEntity account)
        {
            AccountEntities db = new AccountEntities();
            string query = "EXEC InsertIntoAccount @Name = @accountname, @Password = @accountpass";
            try
            {
                db.Database.ExecuteSqlCommand(query, new SqlParameter("accountname", account.AccountName), new SqlParameter("accountpass", account.AccountPass));
                int user_id = db.Users.Where(u => u.UserName == account.AccountName && u.Password == account.AccountPass).Select(u => u.ID).FirstOrDefault();
                string query2 = "Insert into [RoleUser] values (@role_id, @user_id)";
                db.Database.ExecuteSqlCommand(query2 , new SqlParameter("role_id", account.Role_ID), new SqlParameter("user_id", user_id));
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        public List<Role> ListRoles()
        {
            AccountEntities db = new AccountEntities();
            var lstRole = db.Roles.ToList();
            return lstRole;
        }
    }
}