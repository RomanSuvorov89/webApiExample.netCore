using System;
using System.ComponentModel.DataAnnotations;

namespace DAF.DataAccess.Models
{
    public class BaseModel
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}