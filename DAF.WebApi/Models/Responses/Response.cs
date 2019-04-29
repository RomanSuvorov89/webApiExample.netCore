namespace DAF.WebApi.Models.Responses
{
    public class Response
    {
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
    }

    public class Response<T> : Response
    {
        public T ResultOperation { get; set; }
    }
}