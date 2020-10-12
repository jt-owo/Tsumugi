using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Tsumugi.Service;

namespace Tsumugi.Controllers
{
    public class AccountController : BaseController
    {
        public ActionResult Register(string email, string pw, string firstName, string lastName)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashValue = sha256.ComputeHash(Encoding.UTF8.GetBytes(pw));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < hashValue.Length; i++)
                {
                    builder.Append(hashValue[i].ToString("x2"));
                }

                pw = builder.ToString();
            }

            User user = new User
            {
                EMail = email,
                Password = pw,
                FirstName = firstName,
                LastName = lastName
            };

            DC.Users.InsertOnSubmit(user);

            try
            {
                DC.SubmitChanges();
            }
            catch (Exception e)
            {
                // To be defined
            }

            return RedirectToAction("Login", new { email = email, pw = pw });
        }

        public ActionResult Login(string email, string pw)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashValue = sha256.ComputeHash(Encoding.UTF8.GetBytes(pw));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < hashValue.Length; i++)
                {
                    builder.Append(hashValue[i].ToString("x2"));
                }

                pw = builder.ToString();
            }

            // TODO: Connect to database and login user
            User user = DC.Users.Where(m => m.EMail == email && m.Password == pw).FirstOrDefault();
            if (user != null)
            {
                TsumugiUser.UserID = user.ID;
            }

            return RedirectToAction("Dashboard", "Dashboard");
        }

        public ActionResult Logout()
        {
            TsumugiUser.UserID = null;
            return RedirectToAction("Dashboard", "Dashboard");
        }
    }
}