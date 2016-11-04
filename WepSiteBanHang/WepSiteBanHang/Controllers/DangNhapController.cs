using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WepSiteBanHang.Models;

namespace WepSiteBanHang.Controllers
{
    public class DangNhapController : Controller
    {
        // GET: DangNhap
        dbQuanLyEntities db = new dbQuanLyEntities();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult DangNhap()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangNhap(FormCollection collection)
        {
            var tk = collection["taikhoan"];
            var mk = collection["password"];
            if (String.IsNullOrEmpty(tk))
            {
                ViewData["Loi1"] = "Chưa nhập tài khoản";
            }
            else
            {
                if (String.IsNullOrEmpty(mk))
                {
                    ViewData["Loi2"] = "Chưa nhập mật khẩu PASSS";
                }
                else
                {
                    QuanLy ad = db.QuanLies.SingleOrDefault(m => m.TenTK == tk && m.Pass == mk);
                    if (ad != null)
                    {
                        Session["Admin"] = ad;
                        return RedirectToAction("Index", "TrangAdmin");
                    }
                    else
                    {
                        ViewData["Loi2"] = "Tài khoản hoặc mật khẩu sai";
                    }
                }
            }

            return View();
        }
        public ActionResult HienThiTK()
        {
            var ad = (QuanLy)Session["Admin"];
            return PartialView(ad);
        }
        public ActionResult DangXuat()
        {
            Session["Admin"] = null;
            return RedirectToAction("DangNhap", "DangNhap");
        }
    }
}