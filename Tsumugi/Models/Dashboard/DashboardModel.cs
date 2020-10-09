using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tsumugi.Service.DummyData;

namespace Tsumugi.Models.Dashboard
{
    public class DashboardModel
    {
        public string EMail { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

        public bool LoginFailed { get; set; }

        public List<WalletDummy> WalletList { get; set; } = new List<WalletDummy>();
    }
}