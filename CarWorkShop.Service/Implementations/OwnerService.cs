using CarWorkShop.DAL.Interfaces;
using CarWorkShop.Models.Entity;
using CarWorkShop.Models.Enum;
using CarWorkShop.Models.Extensions;
using CarWorkShop.Models.Helpers;
using CarWorkShop.Models.Response;
using CarWorkShop.Models.ViewModel.Owner;
using CarWorkShop.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CarWorkShop.Service.Implementations
{
    public class OwnerService : IOwnerService
    {

        private readonly IBaseRepository<Owner> _ownerRepository;
        private readonly IBaseRepository<Profile> _profileRepository;

        public OwnerService(IBaseRepository<Owner> ownerRepository, IBaseRepository<Profile> profileRepository)
        {
            _ownerRepository = ownerRepository;
            _profileRepository = profileRepository;
        }

        public async Task<IBaseResponse<Owner>> Create(OwnerViewModel viewModel)
        {
            try
            {
                var owner = await _ownerRepository.GetAll().FirstOrDefaultAsync(x => x.Login == viewModel.Login);
                if (owner != null)
                {
                    return new BaseResponse<Owner>()
                    {
                        Description = "Пользователь с таким логином уже существует",
                        StatusCode = Models.Enum.StatusCode.OwnerAlreadyExists
                    };
                }

                owner = new Owner()
                {
                    Login = viewModel.Login,
                    Role = Enum.Parse<Role>(viewModel.Role),
                    Password = HashPasswordHelpers.HashPassowrd(viewModel.Password),
                };

                await _ownerRepository.Create(owner);

                var profile = new Profile()
                {
                    FirstName = string.Empty,
                    LastName = string.Empty,
                    MiddleName = string.Empty,
                    Age = 0,
                    OwnerId = owner.Id,
                };

                await _profileRepository.Create(profile);

                return new BaseResponse<Owner>()
                {
                    Data = owner,
                    StatusCode = StatusCode.OK,
                    Description = "Пользователь добавлен"
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Owner>()
                {
                    Description = $" Внутренняя ошибка {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<bool>> Delete(int id)
        {
            try
            {
                var owner = await _ownerRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
                if (owner == null)
                {
                    return new BaseResponse<bool>
                    {
                        StatusCode = StatusCode.OwnerNotFound,
                        Data = false
                    };
                }
                await _ownerRepository.Delete(owner);

                return new BaseResponse<bool>
                {
                    StatusCode = StatusCode.OK,
                    Data = true
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    StatusCode = StatusCode.InternalServerError,
                    Description = $"Внутренняя ошибка: {ex.Message}"
                };
            }
        }
    

        public async Task<IBaseResponse<IEnumerable<OwnerViewModel>>> GetOwners()
        {
            try
            {
                var owners = await _ownerRepository.GetAll()
                    .Select(x => new OwnerViewModel()
                    {
                        Id = x.Id,
                        Login = x.Login,
                        Role = x.Role.GetDisplayName()
                    })
                    .ToListAsync();

               
                return new BaseResponse<IEnumerable<OwnerViewModel>>()
                {
                    Data = owners,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                
                return new BaseResponse<IEnumerable<OwnerViewModel>>()
                {
                    StatusCode = StatusCode.InternalServerError,
                    Description = $"Внутренняя ошибка: {ex.Message}"
                };
            }
        }

        public BaseResponse<Dictionary<int, string>> GetRoles()
        {
            try
            {
                var roles = ((Role[])Enum.GetValues(typeof(Role)))
                    .ToDictionary(k => (int)k, t => t.GetDisplayName());

                return new BaseResponse<Dictionary<int, string>>()
                {
                    Data = roles,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Dictionary<int, string>>()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
    }
}
