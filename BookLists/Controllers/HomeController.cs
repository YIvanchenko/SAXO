using System.Web.Mvc;

namespace BookLists.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Upload()
        {
            ViewBag.Message = "Please upload you csv file here.";

            return View();
        }
    }
}