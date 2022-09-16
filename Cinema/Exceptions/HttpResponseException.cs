namespace Cinema.Exceptions
{

    
        public class HttpResponseException : Exception
        {
            public int StatusCode { get; set; }

            public ErrorBody Value { get; set; }
        }
    


}
