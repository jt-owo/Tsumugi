using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Tsumugi.Service
{
    public static class TsumugiUser
    {
        public static bool IsLoggedOn
        {
            get
            {
                return UserID.HasValue;
            }
        }

        public static Guid? UserID
        {
            get => HttpContext.Current.Application["UserID"] as Guid?;
            set
            {
                HttpContext.Current.Application["UserID"] = value;
            }
        }
    }
}
