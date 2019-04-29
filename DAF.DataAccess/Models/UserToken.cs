using System;

namespace DAF.DataAccess.Models
{
    public class UserToken : TokenData
    {
        public User User { get; set; }
        public Guid UserId { get; set; }
        public DateTime LastVisit { get; set; }
        public string DeviceName { get; set; }
    }
}