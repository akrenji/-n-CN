﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WepSiteBanHang.Models;
using Newtonsoft.Json;
namespace WepSiteBanHang.Areas.Admin.Controllers
{
    public class ThongKeController : Controller
    {
        dbQuanLyEntities db = new dbQuanLyEntities();
        // GET: Admin/ThongKe
        public ActionResult ThongKeTongDoanhThuThang()
        {
            ViewBag.SoNguoiTruyCap = HttpContext.Application["SoNguoiTruyCap"].ToString();//Số lượng người truy cập từ application đã được tạo
            ViewBag.SoLuongNguoiOnline = HttpContext.Application["SoNguoiDangOnline"].ToString();//Lấy số lượng người đang truy cập
            ViewBag.TongDoanhThu = ThongKeTongDoanhThu();//Thống kê tổng doanh thu
            ViewBag.TongDDH = ThongKeDonHang();//Thống kê dơn hàng
            ViewBag.TongThanhVien = ThongKeThanhVien();//Thống kê thành viên
            //
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
            var days = getAllDates(year,month);
            foreach(var day in days){
                String daylabel = day.ToString("dd/MM/yyyy");
                var sum = (from dh in db.DonDatHangs
                           join ct in db.ChiTietDonDatHangs on dh.MaDDH equals ct.MaDDH
                           where dh.NgayDat.Value.Year == year && dh.NgayDat.Value.Month == month
                           && dh.NgayDat.Value.Day == day.Day && dh.DaThanhToan.Value == true
                           group ct by ct.MaDDH into c
                           select new { y = c.Sum(a => a.SoLuong) * c.Sum(a => a.DonGia), label = daylabel }).FirstOrDefault();

                if (sum != null) {
                    data.Add(sum);
                } else
                {
                    data.Add(new {
                        y = 0,
                        label = daylabel
                    });
                }
              
            }
            return Content(JsonConvert.SerializeObject(data),"application/json");
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
        public ActionResult ThongKeNguoiXem()
        {
            
            return View();

        }
       

    }
}