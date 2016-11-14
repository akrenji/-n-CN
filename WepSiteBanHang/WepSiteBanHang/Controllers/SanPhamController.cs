using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WepSiteBanHang.Models;
using PagedList;
namespace WepSiteBanHang.Controllers
{
    public class SanPhamController : Controller
    {
        dbQuanLyEntities db = new dbQuanLyEntities();
        // GET: SanPham
        public ActionResult SanPhamStyle1Partial()
        {

            return PartialView();
        }
        public ActionResult SanPhamStyle2Partial()
        {

            return PartialView();
        }
        public ActionResult XemChiTiet(int ?id)
        {
            SanPham s = db.SanPhams.SingleOrDefault(n => n.MaSP == id);
            if (id==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                s.LuotXem++;
                UpdateModel(s);
                db.SaveChanges();
            }
            SanPham sp = db.SanPhams.SingleOrDefault(n => n.MaSP == id && n.DaXoa==false);
            if(sp==null)
            {
                return HttpNotFound();
            }
            return View(sp);
        }
        public ActionResult SanPham(int? MaLoaiSP, int? MaNSX, int? page)
        {
            //if (Session["TaiKhoan"] == null || Session["TaiKhoan"].ToString() == "")
            //{
            //    return RedirectToAction("Index","Home");
            //}

            if (MaLoaiSP == null || MaNSX == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            /*Load sản phẩm dựa theo 2 tiêu chí là Mã loại sản phẩm và mã nhà sản xuất (2 trường
            trong bảng sản phẩm */
            var lstSP = db.SanPhams.Where(n => n.MaLoaiSP == MaLoaiSP && n.MaNSX == MaNSX);
            if (lstSP.Count() == 0)
            {
                //Thông báo nếu như không có sản phẩm đó
                return HttpNotFound();
            }
            //Thực hiện chức năng phân trang
            //Tạo biến số sản phẩm trên trang
            int PageSize = 6;
            //Tạo biến thứ 2: Số trang hiện tại
            
            int PageNumber = (page ?? 1);
            ViewBag.MaLoaiSP = MaLoaiSP;
            ViewBag.MaNSX = MaNSX;
        
            return View(lstSP.OrderBy(n => n.MaSP).ToPagedList(PageNumber, PageSize));
        }
        public ActionResult SanPhamLienQuan(int id)
        {
            var sp = db.SanPhams.SingleOrDefault(n=>n.MaSP==id);
            var loai = db.SanPhams.Where(n=>n.LoaiSanPham.MaLoaiSP==sp.MaLoaiSP).ToList();
            return PartialView(loai);
        }
        public ActionResult T10Theodoi()
        {
            var sp = db.SanPhams.OrderByDescending(n => n.LuotXem);
            return PartialView(sp);
        }
          protected void SendMail()
        {
            ThanhVien t = (ThanhVien)Session["Thanhvien"];
            var tv = db.ThanhViens.Single(n => n.MaThanhVien == t.MaThanhVien);
            // Email Address from where you send the mail
            var fromAddress = "sulo2020@yahoo.com.vn";
            // any address where the email will be sending
            var toAddress = tv.Email;
            //Password of your Email address
            const string fromPassword = "7418495869io";
            // Passing the values and make a email formate to display
            string subject = "Đơn hàng từ công ty Luxuxy";
            string body = "From: Đơn hàng";
            // smtp settings
            var smtp = new System.Net.Mail.SmtpClient();
            {
                smtp.Host = "mail.tenmiencuaban.comm";
                smtp.Port = 25;
                smtp.EnableSsl = false;
                smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                smtp.Credentials = new NetworkCredential(fromAddress, fromPassword);
                smtp.Timeout = 20000;
            }
            // Passing values to smtp object
            smtp.Send(fromAddress, toAddress, subject, body);
           
        }
    }


}