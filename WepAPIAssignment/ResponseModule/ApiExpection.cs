namespace WepAPIAssignment.ResponseModule
{
    public class ApiExpection : ApiResponse
    {
        public  string Details { get; set; }


        public ApiExpection(int _statusCode, string _message = null, string details = null)
            : base(_statusCode, _message)
        {
            Details = details;
        }

    }
}
