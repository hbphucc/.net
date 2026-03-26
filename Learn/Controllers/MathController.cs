using Microsoft.AspNetCore.Mvc;

namespace Learn.Controllers
{
    public class MathController : Controller
    {
        public string Sum(int x,int y)
        {
            return (x+y).ToString();
        }
    }
}
