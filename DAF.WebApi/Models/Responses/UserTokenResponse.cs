using System;
using System.Collections.Generic;

namespace DAF.WebApi.Models.Responses
{
    public class UserTokenResponse
    {
        public List<UserTokenData> UserTokens { get; set; }
    }

    public class UserTokenData
    {
        public Guid Id { get; set; }
        public string DeviceName { get; set; }
        public DateTime LastVisit { get; set; }
    }
}