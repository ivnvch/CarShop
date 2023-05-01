using CarWorkShop.Models.ViewModel.Record;
using CarWorkShop.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize]
        public IActionResult GetRecords()
        {
            var records = _recordService.GetRecords();
            if (records.StatusCode == Models.Enum.StatusCode.OK)
            {
                return View(records.Data);
            }

            return RedirectToAction("Error");
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetRecord()
        {
            
            var records = await _recordService.GetRecord(User.Identity.Name);
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
                _logger.LogInformation($"Была удалена запись под номером \"{response.Data}\" пользователем ////");
                return RedirectToAction("GetRecords");
            }

            return View("Error", $"{response.Description}");
        }

        [HttpGet]
        public IActionResult CreateRecord()
        {
            var model = new RecordViewModel()
            {
                Login = User.Identity.Name,
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRecord(RecordViewModel viewModel)
        {
            //byte[] imageData;
            //using (var binaryReader = new BinaryReader(model.Avatar.OpenReadStream()))
            //{
            //    imageData = binaryReader.ReadBytes((int)model.Avatar.Length);
            //}
            if (ModelState.IsValid)
            {
                var response = await _recordService.Create(viewModel, null);

                _logger.LogInformation($"Была добавлена запись под номером \"{response.Data.Id}\" пользователем ////");
                return RedirectToAction("GetRecords");
            }
           
            return View();

           
        }

        [HttpPost]
        public async Task<IActionResult> UpdateRecord(int id, RecordViewModel viewModel)
        {
            //byte[] imageData;
            //using (var binaryReader = new BinaryReader(model.Avatar.OpenReadStream()))
            //{
            //    imageData = binaryReader.ReadBytes((int)model.Avatar.Length);
            //}
            var response = await _recordService.Update(id, viewModel);
            if (response!= null)
            {
                _logger.LogInformation($"Была обновлена запись под номером \"{response.Data}\" пользователем ////");
                return RedirectToAction("GetRecords");
            }
            
            return View();


        }
    }
}
