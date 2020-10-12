using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tsumugi.Service;
using Tsumugi.Service.DummyData;
using Tsumugi.Models.Dashboard;

namespace Tsumugi.Controllers
{
    public class DetailController : BaseController
    {
        public ActionResult Index()
        {
            return RedirectToAction("Details");
        }

        public ActionResult Detail(Guid? walletID)
        {
            if (!TsumugiUser.IsLoggedOn) return RedirectToAction("Dashboard", "Dashboard");

            return View();
        }
    }
}