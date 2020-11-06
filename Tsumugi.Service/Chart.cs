using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tsumugi.Service
{
    public class Chart
    {
        public static TsumugiDataContext DC { get; set; } = new TsumugiDataContext();

        public static decimal CalcEarnings(int month, List<Transaction> transactions)
        {
            DateTime firstDayOfMonth = new DateTime(DateTime.Now.Year, month, 1);
            DateTime lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

            List<Transaction> earnings = transactions.Where(a => a.Type && a.Date >= firstDayOfMonth && a.Date <= lastDayOfMonth).ToList();

            decimal ret = 0;

            foreach (Transaction transaction in earnings)
            {
                ret += transaction.Value;
            }

            return ret;
        }

        public static decimal CalcSpendings(int month, List<Transaction> transactions)
        {
            DateTime firstDayOfMonth = new DateTime(DateTime.Now.Year, month, 1);
            DateTime lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

            List<Transaction> spendings = transactions.Where(a => !a.Type && a.Date >= firstDayOfMonth && a.Date <= lastDayOfMonth).ToList();

            decimal ret = 0;

            foreach (Transaction transaction in spendings)
            {
                ret += transaction.Value;
            }

            return ret;
        }

        public static decimal CalcTrend(int currentMonth, List<Transaction> transactions)
        {
            DateTime firstDay = new DateTime(DateTime.Now.Year, currentMonth-2, 1);
            DateTime lastDay = firstDay.AddMonths(3).AddDays(-1);

            List<Transaction> spendings = transactions.Where(a => !a.Type && a.Date >= firstDay && a.Date <= lastDay).ToList();
            List<Transaction> earnings = transactions.Where(a => a.Type && a.Date >= firstDay && a.Date <= lastDay).ToList();
            decimal ret = 0;

            foreach (Transaction transaction in earnings)
            {
                ret += transaction.Value;
            }

            foreach (Transaction transaction in spendings)
            {
                ret -= transaction.Value;
            }

            if (transactions.Count != 0)
            {
                return ret / transactions.Count;
            }

            return ret;
        }
    }
}
