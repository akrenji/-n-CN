using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WepSiteBanHang.Models;

namespace WepSiteBanHang.Controllers
{
    public class TrangAdminController : Controller
    {
        // GET: TrangAdmin
        dbQuanLyEntities db = new dbQuanLyEntities();
        public ActionResult Index()
        {
            return View();
        }
    }
}