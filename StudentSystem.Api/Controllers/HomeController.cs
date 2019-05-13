using System.Web.Mvc;

namespace StudentSystem.Api.Controllers
{

    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return Redirect("/selectCourse-manager");
        }
    }
}
