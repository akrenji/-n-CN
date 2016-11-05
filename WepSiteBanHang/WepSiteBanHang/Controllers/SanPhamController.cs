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
        public ActionResult XemChiTiet(int ?id)
        {
            if(id==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
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



    }


}