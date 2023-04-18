using CarWorkShop.Models.Entity;
using CarWorkShop.Models.Response;
using CarWorkShop.Models.ViewModel.Profile;

namespace CarWorkShop.Service.Interfaces
{
    public interface IProfileService
    {
        Task<BaseResponse<ProfileViewModel>> GetProfile(string firstName);

        Task<BaseResponse<Profile>> Save(ProfileViewModel viewModel);
    }
}
