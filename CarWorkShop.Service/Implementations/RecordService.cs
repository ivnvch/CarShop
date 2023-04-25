using CarWorkShop.DAL.Interfaces;
using CarWorkShop.DAL.Repositories;
using CarWorkShop.Models.Entity;
using CarWorkShop.Models.Enum;
using CarWorkShop.Models.Response;
using CarWorkShop.Models.ViewModel.Record;
using CarWorkShop.Service.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace CarWorkShop.Service.Implementations
{
    public class RecordService : IRecordService
    {
        private readonly IBaseRepository<Record> _recordRepository;
        private readonly IBaseRepository<Owner> _ownerRepository;

        public RecordService(IBaseRepository<Record> recordRepository, IBaseRepository<Owner> ownerRepository)
        {
            _recordRepository = recordRepository;
            _ownerRepository = ownerRepository;
        }

        public async Task<IBaseResponse<Record>> Create(RecordViewModel recordViewModel, byte[] imageData)
        {
            try
            {

                var record = new Record()
                {
                    Car = new Car()
                    {
                        Id = recordViewModel.Id,
                        Mark = recordViewModel.Mark,
                        Model = recordViewModel.Model,
                        CarNumber = recordViewModel.CarNumber,
                        Avatar = imageData
                    },
                    DateTime = DateTime.Now,
                    Complaint = recordViewModel.Complaint,
                    ProfileId = 2,
                    //CarId = recordViewModel.Id
                    
                    
                };
                await _recordRepository.Create(record);

                return new BaseResponse<Record>()
                {
                    StatusCode = StatusCode.OK,
                    Data = record
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Record>()
                {
                    StatusCode = StatusCode.InternalServerError,
                    Description = $"[Create] : {ex.Message}"
                };
            }
        }

        public IBaseResponse<List<Record>> GetRecords()
        {
            try
            {
                var records = _recordRepository.GetAll().ToList();
                if (records.Any())
                {
                    return new BaseResponse<List<Record>>()
                    {
                        Data = records,
                        StatusCode = StatusCode.OK,
                    };
                }

                return new BaseResponse<List<Record>>()
                {
                    Description = "Список пуст",
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<List<Record>>()
                {
                    Description = $"[GetRecords] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<bool>> Delete(int id)
        {
            try
            {
                var record = await _recordRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
                if (record != null)
                {
                    await _recordRepository.Delete(record);
                    return new BaseResponse<bool>()
                    {
                        Data = true,
                        StatusCode = StatusCode.OK
                    };
                }

                return new BaseResponse<bool>()
                {
                    Data = false,
                    StatusCode = StatusCode.RecordNotFound,
                    Description = "Record Not Found"
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Description = $"[Delete] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        //check logic in this method
        public async Task<IBaseResponse<Record>> Update(int id, RecordViewModel recordViewModel)
        {
            try
            {
                //!!! написать запрос в БД, учитывая все данные
                var record = await _recordRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id 
                                   /* x.CarId == x.Car.Id*/ && x.ProfileId == x.Profile.Id);
                if (record != null)
                {
                    record.DateTime = DateTime.ParseExact(recordViewModel.DateTime, "yyyyMMdd HH:mm", null);
                    record.Complaint = recordViewModel.Complaint;
                    record.Profile.FirstName = recordViewModel.FirstName;
                    record.Profile.LastName = recordViewModel.LastName;
                    record.Profile.MiddleName = recordViewModel.MiddleName;
                    record.Profile.Age = recordViewModel.Age;
                    record.Car.Mark = recordViewModel.Mark;
                    record.Car.Model = recordViewModel.Model;
                    record.Car.CarNumber = recordViewModel.CarNumber;

                    await _recordRepository.Update(record);

                    return new BaseResponse<Record>()
                    {
                        Data = record,
                        StatusCode = StatusCode.OK
                    };
                }

                return new BaseResponse<Record>()
                {
                    Data = record,
                    StatusCode = StatusCode.RecordNotFound,
                    Description = "Record Not Found"
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Record>()
                {
                    StatusCode = StatusCode.InternalServerError,
                    Description = $"[Update]: {ex.Message}"
                };
            }
        }

        public async Task<IBaseResponse<IEnumerable<Record>>> GetRecord(string userName)
        {
            try
            {
                var profile = await _ownerRepository.GetAll().Include(x => x.Profile).ThenInclude(x => x.Records).FirstOrDefaultAsync(x => x.Login == userName);
                var records = profile.Profile?.Records;
                if (records.Any())
                {
                    return new BaseResponse<IEnumerable<Record>>()
                    {
                        Data = records,
                        StatusCode = StatusCode.OK,
                    };
                }

                return new BaseResponse<IEnumerable<Record>>()
                {
                    Description = "Такой Записи нет",
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<Record>>()
                {
                    Description = $"[GetRecord] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
    }
}
