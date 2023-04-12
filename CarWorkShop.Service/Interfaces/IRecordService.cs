using CarWorkShop.Models.Entity;
using CarWorkShop.Models.Response;
using CarWorkShop.Models.ViewModel.Record;


namespace CarWorkShop.Service.Interfaces
{
    public interface IRecordService
    {
        Task<IBaseResponse<Record>> Create(RecordViewModel recordViewModel,byte[]? imageData);

        IBaseResponse<List<Record>> GetRecords();

        Task<IBaseResponse<bool>> Delete(int id);

        Task<IBaseResponse<Record>> Update(int id, RecordViewModel recordViewModel);
    }
}
