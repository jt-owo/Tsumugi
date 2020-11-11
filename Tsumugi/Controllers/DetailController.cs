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
        /// <summary>
        /// Index method
        /// </summary>
        /// <returns>Redirect to Detail View</returns>
        public ActionResult Index()
        {
            return RedirectToAction("Detail");
        }

        /// <summary>
        /// Opens the detail page for a wallet
        /// </summary>
        /// <param name="walletID">WalletID</param>
        /// <returns>Detail View</returns>
        public ActionResult Detail(Guid walletID)
        {
            if (!TsumugiUser.IsLoggedOn) return RedirectToAction("Dashboard", "Dashboard");

            DetailModel m = new DetailModel
            {
                DC = DC,
                WalletID = walletID,
                TransactionList = DC.Transactions.Where(a => a.WalletID == walletID).OrderByDescending(b => b.Date).Select(c => new TransactionListItem(c)).ToList()
            };

            List<SelectListItem> options = DC.Categories.Select(a => new SelectListItem { Value = a.ID.ToString(), Text = a.Name }).ToList();
            options.Add(new SelectListItem { Value = "", Text = "-" });
            m.CategoryOptions = new SelectList(options, "Value", "Text");

            List<Transaction> calcList = DC.Transactions.Where(a => a.WalletID == walletID).ToList();

            m.MonthlyEarnings = Chart.CalcEarnings(DateTime.Now.Month, calcList);
            m.MonthlySpendings = Chart.CalcSpendings(DateTime.Now.Month, calcList);
            m.Trend = Chart.CalcTrend(DateTime.Now.Month, calcList);

            return View(m);
        }

        /// <summary>
        /// Detail page POST
        /// </summary>
        /// <param name="m">DetailModel</param>
        /// <returns>Redirect to Detail GET</returns>
        [HttpPost]
        public ActionResult Detail(DetailModel m)
        {
            if (m.DeleteTransactionID.HasValue)
            {
                Transaction delTrans = DC.Transactions.Where(a => a.ID == m.DeleteTransactionID).FirstOrDefault();
                
                if(delTrans != null)
                {
                    DC.Transactions.DeleteOnSubmit(delTrans);
                    DC.SubmitChanges();
                }
            }
            else
            {
                Transaction newTrans = new Transaction
                {
                    ID = Guid.NewGuid(),
                    WalletID = m.WalletID,
                    Type = m.Type,
                    Date = m.Date,
                    Title = m.Title,
                    Note = m.Note,
                    Value = decimal.Parse(m.Value),
                    CategoryID = m.CategoryID
                };

                DC.Transactions.InsertOnSubmit(newTrans);
                DC.SubmitChanges();
            }

            return RedirectToAction("Detail", new { walletID = m.WalletID });
        }
    }
}