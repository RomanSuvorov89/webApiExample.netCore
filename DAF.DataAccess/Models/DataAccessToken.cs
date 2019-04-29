using System;

namespace DAF.DataAccess.Models
{
    public class DataAccessToken : TokenData
    {
        public Guid DataId { get; set; }
        public Data Data { get; set; }
    }
}