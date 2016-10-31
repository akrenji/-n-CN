using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WepSiteBanHang.Models;
namespace WepSiteBanHang.Controllers
{
    public class HomeController : Controller
    {
        dbQuanly db = new dbQuanly();
        public ActionResult Index()
        {
            //List LapTop mới nhất
            var lstLT = db.SanPhams.Where(n => n.MaLoaiSP == 3 && n.Moi == 1);
            //Gán vào ViewBag
            ViewBag.ListLTM = lstLT;
            var lstDTM = db.SanPhams.Where(n => n.MaLoaiSP == 1 && n.Moi == 1 && n.DaXoa == false);
            //Gán vào ViewBag
            ViewBag.ListDTM = lstDTM;
            var lstMTB = db.SanPhams.Where(n => n.MaLoaiSP == 2 && n.Moi == 1 && n.DaXoa == false);
            //Gán vào ViewBag
            ViewBag.ListMTBM = lstMTB;
            return View();
        }
        public ActionResult MenuPartial()
        {
            //Truy vấn lấy về 1 list các sản phẩm
            var lstSP = db.SanPhams;
            return PartialView(lstSP);
        }
        public ActionResult About()
        {
            return View();
        }



    }
}