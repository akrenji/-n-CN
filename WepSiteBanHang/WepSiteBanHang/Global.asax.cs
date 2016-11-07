using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace WepSiteBanHang
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public void Application_Start(object sender, EventArgs e)
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            Application["So_luot_truy_cap"] = 0;
            Application["So_nguoi_online"] = 0;


        }
        void Session_Start(object sender, EventArgs e)
        {
            // Tăng giá trị biến Application
            Application["So_luot_truy_cap"] = (int)Application["So_luot_truy_cap"] + 1;
            Application["So_nguoi_online"] = (int)Application["So_nguoi_online"] + 1;
            Session["So_luot_truy_cap"] = (int)Application["So_luot_truy_cap"] ;
            Session["So_nguoi_online"] = (int)Application["So_nguoi_online"] ;
        }
        void Session_End(object sender, EventArgs e)
        {
            // Giảm giá trị biến Application
            Session["So_nguoi_online"] = (int)Application["So_nguoi_online"] - 1;

        }


    }
}
