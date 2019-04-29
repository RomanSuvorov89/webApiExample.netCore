using System;

namespace DAF.DataAccess.Models
{
    public class TokenData : BaseModel
    {
        public string TokenString { get; set; }
        public DateTime ExpiresAt { get; set; }
    }
}
