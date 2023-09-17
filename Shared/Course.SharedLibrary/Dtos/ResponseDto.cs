using System;
using System.Collections.Generic;

namespace Course.SharedLibrary.Dtos
{
    public class ResponseDto<T>
    {
        public T Data { get;private set; }
        
        public int StatusCode { get;private set; }
        public bool IsSuccesful { get;private set; }
        
        public List<string> Errors { get; set; }
        
        public static ResponseDto<T> Success(T data, int statusCode)
        {
            return new ResponseDto<T>() { Data = data, StatusCode = statusCode ,IsSuccesful = true};
        }

        public static ResponseDto<T> Success(int statusCode)
        {
            return new ResponseDto<T>() { Data = default(T), StatusCode = statusCode, IsSuccesful = true };
        }

        public static ResponseDto<T> Fail(List<string> errors, int statusCode)
        {
            return new ResponseDto<T>() { Errors = errors, StatusCode = statusCode, IsSuccesful = false };
        }

        public static ResponseDto<T> Fail(string error, int statusCode)
        {
            return new ResponseDto<T>()
                { Errors = new List<string>() { error }, StatusCode = statusCode, IsSuccesful = false };
        }
        
    }
}