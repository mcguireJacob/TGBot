using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TGBot.Helper;

namespace TGBot.Controllers
{
    public class BotUserController : Controller
    {
        public IActionResult Index()
        {
            
            return View();
        }
    }
}
