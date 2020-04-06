using System;
using System.Collections.Generic;
using System.Linq;
using WebApplication3.Entity;
using System.Web;

namespace WebApplication3.Models
{
    public class AccountModel
    {

        public bool CheckAccount(AccountEntity account)
        {
            AccountEntities db = new AccountEntities();
            //int count = db.Accounts.Where(u => u.AccountName == txtUserName && u.AccountPassword == txtPassword).Count();
            string query = "Select count(*) as count from [User] where UserName = '" + account.AccountName + "' and Password = '" + account.AccountPass + "'";
            int count = db.Database.SqlQuery<int>(query).FirstOrDefault();

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
            string query = "Insert into [User] values ( '" + account.AccountName + "' , '" + account.AccountPass + "' )";
            try
            {
                db.Database.ExecuteSqlCommand(query);
                int user_id = db.Users.Where(u => u.UserName == account.AccountName && u.Password == account.AccountPass).Select(u => u.ID).FirstOrDefault();
                string query2 = "Insert into [RoleUser] values ( '" + account.ListRole + "' , '" + user_id + "' )";
                db.Database.ExecuteSqlCommand(query2);

            }
            catch (Exception)
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