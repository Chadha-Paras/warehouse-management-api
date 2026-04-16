namespace warehouse_management_api.DTOs.Inventory
{
    public class CreateInventoryDto
    {
        public int ProductId { get; set; }
        public int WarehouseId { get; set; }
        public int Quantity { get; set; }
    }
}