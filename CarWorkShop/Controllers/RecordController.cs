using CarWorkShop.Models.ViewModel.Record;
using CarWorkShop.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CarWorkShop.Controllers
{
    public class RecordController : Controller
    {
        private readonly IRecordService _recordService;
        private readonly ILogger<RecordController> _logger;

        public RecordController(IRecordService recordService, ILogger<RecordController> logger)
        {
            _recordService = recordService;
            _logger = logger;
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

            _logger.LogInformation($"Была удалена запись под номером \"{response.Data}\" пользователем ////");
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

            _logger.LogInformation($"Была добавлена запись под номером \"{response.Data.Id}\" пользователем ////");
            return RedirectToAction("GetRecords");
        }
    }
}
