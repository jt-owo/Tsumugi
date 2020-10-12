using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tsumugi.Service;
using Tsumugi.Service.DummyData;

namespace Tsumugi.Models.Detail
{
    public class DetailModel
    {
        public Guid WalletID { get; set; }
        public List<Transaction> TransactionList { get; set; } = new List<Transaction>();
        public TsumugiDataContext DC { get; set; }

        public string WalletName
        {
            get
            {
                if (Wallet == null) return "";
                return Wallet.Name;
            }
        }

        public Wallet Wallet
        {
            get
            {
                if(wallet == null) { wallet = DC.Wallets.Where(m => m.ID == WalletID).FirstOrDefault(); }
                return wallet;
            }
        }

        private Wallet wallet;
    }
}