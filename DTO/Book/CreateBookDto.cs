namespace Restfull.DTO.Book
{
    public class CreateBookDto
    {

        public required string name { get; set; }

        public required string description { get; set; }

        public DateTime releaseDate { get; set; }
    }
}
