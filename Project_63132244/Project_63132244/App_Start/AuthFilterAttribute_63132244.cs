using Project_63132244.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Project_63132244.App_Start
{
    public class AuthFilterAttribute_63132244 : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            var user = filterContext.HttpContext.Session["taikhoan"];
            var checkadmin = filterContext.HttpContext.Session["taikhoan"] as QuanTri;

            if (user == null)
            {
                filterContext.Result = new RedirectToRouteResult(
                      new RouteValueDictionary
                      {
                        {"controller", "AccountKhachHangs_63132244"},
                        {"action", "Login_User"},
                        {"area", null}
                      });
            }else if(checkadmin == null)
            {
                filterContext.Result = new RedirectToRouteResult(
                      new RouteValueDictionary
                      {
                        {"controller", "AccountKhachHangs_63132244"},
                        {"action", "Login_User"},
                        {"area", null}
                      });
            }

        }

    }
}