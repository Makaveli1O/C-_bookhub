namespace BusinessLayer.DTOs.BookStore.View
{
    public class DetailedBookStoreViewDto
    {
        public long BookStoreId { get; set; }
        public long AddressId { get; set; }
        public long ManagerId { get; set; }
        public required string Name { get; set; }
        public required string PhoneNumber { get; set; }
        public required string Email { get; set; }
    }
}
