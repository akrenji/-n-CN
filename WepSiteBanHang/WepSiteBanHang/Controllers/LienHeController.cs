using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WepSiteBanHang.Models;
namespace WepSiteBanHang.Controllers
{
    
    public class LienHeController : Controller
    {
        // GET: LienHe
        dbQuanLyEntities db = new dbQuanLyEntities();
        public ActionResult LienHe()
        {
            var lh = db.LienHes.Single(n => n.TrangThai == true);
      
            return View(lh);
        }
        [HttpPost]
        public ActionResult LienHe(FormCollection colection,LienHe lh)
        {

            var hoten = colection["HOTEN"];
            var dienthoai = colection["DIENTHOAI"];
            var diachi = colection["DIACHI"];
            var email = colection["EMAIL"];
            var yeucau = colection["YEUCAU"];
            lh.HoTen = hoten;
            lh.DienThoai = dienthoai;
            lh.DiaChi = diachi;
            lh.Email = email;
            lh.YeuCau = yeucau;
            db.LienHes.Add(lh);
            db.SaveChanges();
            return View(lh);

        }



    }
}