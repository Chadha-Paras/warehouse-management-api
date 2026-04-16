namespace warehouse_management_api.Models
{
    public abstract class BaseEntity
    {
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        public DateTime LastChangedOn { get; set; } = DateTime.UtcNow;
    }
}