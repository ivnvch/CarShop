using CarWorkShop.DAL.Interfaces;
using CarWorkShop.Models.Entity;
using CarWorkShop.Models.Response;
using CarWorkShop.Models.ViewModel.Profile;
using CarWorkShop.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CarWorkShop.Service.Implementations
{
    public class ProfileService : IProfileService
    {

        private readonly IBaseRepository<Profile> _profileRepository;
        public ProfileService(IBaseRepository<Profile> profileRepository)
        {
            _profileRepository = profileRepository;
        }

        public async Task<BaseResponse<ProfileViewModel>> GetProfile(string login)
        {
            try
            {
                var profile = await _profileRepository.GetAll()
                    .Select(x => new ProfileViewModel()
                    {
                        Id = x.Id,
                        FirstName = x.FirstName,
                        LastName = x.LastName,
                        MiddleName = x.MiddleName,
                        Age = x.Age,
                        Login = x.Owner.Login
                    }).FirstOrDefaultAsync(x => x.Login == login);

                return new BaseResponse<ProfileViewModel>()
                {
                    Data = profile,
                    StatusCode = Models.Enum.StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<ProfileViewModel>()
                {
                    StatusCode = Models.Enum.StatusCode.InternalServerError,
                    Description = $"Внутрення ошибка: {ex.Message}"
                };
            }
        }

        public async Task<BaseResponse<Profile>> Save(ProfileViewModel viewModel)
        {
            try
            {
                var profile = await _profileRepository.GetAll()
                    .FirstOrDefaultAsync(x => x.Id == viewModel.Id);

                profile.FirstName = viewModel.FirstName;
                profile.LastName = viewModel.LastName;
                profile.MiddleName = viewModel.MiddleName;
                profile.Age = viewModel.Age;

                await _profileRepository.Update(profile);

                return new BaseResponse<Profile>()
                {
                    Data = profile,
                    Description = "Данные обновлены",
                    StatusCode = Models.Enum.StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Profile>()
                {
                    StatusCode = Models.Enum.StatusCode.InternalServerError,
                    Description = $"Внутренняя ошибка: {ex.Message}"
                };
            }
        }
    }
}
