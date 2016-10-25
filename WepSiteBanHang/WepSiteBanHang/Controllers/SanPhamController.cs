using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WepSiteBanHang.Models;
namespace WepSiteBanHang.Controllers
{
    public class SanPhamController : Controller
    {
        dbQuanly db = new dbQuanly();
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
        public ActionResult SanPham(int ? MaLoaiSP, int? MaNSX)
        {
            if(MaLoaiSP == null || MaNSX== null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
           
            var lstSP = db.SanPhams.Where(n => n.MaLoaiSP == MaLoaiSP && n.MaNSX == MaNSX);
            if (lstSP.Count()==0)
            {
                return HttpNotFound();
            }
                return View(lstSP);
        }
    }


}