using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Restfull.Model
{
    [Table("tbl_user")]
    public class User:BaseEntity
    {
   

        public int id { get; set; }
        public required string name { get; set; }
        public required string email { get; set; }

        public required string password { get; set; }  
        public DateOnly dob { get; set; }
   
    }
}
