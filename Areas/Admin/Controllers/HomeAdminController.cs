using Microsoft.AspNetCore.Mvc;

namespace StoreApp.Areas.Admin.Controllers;

[Area("Admin")]
[Route("admin")]
[Route("admin/homeadmin")]
public class HomeAdminController : Controller
{
    [Route("")]
    [Route("index")]
    public IActionResult Index() {
        return View();
    }
}