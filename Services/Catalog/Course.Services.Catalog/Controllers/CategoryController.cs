using Microsoft.AspNetCore.Mvc;

namespace Course.Services.Catalog.Controllers;

public class CategoryController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}