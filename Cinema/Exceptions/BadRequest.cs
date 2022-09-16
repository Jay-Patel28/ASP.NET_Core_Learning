namespace Cinema.Exceptions
{
    public class BadRequest
    {
        public readonly int status_code;
        public readonly string? error_code;
        public readonly string? message;

        public BadRequest(int status_code, string? error_code, string? message)
        {
            this.status_code = status_code;
            this.error_code = error_code;
            this.message = message;
        }
    }
}
