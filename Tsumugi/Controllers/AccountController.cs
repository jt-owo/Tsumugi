using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tsumugi.Service;

namespace Tsumugi.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult Login()
        {
            // TODO: Connect to database and login user
            TsumugiUser.UserID = Guid.NewGuid();
            return RedirectToAction("Index", "Dashboard");
        }

        public ActionResult Logout()
        {
            TsumugiUser.UserID = null;
            return RedirectToAction("Index", "Dashboard");
        }
    }
}