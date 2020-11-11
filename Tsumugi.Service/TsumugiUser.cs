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
        /// <summary>
        /// Checks if a user is logged on
        /// </summary>
        public static bool IsLoggedOn
        {
            get
            {
                return UserID.HasValue;
            }
        }

        /// <summary>
        /// Saves the userID in the session
        /// </summary>
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
