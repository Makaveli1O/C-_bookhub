﻿namespace MVC.Models.Book;

public class PrimaryAuthorViewModel
{
    public long Id { get; set; }
    public required string Title { get; set; }
    public required string ISBN { get; set; }
    public long AuthorId { get; set; }
    public bool IsPrimary { get; set; }
    public bool Force { get; set; }
}
