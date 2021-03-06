﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WepSiteBanHang.Models;

namespace WepSiteBanHang.Areas.Admin.Controllers
{
    public class NhaCungCapsController : Controller
    {
        private dbQuanLyEntities db = new dbQuanLyEntities();

        // GET: Admin/NhaCungCaps
        public ActionResult Index()
        {

            ViewBag.SoNguoiTruyCap = HttpContext.Application["SoNguoiTruyCap"].ToString();//Số lượng người truy cập từ application đã được tạo
            ViewBag.SoLuongNguoiOnline = HttpContext.Application["SoNguoiDangOnline"].ToString();//Lấy số lượng người đang truy cập
            ViewBag.TongDoanhThu = ThongKeTongDoanhThu();//Thống kê tổng doanh thu
            ViewBag.TongDDH = ThongKeDonHang();//Thống kê dơn hàng
            ViewBag.TongThanhVien = ThongKeThanhVien();//Thống kê thành viên

            return View(db.NhaCungCaps.ToList());
        }

        // GET: Admin/NhaCungCaps/Details/5
        public ActionResult Details(int? id)
        {

            ViewBag.SoNguoiTruyCap = HttpContext.Application["SoNguoiTruyCap"].ToString();//Số lượng người truy cập từ application đã được tạo
            ViewBag.SoLuongNguoiOnline = HttpContext.Application["SoNguoiDangOnline"].ToString();//Lấy số lượng người đang truy cập
            ViewBag.TongDoanhThu = ThongKeTongDoanhThu();//Thống kê tổng doanh thu
            ViewBag.TongDDH = ThongKeDonHang();//Thống kê dơn hàng
            ViewBag.TongThanhVien = ThongKeThanhVien();//Thống kê thành viên
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NhaCungCap nhaCungCap = db.NhaCungCaps.Find(id);
            if (nhaCungCap == null)
            {
                return HttpNotFound();
            }
            return View(nhaCungCap);
        }

        // GET: Admin/NhaCungCaps/Create
        public ActionResult Create()
        {
            ViewBag.SoNguoiTruyCap = HttpContext.Application["SoNguoiTruyCap"].ToString();//Số lượng người truy cập từ application đã được tạo
            ViewBag.SoLuongNguoiOnline = HttpContext.Application["SoNguoiDangOnline"].ToString();//Lấy số lượng người đang truy cập
            ViewBag.TongDoanhThu = ThongKeTongDoanhThu();//Thống kê tổng doanh thu
            ViewBag.TongDDH = ThongKeDonHang();//Thống kê dơn hàng
            ViewBag.TongThanhVien = ThongKeThanhVien();//Thống kê thành viên
            return View();
        }

        // POST: Admin/NhaCungCaps/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaNCC,TenNCC,DiaChi,Email,DienThoai,Fax")] NhaCungCap nhaCungCap)
        {
            ViewBag.SoNguoiTruyCap = HttpContext.Application["SoNguoiTruyCap"].ToString();//Số lượng người truy cập từ application đã được tạo
            ViewBag.SoLuongNguoiOnline = HttpContext.Application["SoNguoiDangOnline"].ToString();//Lấy số lượng người đang truy cập
            ViewBag.TongDoanhThu = ThongKeTongDoanhThu();//Thống kê tổng doanh thu
            ViewBag.TongDDH = ThongKeDonHang();//Thống kê dơn hàng
            ViewBag.TongThanhVien = ThongKeThanhVien();//Thống kê thành viên
            if (ModelState.IsValid)
            {
                db.NhaCungCaps.Add(nhaCungCap);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(nhaCungCap);
        }

        // GET: Admin/NhaCungCaps/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.SoNguoiTruyCap = HttpContext.Application["SoNguoiTruyCap"].ToString();//Số lượng người truy cập từ application đã được tạo
            ViewBag.SoLuongNguoiOnline = HttpContext.Application["SoNguoiDangOnline"].ToString();//Lấy số lượng người đang truy cập
            ViewBag.TongDoanhThu = ThongKeTongDoanhThu();//Thống kê tổng doanh thu
            ViewBag.TongDDH = ThongKeDonHang();//Thống kê dơn hàng
            ViewBag.TongThanhVien = ThongKeThanhVien();//Thống kê thành viên
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NhaCungCap nhaCungCap = db.NhaCungCaps.Find(id);
            if (nhaCungCap == null)
            {
                return HttpNotFound();
            }
            return View(nhaCungCap);
        }

        // POST: Admin/NhaCungCaps/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaNCC,TenNCC,DiaChi,Email,DienThoai,Fax")] NhaCungCap nhaCungCap)
        {
            ViewBag.SoNguoiTruyCap = HttpContext.Application["SoNguoiTruyCap"].ToString();//Số lượng người truy cập từ application đã được tạo
            ViewBag.SoLuongNguoiOnline = HttpContext.Application["SoNguoiDangOnline"].ToString();//Lấy số lượng người đang truy cập
            ViewBag.TongDoanhThu = ThongKeTongDoanhThu();//Thống kê tổng doanh thu
            ViewBag.TongDDH = ThongKeDonHang();//Thống kê dơn hàng
            ViewBag.TongThanhVien = ThongKeThanhVien();//Thống kê thành viên
            if (ModelState.IsValid)
            {
                db.Entry(nhaCungCap).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(nhaCungCap);
        }

        // GET: Admin/NhaCungCaps/Delete/5
        public ActionResult Delete(int? id)
        {
            ViewBag.SoNguoiTruyCap = HttpContext.Application["SoNguoiTruyCap"].ToString();//Số lượng người truy cập từ application đã được tạo
            ViewBag.SoLuongNguoiOnline = HttpContext.Application["SoNguoiDangOnline"].ToString();//Lấy số lượng người đang truy cập
            ViewBag.TongDoanhThu = ThongKeTongDoanhThu();//Thống kê tổng doanh thu
            ViewBag.TongDDH = ThongKeDonHang();//Thống kê dơn hàng
            ViewBag.TongThanhVien = ThongKeThanhVien();//Thống kê thành viên
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NhaCungCap nhaCungCap = db.NhaCungCaps.Find(id);
            if (nhaCungCap == null)
            {
                return HttpNotFound();
            }
            return View(nhaCungCap);
        }

        // POST: Admin/NhaCungCaps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ViewBag.SoNguoiTruyCap = HttpContext.Application["SoNguoiTruyCap"].ToString();//Số lượng người truy cập từ application đã được tạo
            ViewBag.SoLuongNguoiOnline = HttpContext.Application["SoNguoiDangOnline"].ToString();//Lấy số lượng người đang truy cập
            ViewBag.TongDoanhThu = ThongKeTongDoanhThu();//Thống kê tổng doanh thu
            ViewBag.TongDDH = ThongKeDonHang();//Thống kê dơn hàng
            ViewBag.TongThanhVien = ThongKeThanhVien();//Thống kê thành viên
            NhaCungCap nhaCungCap = db.NhaCungCaps.Find(id);
            db.NhaCungCaps.Remove(nhaCungCap);
            db.SaveChanges();
            return RedirectToAction("Index");
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
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
