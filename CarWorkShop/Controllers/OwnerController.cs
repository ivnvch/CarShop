using CarWorkShop.Models.ViewModel.Owner;
using CarWorkShop.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CarWorkShop.Controllers
{
    public class OwnerController : Controller
    {

        private readonly IOwnerService _ownerService;

        public OwnerController(IOwnerService ownerService)
        {
            _ownerService = ownerService;
        }

        public async Task<IActionResult> GetOwners()
        {
            var response = await _ownerService.GetOwners();
            if (response.StatusCode == Models.Enum.StatusCode.OK)
            {
                return View(response.Data);
            }

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var response = await _ownerService.Delete(id);
            if (response.StatusCode == Models.Enum.StatusCode.OK)
            {
                return RedirectToAction("GetOwners");
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult CreateOwner() => View();

        [HttpPost]
        public async Task<IActionResult> CreateOwner(OwnerViewModel viewModel)
        {
            var response = await _ownerService.Create(viewModel);
            
            if (response.StatusCode == Models.Enum.StatusCode.OK)
            {
                return RedirectToAction("GetOwners");
            }
            return View();
        }


        public IActionResult Index()
        {
            return View();
        }
    }
}
