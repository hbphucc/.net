using Microsoft.AspNetCore.Mvc;

namespace Learn.Controllers
{
    public class CollectionController : Controller
    {
        [Route("c/{id?}")]
        public IActionResult Index(string id)
        {
            return View((Object?)id);
        }
    }
}
