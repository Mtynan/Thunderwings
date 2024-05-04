using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common
{
    public class Response<T>
    {
        public bool IsSuccess { get; set; }
        public T? Data { get; set; } 

        public static Response<T> Success(T data) => new Response<T> { IsSuccess = true, Data = data };
        public static Response<T> Failure() => new Response<T> { IsSuccess = false };
    }
}
