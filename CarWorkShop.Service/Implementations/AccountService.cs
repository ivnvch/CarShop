using CarWorkShop.DAL.Interfaces;
using CarWorkShop.Models.Entity;
using CarWorkShop.Models.Helpers;
using CarWorkShop.Models.Response;
using CarWorkShop.Models.ViewModel.Account;
using CarWorkShop.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;


namespace CarWorkShop.Service.Implementations
{
    public class AccountService : IAccountService
    {
        private readonly IBaseRepository<Owner> _ownerRepository;
        private readonly IBaseRepository<Profile> _profileRepository;

        public AccountService(IBaseRepository<Owner> ownerRepository, IBaseRepository<Profile> profileRepository)
        {
            _ownerRepository = ownerRepository;
            _profileRepository = profileRepository;
        }

        public Task<BaseResponse<bool>> ChangePassword(ChangePasswordViewModel viewModel)
        {
            throw new NotImplementedException();
        }

        public async Task<BaseResponse<ClaimsIdentity>> Login(LoginViewModel viewModel)
        {
            try
            {
                var owner = await _ownerRepository.GetAll().FirstOrDefaultAsync(x => x.Login == viewModel.Login);

                if (owner == null)
                {
                    return new BaseResponse<ClaimsIdentity>()
                    {
                        Description = "Пользователя с таким логином не существует"
                    };

                }
                if (owner.Password != HashPasswordHelpers.HashPassowrd(viewModel.Password))
                {
                    return new BaseResponse<ClaimsIdentity>()
                    {
                        Description = "Неверный пароль"
                    };
                }

                var result = Authenticate(owner);

                return new BaseResponse<ClaimsIdentity>()
                {
                    Data = result,
                    StatusCode = Models.Enum.StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<ClaimsIdentity>()
                {
                    Description = ex.Message,
                    StatusCode = Models.Enum.StatusCode.InternalServerError
                };
            }
        }

        public async Task<BaseResponse<ClaimsIdentity>> Register(RegisterViewModel viewModel)
        {
            try
            {
                var owner = await _ownerRepository.GetAll().FirstOrDefaultAsync(x => x.Login == viewModel.Login);
                if (owner != null)
                {
                    return new BaseResponse<ClaimsIdentity>()
                    {
                        Description = "Пользователь с таким логином уже существует"
                    };
                }

                owner = new Owner()
                {
                    Login = viewModel.Login,
                    Role = Models.Enum.Role.Owner,
                    Password = HashPasswordHelpers.HashPassowrd(viewModel.Password),
                };

                await _ownerRepository.Create(owner);

                var profile = new Profile()
                {
                    OwnerId = owner.Id,
                };

                await _profileRepository.Create(profile);

                var result = Authenticate(owner);

                return new BaseResponse<ClaimsIdentity>()
                {
                    Data = result,
                    Description = "Учётная запись создана",
                    StatusCode = Models.Enum.StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<ClaimsIdentity>()
                {
                    Description = ex.Message,
                    StatusCode = Models.Enum.StatusCode.InternalServerError
                };
            }
        }

        private ClaimsIdentity Authenticate(Owner owner)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, owner.Login),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, owner.Role.ToString())
            };
            return new ClaimsIdentity(claims, "ApplicationCookie",
                ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
        }
    }
}
