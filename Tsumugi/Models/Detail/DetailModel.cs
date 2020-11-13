using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Dynamic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tsumugi.Service;

namespace Tsumugi.Models.Detail
{
    public class DetailModel
    {
        public Guid WalletID { get; set; }
        public List<TransactionListItem> TransactionList { get; set; } = new List<TransactionListItem>();
        public TsumugiDataContext DC { get; set; } = new TsumugiDataContext();

        #region CHART DATA

        public decimal MonthlyEarnings { get; set; } = 0;
        public decimal MonthlySpendings { get; set; } = 0;
        public decimal Trend { get; set; } = 0;

        #endregion

        #region NEW / DELETE TRANSACTION

        [DataType(DataType.Date), Required]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        public bool Type { get; set; } = false;
        public string Title { get; set; }
        public string Note { get; set; }
        public string Value { get; set; }
        public Guid? CategoryID { get; set; } = null;
        public SelectList CategoryOptions { get; set; }

        public Guid? DeleteTransactionID { get; set; }

        #endregion

        #region DISPLAY PROPERTIES

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

        public string CurrentMonthLabel
        {
            get
            {
                return new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToString("MMM", CultureInfo.InvariantCulture);
            }
        }

        public string NextMonthLabel
        {
            get
            {
                return new DateTime(DateTime.Now.AddMonths(1).Year, DateTime.Now.AddMonths(1).Month, 1).ToString("MMM", CultureInfo.InvariantCulture);
            }
        }

        public string AfterNextMonthLabel
        {
            get
            {
                return new DateTime(DateTime.Now.AddMonths(2).Year, DateTime.Now.AddMonths(2).Month, 1).ToString("MMM", CultureInfo.InvariantCulture);
            }
        }

        #endregion

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

        public string CategoryFormatted
        {
            get
            {
                TsumugiDataContext DC = new TsumugiDataContext();
                return DC.Categories.Where(m => m.ID == CategoryID).Select(n => n.Name).FirstOrDefault();
            }
        }

        public TransactionListItem(Transaction t)
        {
            ID = t.ID;
            WalletID = t.WalletID;
            Title = t.Title;
            Note = t.Note ?? "-";
            Date = t.Date;
            Value = t.Value;
            Type = t.Type;
            CategoryID = t.CategoryID ?? Guid.Empty;
        }
    }
}