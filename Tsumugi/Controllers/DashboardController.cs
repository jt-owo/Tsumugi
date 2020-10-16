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

        public ActionResult Dashboard(bool? loginFailed, string errorMSG)
        {
            DashboardModel m = new DashboardModel();
            m.LoginFailed = loginFailed ?? false;
            m.ErrorMSG = errorMSG;

            if (!TsumugiUser.IsLoggedOn) return View(m);

            m.WalletList = DC.Wallets.Where(a => a.UserID == TsumugiUser.UserID.Value).OrderBy(b => b.Name).Select(c => new WalletListItem(c, DC)).ToList();

            return View(m);
        }

        [HttpPost]
        public ActionResult Dashboard(DashboardModel m)
        {
            if (!string.IsNullOrEmpty(m.RegisterFirstName) && !string.IsNullOrEmpty(m.RegisterLastName)
                && !string.IsNullOrEmpty(m.RegisterEMail) && !string.IsNullOrEmpty(m.RegisterPassword))
            {
                return RedirectToAction("Register", "Account", new { email = m.RegisterEMail, pw = m.RegisterPassword, firstName = m.RegisterFirstName, lastName = m.RegisterLastName });
            }
            else
            {
                return RedirectToAction("Login", "Account", new { email = m.EMail, pw = m.Password });
            }
        }
    }
}