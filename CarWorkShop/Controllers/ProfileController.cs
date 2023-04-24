using CarWorkShop.Models.ViewModel.Profile;
using CarWorkShop.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarWorkShop.Controllers
{
    public class ProfileController : Controller
    {

        private readonly IProfileService _profileService;

        public ProfileController(IProfileService profileService)
        {
            _profileService = profileService;
        }

        [Authorize]
        public async Task<IActionResult> Detail()
        {
            var login = User.Identity.Name;
            var  response = await _profileService.GetProfile(login);
            if (response.StatusCode == Models.Enum.StatusCode.OK)
            {
                return View(response.Data);
            }
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Save(ProfileViewModel viewModel)
        {
            ModelState.Remove("Login");
            if (ModelState.IsValid)
            {
                 await _profileService.Save(viewModel);
            }
            return RedirectToAction("Detail");
        }
    }
}
