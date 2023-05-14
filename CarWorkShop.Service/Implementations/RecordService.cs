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
        private readonly IBaseRepository<Car> _carRepository;

        public RecordService(IBaseRepository<Record> recordRepository, IBaseRepository<Owner> ownerRepository, IBaseRepository<Car> carRepository)
        {
            _recordRepository = recordRepository;
            _ownerRepository = ownerRepository;
            _carRepository = carRepository;
        }

        public async Task<IBaseResponse<Record>> Create(RecordViewModel recordViewModel, byte[] imageData)
        {
            try
            {
                var owner = _ownerRepository.GetAll().FirstOrDefaultAsync(x => x.Login == recordViewModel.Login);

                if (owner == null)
                {
                    return new BaseResponse<Record>()
                    {
                        Description = "Пользователь не найден",
                        StatusCode = StatusCode.OwnerNotFound
                    };
                }

                var record = new Record()
                {
                    
                    //Id = recordViewModel.Id,
                    DateTime = DateTime.Now,
                    Complaint = recordViewModel.Complaint,
                    OwnerId = owner.Result.Id,
                    
                    
                };

                await _recordRepository.Create(record);

                Car car = new Car()
                {
                   // Id = recordViewModel.Id,
                    Mark = recordViewModel.Mark,
                    Model = recordViewModel.Model,
                    CarNumber = recordViewModel.CarNumber,
                    Avatar = imageData,
                    Record = record

                };

                await _carRepository.Create(car);

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

        public IBaseResponse<List<Record>> GetRecords(string userName)
        {
            try
            {
                var user = _ownerRepository.GetAll().Include(x => x.Records).ThenInclude(x => x.Car).FirstOrDefault(x => x.Login == userName);
                //var records = _recordRepository.GetAll().Include(x => x.Owner).ThenInclude(x => x.Profile).Where(x => x.Owner.Login == userName).ToList();
                var response = user.Records.ToList();
                //if (response.Any())
                //{
                //    return new BaseResponse<List<Record>>()
                //    {
                //        Data = response,
                //        StatusCode = StatusCode.OK,
                //    };
                //}

                return new BaseResponse<List<Record>>()
                {
                   Data = response,
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
                                   /* x.CarId == x.Car.Id*/ /*&& x.ProfileId == x.Profile.Id*/);
                if (record != null)
                {
                    //record.DateTime = DateTime.ParseExact(recordViewModel.DateTime, "yyyyMMdd HH:mm", null);
                    //record.Complaint = recordViewModel.Complaint;
                    //record.Profile.FirstName = recordViewModel.FirstName;
                    //record.Profile.LastName = recordViewModel.LastName;
                    //record.Profile.MiddleName = recordViewModel.MiddleName;
                    //record.Profile.Age = recordViewModel.Age;
                    //record.Car.Mark = recordViewModel.Mark;
                    //record.Car.Model = recordViewModel.Model;
                    //record.Car.CarNumber = recordViewModel.CarNumber;

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
                var profile = await _ownerRepository.GetAll().Include(x => x.Records).FirstOrDefaultAsync(x => x.Login == userName);
                var records = profile.Records;
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

        //public async Task<IBaseResponse<IEnumerable<RecordViewModel>>> GetItemss(string userName)
        //{
        //    try
        //    {
        //        var record = await _ownerRepository.GetAll().Include(x => x.Records).ThenInclude(x => x.Owner).ThenInclude(x => x.Profile).FirstOrDefaultAsync(x => x.Login == userName);

        //        if (record == null)
        //        {
        //            return new BaseResponse<IEnumerable<RecordViewModel>>()
        //            {
        //                Description = "Пользователь не найден",
        //                StatusCode = StatusCode.OwnerNotFound
        //            };
        //        }

        //        var records = record.Records;
        //        var response = from p in records
        //                       join rec in _recordRepository.GetAll() on p.Car.Id equals rec.Id
        //                       select new RecordViewModel()
        //                       {
        //                           Id = rec.Id,
        //                           FirstName = rec?.Owner?.Profile?.FirstName,
        //                           LastName = rec?.Owner?.Profile?.LastName,
        //                           MiddleName = rec?.Owner?.Profile?.MiddleName,
        //                           Mark = rec.Car.Mark,
        //                           Model = rec.Car.Model,
        //                           CarNumber = rec.Car.CarNumber,
        //                           Complaint = rec.Complaint
        //                       };
        //        return new BaseResponse<IEnumerable<RecordViewModel>>()
        //        {
        //            Data = response,
        //            StatusCode = StatusCode.OK
        //        };
        //    }
        //    catch (Exception ex)
        //    {
        //        return new BaseResponse<IEnumerable<RecordViewModel>>()
        //        {
        //            Description = ex.Message,
        //            StatusCode = StatusCode.InternalServerError
        //        };
        //    }
        //}
    }
}
