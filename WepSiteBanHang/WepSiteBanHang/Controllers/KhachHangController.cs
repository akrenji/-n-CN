using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WepSiteBanHang.Models;
namespace WepSiteBanHang.Controllers
{
    public class KhachHangController : Controller
    {
        dbQuanLyEntities db = new dbQuanLyEntities();
        // GET: KhachHang
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult DangKy()
        {
            return PartialView();

        }
        [HttpPost]
        public ActionResult Dangky(FormCollection col, ThanhVien tv)
        {
            var ten = col["Username"];
            var matkhau = col["Password"];
            var Email = col["Email"];
            var dt = col["Phone"];
            var hoten = col["hoten"];
            var diachi = col["DiaChi"];
            tv.TaiKhoan = ten;
            tv.MatKhau = matkhau;
            tv.Email = Email;
            tv.SoDienThoai = dt;
            tv.HoTen = hoten;
            tv.DiaChi = diachi;
            db.ThanhViens.Add(tv);
            db.SaveChanges();
            return this.DangKy();
        }
        [HttpGet]
        public ActionResult Dangnhap()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Dangnhap(FormCollection collection)
        {
            // Gán các giá trị người dùng nhập liệu cho các biến 
            var tendn = collection["User"];
            var matkhau = collection["Pass"];

            if (String.IsNullOrEmpty(tendn))
            {
                ViewData["Loi1"] = "Phải nhập tên đăng nhập hoặc mật khẩu sai";
            }
            else if (String.IsNullOrEmpty(matkhau))
            {
                ViewData["Loi2"] = "Phải nhập tên đăng nhập hoặc mật khẩu sai";
            }

            else
            {
                //Gán giá trị cho đối tượng được tạo mới (kh)

                ThanhVien tv = db.ThanhViens.First(n => n.TaiKhoan == tendn && n.MatKhau == matkhau);

                if (tv != null)
                {
                    // ViewBag.Thongbao = "Chúc mừng đăng nhập thành công";
                    Session["Thanhvien"] = tv;
              
                    return RedirectToAction("Index", "Home");
                }
                else
                    ViewBag.Thongbao = "Tên đăng nhập hoặc mật khẩu không đúng";
            }
            return View();
        }
    }
}