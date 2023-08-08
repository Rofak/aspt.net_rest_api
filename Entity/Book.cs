using System.ComponentModel.DataAnnotations.Schema;

namespace Restfull.Entity
{
    [Table("tbl_book")]
    public class Book
    {
        public int id { get; set; }
        public required string name { get; set; }

        public required string description { get; set; }

        public DateTime releaseDate { get; set; }
    }
}
