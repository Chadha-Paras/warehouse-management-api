namespace warehouse_management_api.Models
{
    public class InventoryTransaction: BaseEntity
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public int FromWarehouseId { get; set; }

        public int ToWarehouseId { get; set; }

        public int Quantity { get; set; }

        public string Type { get; set; } = string.Empty; // ADD / TRANSFER
        
        public int UserId { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}