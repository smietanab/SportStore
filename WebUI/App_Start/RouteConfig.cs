using System.Web.Mvc;
using System.Web.Routing;

namespace WebUI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(null, "Nav/Menu", new { controller = "Nav", action = "Menu" });
            routes.MapRoute(null, "", new { controller = "Product", action = "List", category = (string)null, page = 1 });
            routes.MapRoute(null, "Strona{page}",
                new { controller = "Product", action = "List", category = (string)null },
                new { page = @"\d+" }
                );
            routes.MapRoute(null, "{category}", new { controller = "Product", action = "List", page = 1 });
            routes.MapRoute(null, "{Category}/Strona{page}",
                new { controller = "Product", action = "List" },
                new { page = @"\d+" }

                );

            routes.MapRoute(null, "Cart/Index", new { controller = "Cart", action = "Index" });

            routes.MapRoute(null, "Cart/AddToCart", new { controller = "Cart", action = "AddToCart" });
            routes.MapRoute(null, "Cart/RemoveFromCart", new { controller = "Cart", action = "RemoveFromCart" });
            routes.MapRoute(null, "Cart/Summary", new { controller = "Cart", action = "Summary" });
            routes.MapRoute(null, "Cart/Checkout", new { controller = "Cart", action = "Checkout" });
            routes.MapRoute(null, "Admin/Index", new { controller = "Admin", action = "Index" });
            routes.MapRoute(null, "Admin/Edit", new { controller = "Admin", action = "Edit" });
            routes.MapRoute(null, "Admin/Create", new { controller = "Admin", action = "Create" });
            routes.MapRoute(null, "Admin/Delete", new { controller = "Admin", action = "Delete" });
            routes.MapRoute(null, "Account/Login", new { controller = "Account", action = "Login" });
            routes.MapRoute(null, "Product/GetImage", new { controller = "Product", action = "GetImage" });


            // routes.MapRoute(
            //   "Index",                                              // Route name
            //    "{Cart}/{Index}/{returnUrl}",                           // URL with parameters
            //     new { controller = "Cart", action = "Index", returnUrl = "" }  // Parameter defaults
            // );

        }
    }
}
