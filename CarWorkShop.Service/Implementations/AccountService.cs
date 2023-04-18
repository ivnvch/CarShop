using CarWorkShop.DAL.Interfaces;
using CarWorkShop.Models.Entity;
using CarWorkShop.Models.Response;
using CarWorkShop.Models.ViewModel.Account;
using CarWorkShop.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

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

        public Task<BaseResponse<ClaimsIdentity>> Login(LoginViewModel viewModel)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse<ClaimsIdentity>> Register(RegisterViewModel viewModel)
        {
            throw new NotImplementedException();
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
