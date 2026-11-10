using APP.Domain;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace MVC.Controllers
{
    public class DbController : Controller
    {
        // Db/Seed
        public IActionResult Seed()
        {
            var dbFactory = new DbFactory();
            dbFactory.SeedDb();
            return Content("<b>Database seed successful.</b>", "text/html", Encoding.UTF8);
        }
    }
}
