using System.Net;

namespace Cinema.Exceptions
{

    public class BadRequestException : HttpResponseException
        {
            public BadRequestException(string errorCode, string errorMessage)
            {
                this.StatusCode = (int)HttpStatusCode.BadRequest;
                this.Value = new ErrorBody(
                        (int)HttpStatusCode.BadRequest,
                        errorCode,
                        errorMessage
                    );
            }
       }
}

