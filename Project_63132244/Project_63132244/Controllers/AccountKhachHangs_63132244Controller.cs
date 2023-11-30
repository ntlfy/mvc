using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using Project_63132244.Models;

namespace Project_63132244.Controllers
{
    public class AccountKhachHangs_63132244Controller : Controller
    {
        private Project_63132244Entities db = new Project_63132244Entities();

        // GET: AccountKhachHangs_63132244

        public ActionResult Login_User()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login_User(string username, string password)
        {
            var user = db.KhachHangs.SingleOrDefault(m => m.MaKhachHang.ToLower() == username.ToLower());
            var adminuser = db.QuanTris.SingleOrDefault(m => m.TaiKhoan.ToLower() == username.ToLower());

            // Kiểm tra trong database
            if (user == null && adminuser == null)
            {
                ViewBag.username = username;
                ViewBag.error = "Tài Khoản không tồn tại. Vui lòng đăng ký tài khoản!";
                return View();
            }

            if (user != null && user.Password == password)
            {
                Session["taikhoan"] = user;
                return RedirectToAction("Index", "Home_63132244");
            }

            if (adminuser != null && adminuser.Password == password && adminuser.admin == true)
            {
                Session["taikhoan"] = adminuser;
                return RedirectToAction("Index", "AdminSanPhams_63132244", new { area = "Admin_63132244" });
            }
            if(user != null && user.Password != password)
            {
                ViewBag.error = "Mật khẩu không đúng";
                ViewBag.username = username;
            }
            if (adminuser != null && adminuser.Password == password && adminuser.admin == false)
            {
                ViewBag.error = "Bạn không có quyền đăng nhập vào admin. Vui lòng liên hệ với chúng tôi để được hỗ trợ!";
                ViewBag.username = username;
            }
            return View();
        }
        public ActionResult GetUserInfo(string username)
        {
            if (Session["taikhoan"] == null)
            {
                return RedirectToAction("Login_User");
            }
            var user = Session["taikhoan"] as KhachHang;
            return View(user);
        }
        public ActionResult Logout()
        {
            Session.Remove("taikhoan");
            return RedirectToAction("Login_User");
        }
        public ActionResult Register_User()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register_User([Bind(Include = "MaKhachHang,TaiKhoan,Password,HoTen,Email,DienThoai,Avatar,DiaChi")] HttpPostedFileBase Avatar, KhachHang model)
        {
            var maKH = db.KhachHangs.FirstOrDefault(x=>x.MaKhachHang.ToLower() == model.MaKhachHang.ToLower());
            if(maKH != null)
            {
                ViewBag.error = "Tên Tài Khoản Đã Tồn Tại !";
                return View();
            }
            //xử lý file ảnh
            if (Avatar != null)
            {
                model.Avatar = Avatar.FileName;
                string path = Server.MapPath("/Images/" + Avatar.FileName);
                Avatar.SaveAs(path);
            }
            else
            {
                model.Avatar = "default.png";
            }
            if(model.Email == null)
            {
                model.Email = "user@fruitshop.com";
            }
            if(ModelState.IsValid)
            {
                var checkPhone = new Regex(@"^0\d{9}$");
                if (checkPhone.IsMatch(model.DienThoai))
                {
                    db.KhachHangs.Add(model);
                    db.SaveChanges();
                    return RedirectToAction("Login_User");
                }
                else
                {
                    ModelState.AddModelError("DienThoai", "Số điện thoại không hợp lệ !");
                }
            }
            return View(model);
        }
    }
}
