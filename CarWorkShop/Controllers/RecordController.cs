using CarWorkShop.Models.ViewModel.Record;
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
                return View(records.Data);
            }

            return RedirectToAction("Error");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _recordService.Delete(id);
            if (response.StatusCode == Models.Enum.StatusCode.OK)
            {
                return RedirectToAction("GetRecords");
            }
            return View("Error", $"{response.Description}");
        }

        [HttpPost]
        public async Task<IActionResult> CreateRecord([FromBody] RecordViewModel model)
        {
            byte[] imageData;
            using (var binaryReader = new BinaryReader(model.Avatar.OpenReadStream()))
            {
                imageData = binaryReader.ReadBytes((int)model.Avatar.Length);
            }
            var response = await _recordService.Create(model, imageData);
            return RedirectToAction("GetRecords");
        }
    }
}
