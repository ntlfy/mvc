using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project_63132244.Models;
using PagedList;
using PagedList.Mvc;
using System.Text.RegularExpressions;
using Project_63132244.App_Start;

namespace Project_63132244.Controllers
{
    public class Home_63132244Controller : Controller
    {
        Project_63132244Entities db = new Project_63132244Entities();
        public ActionResult Index(int? page)
        {
            int pageSize = 3; 
            int pageNumber = (page ?? 1); 

            List<Sanpham> danhsachSanPham = db.Sanphams.ToList();
            PagedList<Sanpham> pagedSanPham = new PagedList<Sanpham>(danhsachSanPham, pageNumber, pageSize);

            return View(pagedSanPham);
        }
        public ActionResult About() {
            return View();
        }
        public ActionResult Contact()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Contact([Bind(Include = "HoTen,Email,Phone,Subject,Messenge")] QUESTION model) 
        {
            if (model.Messenge.Length > 200)
            {
                ModelState.AddModelError("Messenge", "Tin nhắn vượt quá 200 ký tự. Vui lòng liên hệ với chúng tôi qua email hoặc số điện thoại !");
                return View(model);
            }
            if (model.Email == null)
            {
                model.Email = "user@shopfruit.com";
            }
            if (ModelState.IsValid)
            {
                var checkPhone = new Regex(@"^0\d{9}$");
                if (checkPhone.IsMatch(model.Phone) )
                {
                    db.QUESTIONs.Add(model);
                    db.SaveChanges();
                    ViewBag.SendSuccess = true;
                    return View(model);
                }
                else
                {
                    ModelState.AddModelError("Phone", "Số điện thoại không hợp lệ !");
                }
            }
            return View(model);
        }

    }
}