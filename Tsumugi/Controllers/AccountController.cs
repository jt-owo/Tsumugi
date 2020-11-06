using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Tsumugi.Models.Account;
using Tsumugi.Service;

namespace Tsumugi.Controllers
{
    public class AccountController : BaseController
    {
        public ActionResult Register(string email, string pw, string firstName, string lastName)
        {
            User user = new User
            {
                ID = Guid.NewGuid(),
                EMail = email,
                Password = Password.HashSHA256(pw),
                FirstName = firstName,
                LastName = lastName
            };

            DC.Users.InsertOnSubmit(user);
            DC.SubmitChanges();

            return RedirectToAction("Login", new { email, pw });
        }

        public ActionResult Login(string email, string pw)
        {
            User user = DC.Users.Where(m => m.EMail == email).FirstOrDefault();
            if (user != null)
            {
                if(Password.Verify(pw, user.Password))
                {
                    TsumugiUser.UserID = user.ID;
                    return RedirectToAction("Dashboard", "Dashboard");
                }

                return RedirectToAction("Dashboard", "Dashboard", new { loginFailed = true, errorMSG = "Password incorrect" });
            }
            else
            {
                return RedirectToAction("Dashboard", "Dashboard", new { loginFailed = true, errorMSG = $"There is no user with the E-Mail: {email}" });
            }
        }

        public ActionResult Logout()
        {
            TsumugiUser.UserID = null;
            return RedirectToAction("Dashboard", "Dashboard");
        }

        public ActionResult Profile(string errorMSG, bool success = false)
        {
            AccountModel m = new AccountModel()
            {
                ErrorMSG = errorMSG,
                Success = success
            };
            if (!TsumugiUser.IsLoggedOn) return View(m);

            m.User = DC.Users.Where(a => a.ID == TsumugiUser.UserID).FirstOrDefault();
            return View(m);
        }

        [HttpPost]
        public ActionResult Profile(AccountModel m)
        {
            User user = DC.Users.Where(a => a.ID == m.User.ID).FirstOrDefault();
            if (!string.IsNullOrEmpty(m.NewPassword) && m.NewPassword != m.RepeatPassword)
            {
                return RedirectToAction("Profile", new { errorMSG = "The passwords don't match!" });
            }
            else
            {
                user.Password = Password.HashSHA256(m.NewPassword);
            }

            user.FirstName = !string.IsNullOrEmpty(m.User.FirstName) ? m.User.FirstName : user.FirstName; user.FirstName = !string.IsNullOrEmpty(m.User.FirstName) ? m.User.FirstName : user.FirstName;
            user.LastName = !string.IsNullOrEmpty(m.User.LastName) ? m.User.LastName : user.LastName;
            user.EMail = !string.IsNullOrEmpty(m.User.EMail) ? m.User.EMail : user.EMail;
            DC.SubmitChanges();

            return RedirectToAction("Profile", new { success = true });
        }
    }
}