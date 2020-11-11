using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tsumugi.Service;
using Tsumugi.Models.Dashboard;

namespace Tsumugi.Controllers
{
    public class DashboardController : BaseController
    {
        /// <summary>
        /// Index method
        /// </summary>
        /// <returns>Redirect to Dashboard View</returns>
        public ActionResult Index()
        {
            return RedirectToAction("Dashboard");
        }

        /// <summary>
        /// Opens the dashboard page
        /// </summary>
        /// <param name="loginFailed">True if login failed</param>
        /// <param name="errorMSG">Error message if something went wrong</param>
        /// <returns>Dashboard View</returns>
        public ActionResult Dashboard(bool? loginFailed, string errorMSG)
        {
            DashboardModel m = new DashboardModel();
            m.LoginFailed = loginFailed ?? false;
            m.ErrorMSG = errorMSG;

            if (!TsumugiUser.IsLoggedOn) return View(m);

            m.WalletList = DC.Wallets.Where(a => a.UserID == TsumugiUser.UserID.Value).OrderBy(b => b.Name).Select(c => new WalletListItem(c, DC)).ToList();

            return View(m);
        }

        /// <summary>
        /// Dashboard page POST method
        /// </summary>
        /// <param name="m">DashboardModel</param>
        /// <returns>Redirect to Dashboard GET</returns>
        [HttpPost]
        public ActionResult Dashboard(DashboardModel m)
        {
            if (!string.IsNullOrEmpty(m.RegisterFirstName) && !string.IsNullOrEmpty(m.RegisterLastName)
                && !string.IsNullOrEmpty(m.RegisterEMail) && !string.IsNullOrEmpty(m.RegisterPassword))
            {
                return RedirectToAction("Register", "Account", new { email = m.RegisterEMail, pw = m.RegisterPassword, firstName = m.RegisterFirstName, lastName = m.RegisterLastName });
            }
            else if(!string.IsNullOrEmpty(m.EMail) && !string.IsNullOrEmpty(m.Password))
            {
                return RedirectToAction("Login", "Account", new { email = m.EMail, pw = m.Password });
            }
            else if (!string.IsNullOrEmpty(m.NewWalletName) && TsumugiUser.UserID.HasValue)
            {
                Wallet w = new Wallet
                {
                    ID = Guid.NewGuid(),
                    UserID = TsumugiUser.UserID.Value,
                    Name = m.NewWalletName,
                    Sum = 0
                };

                DC.Wallets.InsertOnSubmit(w);
                DC.SubmitChanges();
            }
            return RedirectToAction("Dashboard");
        }
    }
}