using Microsoft.AspNetCore.Http;

namespace WepAPIAssignment.ResponseModule
{
    public class ApiResponse
    {
        public int StatusCode { get; set; }

        public string Message { get; set; }
        public ApiResponse(int _statusCode, string _message = null) 
        {
            StatusCode = _statusCode;

           Message = GetDefualtMessageForStatusCode(StatusCode);
        }


        private string GetDefualtMessageForStatusCode(int statusCode)
        {
            return statusCode switch
            {
                400 => "You made a bad request",
                401 => "You are unathourized to view this action",
                404 => "Resourse not found",
                500 => "Server error",
                _ => "Bad Requset"
            };

        }
    }
}
