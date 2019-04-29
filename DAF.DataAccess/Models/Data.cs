using System;
using System.Collections.Generic;

namespace DAF.DataAccess.Models
{
    public class Data : BaseModel
    {
        public User User { get; set; }
        public Guid UserId { get; set; }

        public string Description { get; set; }
        public string Value { get; set; }

        public DataAccessToken DataAccessToken { get; set; }
    }
}