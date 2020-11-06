using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tsumugi.Service;
using Tsumugi.Models.Detail;

namespace Tsumugi.Controllers
{
    public class DetailController : BaseController
    {
        public ActionResult Index()
        {
            return RedirectToAction("Details");
        }

        public ActionResult Detail(Guid walletID)
        {
            if (!TsumugiUser.IsLoggedOn) return RedirectToAction("Dashboard", "Dashboard");

            DetailModel m = new DetailModel
            {
                DC = DC,
                WalletID = walletID,
                TransactionList = DC.Transactions.Where(a => a.WalletID == walletID).OrderByDescending(b => b.Date).Select(c => new TransactionListItem(c)).ToList()
            };

            List<Transaction> calcList = DC.Transactions.Where(a => a.WalletID == walletID).ToList();

            m.MonthlyEarnings = Chart.CalcEarnings(DateTime.Now.Month, calcList);
            m.MonthlySpendings = Chart.CalcSpendings(DateTime.Now.Month, calcList);
            m.Trend = Chart.CalcTrend(DateTime.Now.Month, calcList);

            return View(m);
        }
    }
}