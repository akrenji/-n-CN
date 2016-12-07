﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WepSiteBanHang.Models;
using System.Net;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Text;

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
        [HttpPost]
        private bool IsValidRecaptcha(string recaptcha)
        {
            if (string.IsNullOrEmpty(recaptcha))
            {
                return false;
            }
            var secretKey = "6LcA1wsUAAAAALcZIOvG5WltgIjqaf-nkLJCypnv";//Mã bí mật
            string remoteIp = Request.ServerVariables["REMOTE_ADDR"];
            string myParameters = String.Format("secret={0}&response={1}&remoteip={2}", secretKey, recaptcha, remoteIp);
            RecaptchaResult captchaResult;
            using (var wc = new WebClient())
            {
                wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                var json = wc.UploadString("https://www.google.com/recaptcha/api/siteverify", myParameters);
                var js = new DataContractJsonSerializer(typeof(RecaptchaResult));
                var ms = new MemoryStream(Encoding.ASCII.GetBytes(json));
                captchaResult = js.ReadObject(ms) as RecaptchaResult;
                if (captchaResult != null && captchaResult.Success)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        [HttpGet]
        public ActionResult DangKy()
        {
            return PartialView();

        }


        [HttpPost]
        public ActionResult Dangky(FormCollection col, ThanhVien tv)
        {
            if (IsValidRecaptcha(Request["g-recaptcha-response"]))
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
            else
            {
                ViewBag.Status = "Không hợp lệ";
                return View();
            }


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
        public ActionResult Thoattk()
        {
            Session["Thanhvien"] = null;
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public ActionResult Xacnhanemail()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Xacnhanemail(FormCollection collection)
        {
            var ten = collection["tentk"];
            var email = collection["xnemail"];
            SendMail(ten, email);
            return RedirectToAction("Index", "Home");
        }
        protected void SendMail(string ten, string email)
        {
            ThanhVien tv = db.ThanhViens.First(n => n.TaiKhoan == ten && n.Email == email);
            var fromAddress = "sulo42229@gmail.com";
            // any address where the email will be sending
            var toAddress = email.ToString();
            //Password of your Email address
            const string fromPassword = "nhan2066";
            // Passing the values and make a email formate to display
            string subject = "Bạn đã yêu cầu tìm lại mật khẩu";
            string body = "Mật khẩu của bạn " + tv.MatKhau + "\n";
            // smtp settings
            var smtp = new System.Net.Mail.SmtpClient();
            {
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                smtp.Credentials = new NetworkCredential(fromAddress, fromPassword);
                smtp.Timeout = 20000;
            }
            // Passing values to smtp object
            smtp.Send(fromAddress, toAddress, subject, body);
        }
    }
}