using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project_63132244.App_Start;
using Project_63132244.Models;
namespace Project_63132244.Areas.Admin_63132244.Controllers
{
    public class AdminQuestion_63132244Controller : Controller
    {
        Project_63132244Entities db = new Project_63132244Entities();
        // GET: Admin_63132244/AdminQuestion_63132244
        [AuthFilterAttribute_63132244]
        public ActionResult Index(string searchString)
        {
            IQueryable<QUESTION> result = db.QUESTIONs;
            if (!String.IsNullOrEmpty(searchString))
            {
                result = result.Where(x => x.HoTen.Contains(searchString) || x.Phone.Contains(searchString) || x.Email.Contains(searchString));
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
        public ActionResult UpdateIndex (int id)
        {
            var idquestion = db.QUESTIONs.Find(id);
            if(idquestion != null)
            {
                idquestion.Status = true;
                db.SaveChangesAsync();
            }
            return Json(new { success = true });
        }
    }
}