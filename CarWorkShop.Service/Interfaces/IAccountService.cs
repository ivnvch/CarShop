using CarWorkShop.Models.Response;
using CarWorkShop.Models.ViewModel.Account;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CarWorkShop.Service.Interfaces
{
    public interface IAccountService
    {
        Task<BaseResponse<ClaimsIdentity>> Register(RegisterViewModel viewModel);
        Task<BaseResponse<ClaimsIdentity>> Login(LoginViewModel viewModel);
        Task<BaseResponse<bool>> ChangePassword(ChangePasswordViewModel viewModel);
    }
}
