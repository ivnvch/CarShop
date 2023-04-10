using CarWorkShop.Models;
using CarWorkShop.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CarWorkShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRecordService _recordService;

        public HomeController(IRecordService recordService, ILogger<HomeController> logger)
        {
            _recordService = recordService;
            _logger = logger;
        }


        public IActionResult Index()
        {
            var records = _recordService.GetRecords();
            return View(records.Data.ToList());

        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}