using System.ComponentModel.DataAnnotations;

namespace warehouse_management_api.Models
{ 
    public class Warehouse: BaseEntity
    {
        public int Id { get; set; }

        [Required, MaxLength(200)]
        public string Name { get; set; } = string.Empty;

        public ICollection<Inventory> Inventories { get; set; } = new List<Inventory>();
    }
}