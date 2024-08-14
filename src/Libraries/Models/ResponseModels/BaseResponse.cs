using System.Collections.Generic;

namespace Models.ResponseModels
{
    public class BaseResponse<T>
    {
        public BaseResponse()
        {
        }

        public BaseResponse(T data, string message = null, bool succeeded = true)
        {
            Message = message;
            Data = data;
            Succeeded = succeeded;
        }
        
        public BaseResponse(string message)
        {
            Message = message;
        }

        public bool Succeeded;
        public string Message { get; set; }
        public List<string> Errors;
        public T Data { get; set; }
    }
}