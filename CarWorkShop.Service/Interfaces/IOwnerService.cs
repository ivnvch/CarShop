using CarWorkShop.Models.Entity;
using CarWorkShop.Models.Response;
using CarWorkShop.Models.ViewModel.Owner;

namespace CarWorkShop.Service.Interfaces
{
    public interface IOwnerService
    {
        Task<IBaseResponse<Owner>> Create(OwnerViewModel viewModel);
        Task<IBaseResponse<IEnumerable<OwnerViewModel>>> GetOwners();
        BaseResponse<Dictionary<int, string>> GetRoles();
        Task<IBaseResponse<bool>> Delete(int id);

    }
}
