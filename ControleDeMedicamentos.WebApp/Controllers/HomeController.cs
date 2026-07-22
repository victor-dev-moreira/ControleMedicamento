using Microsoft.AspNetCore.Mvc;
namespace ControleDeMedicamentos.WebApp.Controllers;

public class HomeController : Controller
{
    public ActionResult Index()
    {
        return View();
    }
}
