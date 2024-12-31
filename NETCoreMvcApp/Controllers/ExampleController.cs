using Microsoft.AspNetCore.Mvc;

namespace NETCoreMvcApp.Controllers;


public interface IService
{
    Distance GetDistance();
}

public readonly struct Distance(double dx, double dy)
{
    public readonly double Magnitude { get; } = Math.Sqrt(dx * dx + dy * dy);
    public readonly double Direction { get; } = Math.Atan2(dy, dx);
}

public class ExampleController(IService service) : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public ActionResult<Distance> Get()
    {
        return service.GetDistance();
    }
}
