using CarWorkShop.Models.ViewModel.Record;
using CarWorkShop.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CarWorkShop.Controllers
{
    public class RecordController : Controller
    {
        private readonly IRecordService _recordService;
        private readonly IOwnerService _ownerService;
        private readonly IProfileService _profileService;
        private readonly ILogger<RecordController> _logger;

        public RecordController(IRecordService recordService, ILogger<RecordController> logger, IOwnerService ownerService, IProfileService profileService)
        {
            _recordService = recordService;
            _logger = logger;
            _ownerService = ownerService;
            _profileService = profileService;
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetRecords()
        {
            var userName = User.Identity.Name;
            var records =  _recordService.GetRecords(userName);
            if (records.StatusCode == Models.Enum.StatusCode.OK)
            {
                return View(records.Data);
            }

            return RedirectToAction("Index", "Home");
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
            
            var profile = _profileService.GetProfile(User.Identity.Name);
            var model = new RecordViewModel()
            {
                Login = User.Identity.Name,
                FirstName = profile?.Result.Data.FirstName,
                LastName = profile.Result.Data.LastName,
                MiddleName = profile.Result.Data.MiddleName,
                Age = profile.Result.Data.Age,
                DateTime = DateTime.Now.ToLongDateString()

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
            viewModel.Login = User.Identity.Name;
            if (ModelState.IsValid)
            {
                var response = await _recordService.Create(viewModel, null);

                _logger.LogInformation($"Была добавлена запись под номером \"{response.Data.Id}\" пользователем {User.Identity.Name}");
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
