using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tsumugi.Service;

namespace Tsumugi.Controllers
{
    public class BaseController : Controller
    {
        public TsumugiDataContext DC { get; set; } = new TsumugiDataContext();
    }
}