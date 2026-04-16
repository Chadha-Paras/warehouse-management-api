namespace warehouse_management_api.DTOs.Inventory
{
    public class BulkResponseDto
    {
        public List<object> Success { get; set; } = new();
        public List<object> Failed { get; set; } = new();
    }
}