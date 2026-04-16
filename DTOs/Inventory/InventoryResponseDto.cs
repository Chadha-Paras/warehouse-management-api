namespace warehouse_management_api.DTOs.Inventory
{
    public class InventoryResponseDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int WarehouseId { get; set; }
        public int Quantity { get; set; }
    }
}