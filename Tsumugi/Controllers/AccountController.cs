using System;
using System.Linq;
using System.Web.Mvc;
using Tsumugi.Models.Account;
using Tsumugi.Service;

namespace Tsumugi.Controllers
{
    public class AccountController : BaseController
    {
        /// <summary>
        /// Creates a new user
        /// </summary>
        /// <param name="email">E-Mail</param>
        /// <param name="pw">Password</param>
        /// <param name="firstName">First name</param>
        /// <param name="lastName">Last name</param>
        /// <returns>Redirect to Login method</returns>
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

        /// <summary>
        /// Login an existing user
        /// </summary>
        /// <param name="email">E-Mail</param>
        /// <param name="pw">Password</param>
        /// <returns>Redirect to Dashboard View</returns>
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

        /// <summary>
        /// Logout user
        /// </summary>
        /// <returns>Redirect to Dashboard View</returns>
        public ActionResult Logout()
        {
            TsumugiUser.UserID = null;
            return RedirectToAction("Dashboard", "Dashboard");
        }

        /// <summary>
        /// Opens the profile page
        /// </summary>
        /// <param name="errorMSG">Error Message if something went wrong</param>
        /// <param name="success">True if password changing or user data changing was successful</param>
        /// <returns>Profile View</returns>
        public ActionResult Profile(string errorMSG, bool success = false)
        {
            if (!TsumugiUser.IsLoggedOn) return RedirectToAction("Dashboard", "Dashboard");
            AccountModel m = new AccountModel()
            {
                ErrorMSG = errorMSG,
                Success = success
            };

            m.User = DC.Users.Where(a => a.ID == TsumugiUser.UserID).FirstOrDefault();
            return View(m);
        }

        /// <summary>
        /// Profile page POST method
        /// </summary>
        /// <param name="m">AccountModel</param>
        /// <returns>Redirect to Profile GET</returns>
        [HttpPost]
        public ActionResult Profile(AccountModel m)
        {
            User user = DC.Users.Where(a => a.ID == m.User.ID).FirstOrDefault();
            if(user == null)
            {
                return RedirectToAction("Dashboard", "Dashboard");
            }
            if (!string.IsNullOrEmpty(m.NewPassword) && m.NewPassword != m.RepeatPassword)
            {
                return RedirectToAction("Profile", new { errorMSG = "The passwords don't match!" });
            }
            else if(!string.IsNullOrEmpty(m.NewPassword) && m.NewPassword == m.RepeatPassword)
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