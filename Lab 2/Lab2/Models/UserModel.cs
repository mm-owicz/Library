namespace Lab2.Models;
using System.ComponentModel.DataAnnotations;

public class User
{
    [Key]
    public string? user { get; set; }
    public string? pwd {get; set;}

    [Timestamp]
    public byte[]? RowVersion {get; set;}
}
