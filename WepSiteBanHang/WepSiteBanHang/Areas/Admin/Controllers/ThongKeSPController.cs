using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WepSiteBanHang.Models;
using Newtonsoft.Json;

namespace WepSiteBanHang.Areas.Admin.Controllers
{
    public class ThongKeSPController : Controller
    {
        dbQuanLyEntities db = new dbQuanLyEntities();
        // GET: Admin/ThongKeSP
        public ActionResult Index()
        {
            ViewBag.SoNguoiTruyCap = HttpContext.Application["SoNguoiTruyCap"].ToString();//Số lượng người truy cập từ application đã được tạo
            ViewBag.SoLuongNguoiOnline = HttpContext.Application["SoNguoiDangOnline"].ToString();//Lấy số lượng người đang truy cập
            ViewBag.TongDoanhThu = ThongKeTongDoanhThu();//Thống kê tổng doanh thu
            ViewBag.TongDDH = ThongKeDonHang();//Thống kê dơn hàng
            ViewBag.TongThanhVien = ThongKeThanhVien();//Thống kê thành viên
            return View();
        }
        public decimal ThongKeTongDoanhThu()
        {
            //Thống kê theo tất cả doanh thu
            decimal TongDoanhThu = db.ChiTietDonDatHangs.Sum(n => n.SoLuong * n.DonGia).Value;
            return TongDoanhThu;
        }
        public double ThongKeDonHang()
        {
            //Dếm đơn đặt hàng
            double slDDH = db.DonDatHangs.Count();
            return slDDH;
        }
        public double ThongKeThanhVien()
        {
            //Dếm đơn đặt hàng
            double slTV = db.ThanhViens.Count();
            return slTV;
        }
        public ActionResult GetReport()
        {
            int year = DateTime.Now.Year;
            int month = DateTime.Now.Month;
            List<object> data = new List<object>();
            var days = getAllDates(year, month);
            foreach (var day in days)
            {
                String daylabel = day.ToString("dd/MM/yyyy");
                var ss = db.ChiTietDonDatHangs.SingleOrDefault(n => n.DonDatHang.NgayDat == DateTime.Parse(daylabel));
                var sum = (from dh in db.DonDatHangs
                           join ct in db.ChiTietDonDatHangs on dh.MaDDH equals ct.MaDDH
                           join sp in db.SanPhams on ct.MaSP equals sp.MaSP

                           group ct by ct.MaSP into c
                           select new
                           {
                               y = c.Sum(n => n.SoLuong),
                               label = c.Sum(n => n.SoLuong),
                           }).FirstOrDefault();

                if (sum != null)
                {
                    data.Add(sum);
                }
                else
                {
                    data.Add(new
                    {
                        y = 0,
                        label = daylabel
                    });
                }

            }
            return Content(JsonConvert.SerializeObject(data), "application/json");
        }

        public List<DateTime> getAllDates(int year, int month)
        {
            var ret = new List<DateTime>();
            for (int i = 1; i <= DateTime.DaysInMonth(year, month); i++)
            {
                ret.Add(new DateTime(year, month, i));
            }
            return ret;
        }


    }

}