namespace WepAPIAssignment.ResponseModule
{
    public class ApiValdationErrors: ApiExpection
    {
        public ApiValdationErrors() : base(400)
        {

        }

        public IEnumerable<string> errors { get; set; } 
    }
}
