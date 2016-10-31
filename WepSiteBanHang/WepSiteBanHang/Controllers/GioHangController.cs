using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WepSiteBanHang.Models;
namespace WepSiteBanHang.Controllers
{
    public class GioHangController : Controller
    {
        dbQuanly db = new dbQuanly();

        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public JsonResult AddToCart(int id)
        {
            List<CartItem> listCartItem;
            //Process Add To Cart
            if (Session["ShoppingCart"] == null)
            {
                //Create New Shopping Cart Session
                listCartItem = new List<CartItem>();
                listCartItem.Add(new CartItem
                {
                    Quality = 1,
                    productOrder = db.SanPhams.Find(id)
                });
                Session["ShoppingCart"] = listCartItem;
            }
            else
            {
                bool flag = false;
                listCartItem = (List<CartItem>)Session["ShoppingCart"];
                foreach (CartItem item in listCartItem)
                {
                    if (item.productOrder.MaSP == id)
                    {
                        item.Quality++;
                        flag = true;
                        break;
                    }
                }
                if (!flag)
                    listCartItem.Add(new CartItem
                    {
                        Quality = 1,
                        productOrder = db.SanPhams.Find(id)
                    });
                Session["ShoppingCart"] = listCartItem;
            }
            //Count item in shopping cart
            int cartcount = 0;
            List<CartItem> ls = (List<CartItem>)Session["ShoppingCart"];
            foreach (CartItem item in ls)
            {
                cartcount += item.Quality;
            }
            return Json(new { ItemAmount = cartcount});
        }
        public ActionResult Giohang()
        {
            return View();
        }
        public ActionResult Update(int id,FormCollection col)
        {
            List<CartItem> ls = (List<CartItem>)Session["ShoppingCart"];
            CartItem sp = ls.SingleOrDefault(n => n.productOrder.MaSP == id);
            if(sp != null)
            { 
                sp.Quality = int.Parse(col["Quality"].ToString());
            }
       
            return RedirectToAction("Giohang");
        }
        [HttpGet]
        public ActionResult DatHang()
        {
            //Kiem tra dang nhap
            if (Session["Thanhvien"] == null || Session["Thanhvien"].ToString() == "")
            {
                return RedirectToAction("Dangnhap", "Khachhang");
            }
            if (Session["ShoppingCart"] == null)
            {
                return RedirectToAction("Index", "Home");
            }


            return View();
        }
        //Xay dung chuc nang Dathang
        [HttpPost]
        public ActionResult DatHang(FormCollection collection)
        {
            //Them Don hang
            DonDatHang ddh = new DonDatHang();
            ThanhVien kh = (ThanhVien)Session["ThanhVien"];
            List<CartItem> gh = (List<CartItem>)Session["ShoppingCart"];
            ddh.MaThanhVien = kh.MaThanhVien;
            ddh.NgayDat = DateTime.Now;
            var ngaygiao = String.Format("{0:MM/dd/yyyy}", collection["Ngaygiao"]);
            ddh.NgayGiao = DateTime.Parse(ngaygiao);
            ddh.TinhTrangGiaoHang = false;
            ddh.DaThanhToan = false;
            db.DonDatHangs.Add(ddh);
            db.SaveChanges();
            //Them chi tiet don hang            
            foreach (var item in gh)
            {
                ChiTietDonDatHang ctdh = new ChiTietDonDatHang();
                ctdh.MaDDH = ddh.MaDDH;
                ctdh.MaSP = item.productOrder.MaSP;
                ctdh.SoLuong = item.Quality;
                ctdh.DonGia = (decimal)item.productOrder.DonGia;
                db.ChiTietDonDatHangs.Add(ctdh);
            }
            db.SaveChanges();
            Session["ShoppingCart"] = null;
            return RedirectToAction("Xacnhandonhang", "Giohang");
        }
        public ActionResult Hienkh()
        {
            if (Session["Thanhvien"] == null)
            {
                return RedirectToAction("Dangnhap", "Khachhang");
            }
            else
            {   
                List<KhachHang> kh = (List<KhachHang>)Session["Thanhvien"];
             
            }
            return PartialView();
        }
        public ActionResult XoaGiohang(int id)
        {
            //Lay gio hang tu Session
            List<CartItem> gh = (List<CartItem>)Session["ShoppingCart"];
            //Kiem tra sach da co trong Session["Giohang"]
            CartItem sp = gh.SingleOrDefault(n => n.productOrder.MaSP == id);
            //Neu ton tai thi cho sua Soluong
            if (sp != null)
            {
                gh.RemoveAll(n => n.productOrder.MaSP == id);
                return RedirectToAction("GioHang");

            }
            if (gh.Count == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("GioHang");
        }
        //Xoa tat ca thong tin trong Gio hang
        public ActionResult XoaTatcaGiohang()
        {
            //Lay gio hang tu Session
            List<CartItem> gh = (List<CartItem>)Session["ShoppingCart"];
            gh.Clear();
            return RedirectToAction("Index", "Home");
        }

    }
}