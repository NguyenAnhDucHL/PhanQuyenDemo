using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebApplication3.Entity;
using WebApplication3.Models;
using WebApplication3.SupportClass;
using XCrypt;

namespace WebApplication3.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account

        [HttpGet]
        [Route("login")]
        public ActionResult Index()
        {
            return View("~/Views/LoginAccount/LoginForm.cshtml");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("home")]
        public ActionResult CheckAccount(string txtUserName, string txtPassword)
        {
            if(ModelState.IsValid)
            {
                AccountModel accountModel = new AccountModel();
                string passXC = new XCryptEngine(XCryptEngine.AlgorithmType.MD5).Encrypt(txtPassword,
                                                                                             "pl");
                bool check = accountModel.CheckAccount(new AccountEntity(txtUserName, passXC));
                if (check)
                {
                    Session["username"] = txtUserName;
                    FormsAuthentication.SetAuthCookie(txtUserName, false);
                    return View("~/Views/Home/Index.cshtml");
                }
                else
                {
                    ViewBag.error = "Error Password and User";
                    return View("~/Views/Home/Index.cshtml");
                }
            }
            else
            {
                ViewBag.error = "Error";
                return View("~/Views/LoginAccount/LoginForm.cshtml");
            }
           
        }

        [Route("registration")]
        [HttpGet]
        public ActionResult Registration()
        {
            ListRole();
            return View("~/Views/LoginAccount/RegistrationForm.cshtml");
        }

        [Route("registration")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddRegistration(AccountEntity account)
        {
            if (ModelState.IsValid)
            {
                AccountModel accountModel = new AccountModel();
                string passXC = new XCryptEngine(XCryptEngine.AlgorithmType.MD5).Encrypt(account.AccountPass,
                                                                                         "pl");
                account.AccountPass = passXC;
                bool check = accountModel.AddAccount(account);
                if (check == true)
                {
                    return View("~/Views/LoginAccount/LoginForm.cshtml");
                }
                else
                {
                    ViewBag.error = "Error";
                    ListRole();
                    return View("~/Views/LoginAccount/RegistrationForm.cshtml");
                }
            }
            else
            {
                ListRole();
                ViewBag.error = "Error";
                return View("~/Views/LoginAccount/RegistrationForm.cshtml");
            }
        }

        private void ListRole()
        {
            List<Role> roles = new AccountModel().ListRoles();
            List<SelectListItem> listItems = new List<SelectListItem>();
            foreach (Role role in roles)
            {
                listItems.Add(new SelectListItem { Text = role.Role_Name, Value = role.ID.ToString() });
            }
            ViewBag.lstRoles = listItems;
        }
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session["username"] = null;
            return RedirectToAction("Index", "Account");
        }
    }
}