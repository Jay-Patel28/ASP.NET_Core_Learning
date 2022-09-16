namespace Cinema.Exceptions
{
    public class ErrorBody
    {
        public int StatusCode { get; set; }

        public string ErrorCode { get; set; }

        public string ErrorMessage { get; set; }

        public ErrorBody(int statusCode, string errorCode, string errorMessage)
        {
            StatusCode = statusCode;
            ErrorCode = errorCode;
            ErrorMessage = errorMessage;
        }
    }
}


