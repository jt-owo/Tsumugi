using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tsumugi.Service;

namespace Tsumugi.Controllers
{
    public class AccountController : BaseController
    {
        public ActionResult Login(string email, string pw)
        {
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