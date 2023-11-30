using Project_63132244.App_Start;
using Project_63132244.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Project_63132244.Controllers
{
    public class HoaDons_63132244Controller : Controller
    {
        Project_63132244Entities db = new Project_63132244Entities();
        // GET: HoaDons_63132244
        public ActionResult GetHoaDon()
        {
            if (Session["taikhoan"] == null)
            {
                return RedirectToAction("Login_User" , "AccountKhachHangs_63132244");
            }
            var user = Session["taikhoan"] as KhachHang;
            var danhSachHoaDon = db.HoaDons.Where(hd => hd.MaKhachHang == user.MaKhachHang).ToList();
            ViewBag.DanhSachHoaDon = danhSachHoaDon;
            return View();
        }
        public ActionResult ViewChiTietHoaDon(string maHoaDon)
        {
            if (string.IsNullOrEmpty(maHoaDon))
            {
                // Xử lý khi maHoaDon không hợp lệ
                return RedirectToAction("Index");
            }
            // Lấy danh sách chi tiết hóa đơn từ CSDL dựa trên mã hóa đơn
            List<ChiTietHoaDon> chiTietHoaDonList = db.ChiTietHoaDons.Where(cthd => cthd.Ma_HoaDon == maHoaDon).ToList();
            return View(chiTietHoaDonList);
        }

    }
}