using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tsumugi.Models.Detail;
using Tsumugi.Service;

namespace Tsumugi.Controllers
{
    public class JsonController : BaseController
    {
        /// <summary>
        /// Renames a wallet
        /// </summary>
        /// <param name="walletID">WalletID</param>
        /// <param name="newName">New name of the wallet</param>
        /// <returns>JSON Object</returns>
        public ActionResult RenameWallet(Guid walletID, string newName)
        {
            Wallet wallet = DC.Wallets.Where(m => m.ID == walletID).FirstOrDefault();
            if (wallet != null)
            {
                wallet.Name = newName;
                DC.SubmitChanges();
                return Json(wallet.Name, JsonRequestBehavior.AllowGet);
            }

            return Json(false, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Deletes a wallet
        /// </summary>
        /// <param name="walletID">WalletID</param>
        /// <returns>JSON Object</returns>
        public ActionResult DeleteWallet(Guid walletID)
        {
            Wallet wallet = DC.Wallets.Where(m => m.ID == walletID).FirstOrDefault();
            if(wallet != null)
            {
                if (Convert.ToBoolean(DC.DeleteWallet(walletID)))
                { 
                    return Json(wallet.Name, JsonRequestBehavior.AllowGet);
                }
            }

            return Json(false, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Deletes a user
        /// </summary>
        /// <returns>JSON Object or redirect to Dashboard page</returns>
        public ActionResult DeleteUser()
        {
            User user = DC.Users.Where(m => m.ID == TsumugiUser.UserID).FirstOrDefault();
            if (user != null)
            {
                if (Convert.ToBoolean(DC.DeleteUser(TsumugiUser.UserID)))
                {
                    TsumugiUser.UserID = null;
                    return RedirectToAction("Index", "Dashboard");
                }
            }

            return Json(false, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Get transaction detail
        /// </summary>
        /// <param name="transactionID">TransactionID</param>
        /// <returns>JSON Object</returns>
        public ActionResult GetTransaction(Guid transactionID)
        {
            Transaction transaction = DC.Transactions.Where(m => m.ID == transactionID).FirstOrDefault();
            if(transaction != null)
            {
                TransactionListItem item = new TransactionListItem(transaction);
                return Json(item, JsonRequestBehavior.AllowGet);
            }

            return Json(false, JsonRequestBehavior.AllowGet);
        }
    }
}