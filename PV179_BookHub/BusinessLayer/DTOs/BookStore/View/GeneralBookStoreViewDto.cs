﻿using BusinessLayer.DTOs.Address.View;
using BusinessLayer.DTOs.User.View;

namespace BusinessLayer.DTOs.BookStore.View;

public class GeneralBookStoreViewDto
{
    public long Id { get; set; }
    public GeneralAddressView Address { get; set; }
    public GeneralUserViewDto Manager { get; set; }
    public required string Name { get; set; }
    public required string PhoneNumber { get; set; }
    public required string Email { get; set; }
}
