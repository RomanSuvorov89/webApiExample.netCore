using System.Collections.Generic;

namespace DAF.DataAccess.Models
{
    public class User : BaseModel
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public virtual ICollection<UserToken> AcсessTokens { get; set; } = new HashSet<UserToken>();
    }
}