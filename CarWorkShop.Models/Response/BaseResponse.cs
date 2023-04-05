using CarWorkShop.Models.Enum;

namespace CarWorkShop.Models.Response
{
    public class BaseResponse<T> : IBaseResponse<T>
    {
        public T Data { get; set; }
        public string Description { get; set; }
        public StatusCode StatusCode { get; set; }
    }

    public interface IBaseResponse<T>
    {
        T Data { get; }
        string Description { get; }
        StatusCode StatusCode { get; }
    }
}
