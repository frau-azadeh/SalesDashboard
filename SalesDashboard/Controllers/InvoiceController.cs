using Microsoft.AspNetCore.Mvc;

public class InvoiceController : Controller
{
    public IActionResult Create()
    {
        return View();
    }
}
