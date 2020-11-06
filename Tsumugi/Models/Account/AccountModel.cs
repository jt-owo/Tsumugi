using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tsumugi.Service;

namespace Tsumugi.Models.Account
{
    public class AccountModel
    {
        public User User { get; set; }
        public string NewPassword { get; set; }
        public string RepeatPassword { get; set; }
        public string ErrorMSG { get; set; }
        public bool Success { get; set; }
    }
}