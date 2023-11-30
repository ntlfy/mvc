using Project_63132244.App_Start;
using Project_63132244.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
namespace Project_63132244.Areas.Admin_63132244.Controllers
{
    public class AdminSanPhams_63132244Controller : Controller
    {
        Project_63132244Entities db = new Project_63132244Entities();
        [AuthFilterAttribute_63132244]
        public ActionResult Index(string searchString)
        {
            IQueryable<Sanpham> result = db.Sanphams;
            if (!String.IsNullOrEmpty(searchString))
            {
                result = result.Where(x => x.Loai_SP.Contains(searchString) || x.Ten_SP.Contains(searchString));
            }
            if (result.Count() == 0)
            {
                ViewBag.Error = "Không có thông tin cần tìm.";
            }
            var model = result.ToList();
            ViewBag.searchString = searchString;

            return View(model);
        }

        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sanpham sanpham = db.Sanphams.Find(id);
            if (sanpham == null)
            {
                return HttpNotFound();
            }
            return View(sanpham);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create([Bind(Include = "Ma_SP,Loai_SP,Ten_SP,XuatXu,DonGia,KhoiLuong,MoTa,AnhSP")] Sanpham model, HttpPostedFileBase Avatar)
        {
            //xử lý file ảnh
            if (Avatar != null)
            {
                model.AnhSP = Avatar.FileName;
                string path = Server.MapPath("/Images/" + Avatar.FileName);
                Avatar.SaveAs(path);
            }
            if(model.Ma_SP == null || ModelState.IsValid)
            {
                ViewBag.Error = "Sai rồi! Vui lòng kiểm tra lại Mã sản phẩm vì nó không được trùng";
            }
            else
            {
                db.Sanphams.Add(model);
                db.SaveChanges();
                ViewBag.Message = "Thêm Thành Công";
            }
            return View(model);
        }
        public ActionResult Edit(string id)
        {
            Session["currentPage"] = Request.Url.ToString();
            Sanpham model = db.Sanphams.Find(id);
            return View(model);
        }
        [HttpPost]
        public ActionResult Edit([Bind(Include = "Loai_SP,Ten_SP,XuatXu,DonGia,KhoiLuong,MoTa,AnhSP")] HttpPostedFileBase Avatar, Sanpham sanPham, string id)
        {
            // Xử lý ảnh
            if (Avatar != null)
            {
                sanPham.AnhSP = Avatar.FileName;
                string path = Server.MapPath("/Images/" + Avatar.FileName);
                Avatar.SaveAs(path);
            }
            Sanpham updateModel = db.Sanphams.Find(id);
            updateModel.Ten_SP = sanPham.Ten_SP;
            updateModel.Loai_SP = sanPham.Loai_SP;
            updateModel.XuatXu = sanPham.XuatXu;
            updateModel.DonGia = sanPham.DonGia;
            updateModel.KhoiLuong = sanPham.KhoiLuong;
            updateModel.MoTa = sanPham.MoTa;
            updateModel.AnhSP = sanPham.AnhSP;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Admin_63132244/Sanphams_6313224/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sanpham sanpham = db.Sanphams.Find(id);
            if (sanpham == null)
            {
                return HttpNotFound();
            }
            return View(sanpham);
        }

        // POST: Admin_63132244/Sanphams_6313224/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Sanpham sanpham = db.Sanphams.Find(id);
            db.Sanphams.Remove(sanpham);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}