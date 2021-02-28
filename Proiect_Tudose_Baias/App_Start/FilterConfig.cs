using System.Web;
using System.Web.Mvc;

namespace Proiect_Tudose_Baias
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
