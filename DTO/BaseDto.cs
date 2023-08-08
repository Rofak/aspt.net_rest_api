namespace Restfull.DTO
{
    public class BaseDto<Create,Update>
    {
        public required Create createDto { get; set; }
        public required Update updateDto { get; set; }
    }
}
