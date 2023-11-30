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
    public class AdminKhachHangs_63132244Controller : Controller
    {
        Project_63132244Entities db = new Project_63132244Entities();
        // GET: Admin_63132244/KhachHangs_63132244
        [AuthFilterAttribute_63132244]
        public ActionResult Index(string searchString)
        {
            IQueryable<KhachHang> result = db.KhachHangs;
            if (!String.IsNullOrEmpty(searchString))
            {
                result = result.Where(x => x.MaKhachHang.Contains(searchString) || x.HoTen.Contains(searchString) || x.DienThoai.Contains(searchString));
            }
            if (result.Count() == 0)
            {
                ViewBag.Error = "Không có thông tin cần tìm.";
            }
            var model = result.ToList();
            ViewBag.searchString = searchString;

            return View(model);
        }

        // GET: Admin_63132244/KhachHangs_63132244/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KhachHang KhachHang = db.KhachHangs.Find(id);
            if (KhachHang == null)
            {
                return HttpNotFound();
            }
            return View(KhachHang);
        }
        // GET: Admin_63132244/KhachHangs_63132244/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KhachHang KhachHang = db.KhachHangs.Find(id);
            if (KhachHang == null)
            {
                return HttpNotFound();
            }
            return View(KhachHang);
        }

        [HttpPost]
        public ActionResult Edit([Bind(Include = "MaKhachHang,Password,HoTen,DienThoai,DiaChi,Avatar")] KhachHang khachHang, HttpPostedFileBase Avatar, string id)
        {
            // Xử lý ảnh
            if (Avatar != null)
            {
                khachHang.Avatar = Avatar.FileName;
                string path = Server.MapPath("/Images/" + Avatar.FileName);
                Avatar.SaveAs(path);
            }
            else
            {
                khachHang.Avatar = "default.png";
            }
            KhachHang updateModel = db.KhachHangs.Find(id);
            updateModel.HoTen = khachHang.HoTen;
            updateModel.DienThoai = khachHang.DienThoai;
            updateModel.Password = khachHang.Password;
            updateModel.DiaChi = khachHang.DiaChi;
            updateModel.Avatar = khachHang.Avatar;
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: Admin_63132244/KhachHangs_63132244/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KhachHang KhachHang = db.KhachHangs.Find(id);
            if (KhachHang == null)
            {
                return HttpNotFound();
            }
            return View(KhachHang);
        }

        // POST: Admin_63132244/KhachHangs_63132244/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            KhachHang KhachHang = db.KhachHangs.Find(id);
            db.KhachHangs.Remove(KhachHang);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}