using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TGBot.Helper;

namespace TGBot.Controllers
{
    [Authorize(Roles = "TraderAdmin")]
    public class ConstructDateMessageController : Controller
    {



        private ComplexEntity _Database;
        public ConstructDateMessageController(ComplexEntity Database)
        {
            _Database = Database;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetList(string from, string to)
        {
            var fromDate = DateTime.Parse(from);
            var toDate = DateTime.Parse(to);

            var list = _Database.TradeInfo_SearchByDate(fromDate, toDate) ?? new List<TradeInfo_SearchByDate_Result>();

            return PartialView("_MessagesInDateRange", list);
        }




    }
}
