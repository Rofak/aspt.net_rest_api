namespace Restfull.Model
{
    public abstract class BaseEntity
    {
        public DateTime? createdAt { get; set; }
        public DateTime? updatedAt { get; set; }
        public DateTime? deletedAt { get; set; }
    }
}
