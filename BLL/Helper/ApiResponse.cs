using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Helper
{
    public class ApiResponse<TResult>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public TResult Data { get; set; }

        public static ApiResponse<TResult> Succeded(TResult data, string message = null)
        {
            return new ApiResponse<TResult>
            {
                Success = true,
                Message = message ?? "Request successful.",
                Data = data
            };
        }

        public static ApiResponse<TResult> Fail(string message)
        {
            return new ApiResponse<TResult>
            {
                Success = false,
                Message = message,
                Data = default
            };
        }

    }

}
