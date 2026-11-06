namespace Ecom.API.Helper
{
    public class ResponseAPI
    {
        public ResponseAPI( int statusCode, string message = null)
        {
            
            StatusCode = statusCode;
            Message = message ?? GetMessageFromStatusCode(StatusCode);
        }
        private string GetMessageFromStatusCode(int StatusCode)
        {
            return StatusCode switch
            {
                200 => "Done",
                400 => "Bad Request",
                401 => "Un Authorized",
                500 =>"Server Error",
                404 =>"Not Found Resource",
                _ => null,
            };
        }
        public string? Message { get; set; }
        public int StatusCode { get; set; }
    }
}
