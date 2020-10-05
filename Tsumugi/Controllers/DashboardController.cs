using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tsumugi.Models.Dashboard;
using Tsumugi.DummyData;

namespace Tsumugi.Controllers
{
    public class DashboardController : BaseController
    {
        public ActionResult Index()
        {
            DashboardModel m = new DashboardModel();
            m.WalletList.Add(new WalletDummy("Main", 5000));
            m.WalletList.Add(new WalletDummy("Second", 50000));
            m.WalletList.Add(new WalletDummy("Third", 50000));
            return View(m);
        }
    }
}