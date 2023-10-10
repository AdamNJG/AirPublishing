namespace ImageStorage.ImageVerification.Service.status
{
    public class RequestStatus
    {
        public StatusCode StatusCode { get; set; }
        public string Message { get; set; }

        public RequestStatus(StatusCode statusCode, string message)
        {
            StatusCode = statusCode;
            Message = message;
        }
    }
}
