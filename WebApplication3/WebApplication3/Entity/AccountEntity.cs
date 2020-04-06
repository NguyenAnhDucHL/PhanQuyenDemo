using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebApplication3.Models;

namespace WebApplication3.Entity
{
    public class AccountEntity
    {
        public AccountEntity()
        {
        }

        public AccountEntity(string accountName, string accountPass)
        {
            AccountName = accountName;
            AccountPass = accountPass;
        }
        [Required(ErrorMessage = "UserName is not empty")]
        public string AccountName { get; set; }
        [RegularExpression("^[0-9+]{5}-[0-9+]{7}-[0-9]{1}$", ErrorMessage = "Error only input number")]
        [Required(ErrorMessage = "Password is not empty")]
        public string AccountPass { get; set; }
        [Required]
        [Compare("AccountPass", ErrorMessage = "Password not match")]
        public string RePass { get; set; }
        [Required(ErrorMessage = "UserName is not empty")]
        public string ListRole { get; set; }
    }
}