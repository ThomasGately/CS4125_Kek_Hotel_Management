using System.Web;
using System.Web.Mvc;

namespace CS4125_Kek_Hotel_Management
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
