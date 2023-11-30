using System.Web.Mvc;

namespace Project_63132244.Areas.Admin_63132244
{
    public class Admin_63132244AreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Admin_63132244";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Admin_63132244_default",
                "Admin_63132244/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}