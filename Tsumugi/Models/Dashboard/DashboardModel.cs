using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tsumugi.Service.DummyData;

namespace Tsumugi.Models.Dashboard
{
    public class DashboardModel
    {
        public List<WalletDummy> WalletList { get; set; } = new List<WalletDummy>();
    }
}