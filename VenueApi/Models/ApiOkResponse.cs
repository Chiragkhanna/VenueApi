namespace VenueApi.Models
{
    public class ApiOkResponse
    {
        public bool success { get; set; } = true;
        public object data { get; }

        public ApiOkResponse(object result)
        {
            data = result;
        }
    }
    public class ApiBadResponse
    {
        public bool success { get; set; } = false;
        public object error { get; }

        public ApiBadResponse(object result)
        {
            error = result;
        }
    }
}
