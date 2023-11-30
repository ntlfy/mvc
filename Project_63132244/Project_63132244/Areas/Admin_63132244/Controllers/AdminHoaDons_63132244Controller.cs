using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Project_63132244.App_Start;
using Project_63132244.Models;

namespace Project_63132244.Areas.Admin_63132244.Controllers
{
    public class AdminHoaDons_63132244Controller : Controller
    {
        Project_63132244Entities db = new Project_63132244Entities();
        // GET: Admin_63132244/AdminHoaDons_63132244
        [AuthFilterAttribute_63132244]
        public ActionResult Index(string searchString)
        {
            IQueryable<HoaDon> result = db.HoaDons;
            if (!String.IsNullOrEmpty(searchString))
            {
                result = result.Where(x => x.MaKhachHang.Contains(searchString) || x.Ma_HoaDon.Contains(searchString));
            }
            if (result.Count() == 0)
            {
                ViewBag.Error = "Khong có thông tin cần tìm.";
            }
            var model = result.ToList();
            ViewBag.searchString = searchString;

            return View(model);
        }
        [HttpPost]
        public ActionResult UpdateSTT(string id)
        {
            var idMKH =  db.HoaDons.Find(id);
            if (idMKH != null)
            {
                idMKH.TrangThai_VanChuyen = true;
                idMKH.TrangThai_ThanhToan = true;
                idMKH.NgayGiao = DateTime.Now;
                db.SaveChangesAsync();
            }
            return Json(new { success = true});
        }
        public ActionResult Details(string maHoaDon)
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
        public ActionResult Edit(string id)
        {
            HoaDon HoaDon = db.HoaDons.Find(id);
            return View(HoaDon);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaKhachHang,TrangThai_VanChuyen,TrangThai_ThanhToan,PhuongThuc_ThanhToan,NgayDat,NgayGiao,DiaChi")] HoaDon HoaDon, string id)
        {
            if (ModelState.IsValid)
            {
                HoaDon updateModel = db.HoaDons.Find(id);
                updateModel.PhuongThuc_ThanhToan = HoaDon.PhuongThuc_ThanhToan;
                updateModel.NgayGiao = HoaDon.NgayGiao;
                updateModel.DiaChi = HoaDon.DiaChi;
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(HoaDon);
        }
    }
}