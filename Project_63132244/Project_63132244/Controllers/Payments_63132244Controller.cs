using Project_63132244.App_Start;
using Project_63132244.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace Project_63132244.Controllers
{
    public class Payments_63132244Controller : Controller
    {
        // GET: Payments_63132244
        Project_63132244Entities db = new Project_63132244Entities();
        public ActionResult Index()
        {
            var user = Session["taikhoan"] as KhachHang;
            if (Session["taikhoan"] == null)
            {
                return RedirectToAction("Login_User", "AccountKhachHangs_63132244");
            }
            else
            {
                List<Cart_63132244> cart = Session["cart"] as List<Cart_63132244>;
                HoaDon hoaDon = new HoaDon();
                hoaDon.Ma_HoaDon = user.MaKhachHang + "-" + DateTime.Now.ToString("yyyyMMddmmss");
                hoaDon.MaKhachHang = user.MaKhachHang;
                hoaDon.TrangThai_VanChuyen = false;
                hoaDon.TrangThai_ThanhToan = false;
                hoaDon.PhuongThuc_ThanhToan = false;
                hoaDon.NgayDat = DateTime.Now;
                hoaDon.DiaChi = user.DiaChi;
                db.HoaDons.Add(hoaDon);
                //Lưu Thông Tin vào bảng
                db.SaveChanges();
                //Lấy mã hóa đơn 
                string maHoaDon = hoaDon.Ma_HoaDon;
                // Save invoice details
                List<ChiTietHoaDon> listCTHD = new List<ChiTietHoaDon>();
                foreach (var item in cart)
                {
                    ChiTietHoaDon CTHD = new ChiTietHoaDon();
                    CTHD.Ma_HoaDon = maHoaDon;
                    CTHD.Ma_SP = item.Ma_SP;
                    CTHD.KhoiLuong = item.KhoiLuong;
                    CTHD.DonGia = item.DonGia;
                    listCTHD.Add(CTHD);
                }
                db.ChiTietHoaDons.AddRange(listCTHD);
                db.SaveChanges();
            }
            //Tiến hành xóa giỏ hàng khi đã lên đơn
            Session.Remove("cart");
            return View();
        }
    }
}