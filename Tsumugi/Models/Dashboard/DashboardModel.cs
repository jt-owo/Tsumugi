using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tsumugi.Service;
using Tsumugi.Service.DummyData;

namespace Tsumugi.Models.Dashboard
{
    public class DashboardModel
    {
        public string EMail { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

        public bool LoginFailed { get; set; } = false;

        public List<WalletListItem> WalletList { get; set; } = new List<WalletListItem>();
    }

    public class WalletListItem
    {
        public Guid ID { get; set; }
        public Guid UserID { get; set; }
        public string Name { get; set; }
        public decimal Sum { get; set; }

        public WalletListItem(Wallet w)
        {
            ID = w.ID;
            UserID = w.UserID;
            Name = w.Name;
            Sum = w.Sum ?? 0;
        }
    }
}