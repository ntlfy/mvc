using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project_63132244.Models;
using PagedList;
using PagedList.Mvc;
using Project_63132244.App_Start;

namespace Project_63132244.Controllers
{
    public class SanPhams_63132244Controller : Controller
    {
        Project_63132244Entities db = new Project_63132244Entities();
        // GET: SanPhams_63132244
        public ActionResult Index(int? page, string searchString)
        {

            // Kết quả tìm kiếm
            IQueryable<Sanpham> result = db.Sanphams;

            if (!String.IsNullOrEmpty(searchString))
            {
                result = db.Sanphams.Where(x => x.Loai_SP.Contains(searchString) || x.Ten_SP.Contains(searchString));
            }
            if(result.Count() == 0)
            {
                ViewBag.Error = "Sản phẩm đã hết hoặc không kinh doanh ! Vui Lòng đóng góp cho chúng tôi qua phần liên hệ . ";
            }

            // Phân trang
            int pageSize = 15;
            int pageNumber = (page ?? 1);
            PagedList<Sanpham> model = new PagedList<Sanpham>(result.ToList(), pageNumber, pageSize);
            ViewBag.searchString = searchString;

            return View(model);

        }
        public ActionResult ChiTietSanPham(string id)
        {
            var sanPham = db.Sanphams.Where(x=>x.Ma_SP == id).FirstOrDefault();
            return View(sanPham);
        }
        public JsonResult AddCart(string Ma_SP , string ghiChu)
        {
            if (Session["cart"] == null)
            {
                Session["cart"] = new List<Cart_63132244>();
            }

            List<Cart_63132244> giohang = Session["cart"] as List<Cart_63132244>;

            Cart_63132244 cartItem = giohang.FirstOrDefault(item => item.Ma_SP == Ma_SP);

            if (cartItem == null)
            {
                Sanpham sp = db.Sanphams.FirstOrDefault(p => p.Ma_SP == Ma_SP);

                if (sp != null)
                {
                    Cart_63132244 newItem = new Cart_63132244()
                    {
                        Anh_SP = sp.AnhSP,
                        Ma_SP = sp.Ma_SP,
                        Ten_SP = sp.Ten_SP,
                        DonGia = (int)sp.DonGia,
                        KhoiLuong = 1
                    };

                    giohang.Add(newItem);
                }
            }
            else
            {
                cartItem.KhoiLuong++;
            }
            return Json(new { success = true });
        }
        public ActionResult EditCart(string Ma_SP , int khoiLuongMoi)
        {
            List<Cart_63132244> cart = Session["cart"] as List<Cart_63132244>;
            Cart_63132244 editItem = cart.FirstOrDefault(m => m.Ma_SP == Ma_SP);
            if (editItem != null)
            {
                editItem.KhoiLuong = khoiLuongMoi;
            }
            return RedirectToAction("Cart");
        }
        public ActionResult XoaKhoiGio(string Ma_SP)
        {
            List<Cart_63132244> cart = Session["cart"] as List<Cart_63132244>;
            Cart_63132244 deleteItem = cart.FirstOrDefault(m => m.Ma_SP == Ma_SP);
            if (deleteItem != null)
                if (deleteItem != null)
            {
                cart.Remove(deleteItem);
            }
            return Json(new { success = true });
        }
        public ActionResult Cart()
        {
            List<Cart_63132244> cart = Session["cart"] as List<Cart_63132244>;
            return View(cart);
        }
    }
}