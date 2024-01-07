﻿using DataAccessLayer.Models.Enums;

namespace BusinessLayer.DTOs.User.View;

public class GeneralUserViewDto
{
    public int Id { get; set; }
    public string UserName { get; set; }
    public string PasswordHash { get; set; }
    public string Salt { get; set; }
    public UserRole Role { get; set; }
}