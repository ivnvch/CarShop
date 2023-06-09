﻿using CarWorkShop.Models.Entity;
using CarWorkShop.Models.Response;
using CarWorkShop.Models.ViewModel.Record;


namespace CarWorkShop.Service.Interfaces
{
    public interface IRecordService
    {
        Task<IBaseResponse<Record>> Create(RecordViewModel recordViewModel,byte[]? imageData);

        IBaseResponse<List<Record>> GetRecords(string userName);
        //Task<IBaseResponse<IEnumerable<RecordViewModel>>> GetItemss(string userName);

        Task<IBaseResponse<IEnumerable<Record>>> GetRecord(string userName);


        Task<IBaseResponse<bool>> Delete(int id);

        Task<IBaseResponse<Record>> Update(int id, RecordViewModel recordViewModel);
    }
}
