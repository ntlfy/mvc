using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project_63132244.App_Start;
using Project_63132244.Models;

namespace Project_63132244.Areas.Admin_63132244.Controllers
{
    public class AdminQuanTris_63132244Controller : Controller
    {
        Project_63132244Entities db = new Project_63132244Entities();
        // GET: Admin_63132244/AdminQuanTris_63132244
        [AuthFilterAttribute_63132244]
        public ActionResult Index(string searchString)
        {
            IQueryable<QuanTri> result = db.QuanTris;
            if (!String.IsNullOrEmpty(searchString))
            {
                result = result.Where(x =>  x.HoTen.Contains(searchString) || x.DienThoai.Contains(searchString));
            }
            if (result.Count() == 0)
            {
                ViewBag.Error = "Không có thông tin cần tìm";
            }
            var model = result.ToList();
            ViewBag.searchString = searchString;

            return View(model);
        }
        [HttpPost]
        public ActionResult UpdateIndex(string taiKhoan)
        {
            var Tk = db.QuanTris.Find(taiKhoan);
            if (Tk != null)
            {
                Tk.admin = !Tk.admin;
                db.SaveChangesAsync();
            }
            return Json(new { success = true });
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create([Bind(Include = "TaiKhoan,Password,HoTen,DienThoai,Avatar,admin")] QuanTri quantri, HttpPostedFileBase Avatar)
        {
            //xử lý file ảnh
            if (Avatar != null)
            {
                quantri.Avatar = Avatar.FileName;
                string path = Server.MapPath("/Images/" + Avatar.FileName);
                Avatar.SaveAs(path);
            }
            if(Avatar == null)
            {
                quantri.Avatar = "default.png";
            }
            if(quantri.admin == null)
            {
                quantri.admin = false;
            }
            if (ModelState.IsValid)
            {
                try
                {
                    db.QuanTris.Add(quantri);
                    db.SaveChanges();
                    ViewBag.Message = "Thêm Mới Thành Công";
                }
                catch
                {
                    ViewBag.Error = "Thêm Mới Thất Bại";
                }
            }
            else
            {
                ViewBag.Error = "Dữ liệu không hợp lệ, vui lòng kiểm tra lại.";
            }

            return View();
        }
    }
}