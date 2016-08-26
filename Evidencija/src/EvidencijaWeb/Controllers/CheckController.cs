///Created by: Bartul Kovačić
///Github: https:github.com/BKova
using Microsoft.AspNetCore.Mvc;

namespace Evidencija.Controllers
{
    [Route("check")]
    public class CheckController : Controller
    {
        [HttpGet]
        public string Get()
        {
            return "OK";
        }
    }
}
