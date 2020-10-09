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
            TsumugiUser.UserID = Guid.NewGuid();
            return RedirectToAction("Dashboard", "Dashboard");
        }

        public ActionResult Logout()
        {
            TsumugiUser.UserID = null;
            return RedirectToAction("Dashboard", "Dashboard");
        }
    }
}