using Lab2.Models;
using Microsoft.EntityFrameworkCore;

namespace Lab2.Data;

public class LibraryContext : DbContext
{
    public LibraryContext(DbContextOptions<LibraryContext> options)
        : base(options){

        }

    public DbSet<User> Users {get; set;} = default!;
    public DbSet<Book> Books {get; set;} = default!;

}