using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using Tsumugi.Service;
using Tsumugi.Service.DummyData;

namespace Tsumugi.Models.Detail
{
    public class DetailModel
    {
        public Guid WalletID { get; set; }
        public List<TransactionListItem> TransactionList { get; set; } = new List<TransactionListItem>();
        public TsumugiDataContext DC { get; set; }

        public decimal MonthlyEarnings { get; set; } = 0;
        public decimal MonthlySpendings { get; set; } = 0;

        public string WalletName
        {
            get
            {
                if (Wallet == null) return "";
                return Wallet.Name;
            }
        }

        public bool VibeCat
        {
            get
            {
                return Wallet.Name.ToUpper() == "CAT";
            }
        }

        public Wallet Wallet
        {
            get
            {
                if (wallet == null) { wallet = DC.Wallets.Where(m => m.ID == WalletID).FirstOrDefault(); }
                return wallet;
            }
        }

        private Wallet wallet;
    }

    public class TransactionListItem
    {
        public Guid ID { get; set; }
        public Guid WalletID { get; set; }
        public string Title { get; set; }
        public string Note { get; set; }
        public DateTime Date { get; set; }
        public decimal Value { get; set; }
        public bool Type { get; set; }
        public Guid CategoryID { get; set; }

        public string TransType
        {
            get
            {
                return Type ? "plus" : "minus";
            }
        }

        public string Category
        {
            get
            {
                return "";
            }
        }

        public string ValueFormatted
        {
            get
            {
                return Type ? $"+{Value}€" : $"-{Value}€";
            }
        }

        public string DateFormatted
        {
            get
            {
                return string.Format("{0:yyyy.MM.dd}", Date);
            }
        }

        public TransactionListItem(Transaction t)
        {
            ID = t.ID;
            WalletID = t.WalletID;
            Title = t.Title;
            Note = t.Note;
            Date = t.Date;
            Value = t.Value;
            Type = t.Type;
            CategoryID = t.CategoryID ?? Guid.Empty;
        }
    }
}