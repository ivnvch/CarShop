using CarWorkShop.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CarWorkShop.Controllers
{
    public class RecordController : Controller
    {
        private readonly IRecordService _recordService;

        public RecordController(IRecordService recordService)
        {
            _recordService = recordService;
        }

        [HttpGet]
        public IActionResult GetRecords()
        {
            var records = _recordService.GetRecords();
            if (records.StatusCode == Models.Enum.StatusCode.OK)
            {
                return View(records.Data.ToList());
            }

            return RedirectToAction("Error");
        }
    }
}
