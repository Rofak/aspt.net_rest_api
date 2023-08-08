namespace Restfull.DTO.Book
{
    public class UpdateBookDto
    {
        public  string? name { get; set; }

        public  string? description { get; set; }

        public DateTime releaseDate { get; set; }
    }
}
