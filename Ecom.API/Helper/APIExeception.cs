namespace Ecom.API.Helper
{
    public class APIExeception : ResponseAPI
    {
        public APIExeception(int statusCode, string message = null, string details = null) : base(statusCode, message)
        {
            Details = details;
        }
        public string Details { get; set; }
    }
}
