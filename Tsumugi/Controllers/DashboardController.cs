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
    public class DashboardController : BaseController
    {
        public ActionResult Index()
        {
            return RedirectToAction("Dashboard");
        }

        public ActionResult Dashboard(bool? loginFailed)
        {
            DashboardModel m = new DashboardModel();
            m.LoginFailed = loginFailed ?? false;
            if (!TsumugiUser.IsLoggedOn) return View(m);

            m.WalletList.Add(new WalletDummy("Main", 5000));
            m.WalletList.Add(new WalletDummy("Second", 50000));
            m.WalletList.Add(new WalletDummy("Third", 50000));
            return View(m);
        }

        [HttpPost]
        public ActionResult Dashboard(DashboardModel m)
        {
            if(!string.IsNullOrEmpty(m.EMail) && !string.IsNullOrEmpty(m.Password))
            {
                return RedirectToAction("Login", "Account", new { email = m.EMail, pw = m.Password });
            }
            return RedirectToAction("Dashboard", new { loginFailed = true });
        }
    }
}