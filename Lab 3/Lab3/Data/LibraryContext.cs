using Lab3.Models;
using Microsoft.EntityFrameworkCore;

namespace Lab3.Data;

public class LibraryContext : DbContext
{
    public LibraryContext(DbContextOptions<LibraryContext> options)
        : base(options){

        }

    public DbSet<User> Users {get; set;}
    public DbSet<Book> Books {get; set;}

}