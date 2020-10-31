using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tsumugi.Service;

namespace Tsumugi.Controllers
{
    public class JsonController : BaseController
    {
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

        public ActionResult DeleteWallet(Guid walletID)
        {
            Wallet wallet = DC.Wallets.Where(m => m.ID == walletID).FirstOrDefault();
            if(wallet != null)
            {
                // EXECUTE DELETE STORED PROCEDURES
                return Json(wallet.Name, JsonRequestBehavior.AllowGet);
            }

            return Json(false, JsonRequestBehavior.AllowGet);
        }
    }
}