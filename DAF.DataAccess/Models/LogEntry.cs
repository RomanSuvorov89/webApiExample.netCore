namespace DAF.DataAccess.Models
{
    public class LogEntry : BaseModel
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public string OperationName { get; set; }
    }
}