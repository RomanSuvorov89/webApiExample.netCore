using System;
using System.Collections.Generic;

namespace DAF.WebApi.Models.Responses
{
    public class UserDataResponse
    {
        public ICollection<DataToken> ActiveTokenData { get; set; }
        public ICollection<DataToken> ExpiredTokenData { get; set; }
    }

    public class DataToken
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public string Value { get; set; }
        public string Token { get; set; }
    }
}