using Learn.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.HttpSys;
using System.Diagnostics;

namespace Learn.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRepository repository;
        private readonly ILogger<HomeController> _logger;

        public HomeController(IRepository repository, ILogger<HomeController> logger)
        {
            this.repository = repository;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View(new HelloModel() { Name="Phuc"});
        }

        public IActionResult Privacy()
        {
            return View("Index",new HelloModel() { Name = "Phuc" });
        }
        public ActionResult Contact()
        {
            return View();
        }
        public IActionResult NewActionMethod(string name,int n)
        {
            return Content("Hi from new action method"+repository.GetByID(name));
        }

        [HttpGet]
        public IActionResult Users([FromHeader] string apiKey,[FromServices]IUserRepository userRepository)
        {
            _logger.LogInformation("[Users] METHOD: {m}, apiKey={apikey}", Request.Method, apiKey);

            ValidateApikey(apiKey);

            return Content($"Users: {string.Join(',',userRepository.Users)}");
        }

        [HttpPost]
        public IActionResult Users([FromHeader] string apiKey, [FromServices] IUserRepository userRepository,string user)
        {
            _logger.LogInformation("[Users] METHOD: {m}, apiKey={apikey}", Request.Method, apiKey);
            ValidateApikey(apiKey);
            userRepository.Add(user);
            return Ok();

        }
        private void ValidateApikey(string apiKey)
        {
            if (apiKey == null)
            {
                throw new ArgumentException(nameof(apiKey));
            }
        }


        //[HttpPost]
        //public IActionResult Users(string user)
        //{
        //    _logger.LogInformation("[User] METHOD: {m}", Request.Method);
        //    return Content("User added: "+user);
        //}

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
