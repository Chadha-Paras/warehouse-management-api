namespace warehouse_management_api.DTOs.Transaction
{
    public class TransactionResponseDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int FromWarehouseId { get; set; }
        public int ToWarehouseId { get; set; }
        public int Quantity { get; set; }
        public string Type { get; set; } = string.Empty;
        public int UserId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}