using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Lab3.Data;
using System;
using System.Linq;

namespace Lab3.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new LibraryContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<LibraryContext>>()))
            {

                if (context.Books.Any())
                {
                    return;  // DB has been seeded.
                }

                context.Books.AddRange(
                    new Book
                    {
                        //Bookid = 1,
                        author = "Jeremy Clarkson",
                        title = "Can You Make This Thing Go Faster",
                        date = 2022,
                        publisher = "Penguin Random House UK",
                        user = "",
                        reserved = null,
                        leased = null
                    },

                    new Book
                    {
                        //Bookid = 2,
                        author = "Jeremy Clarkson",
                        title = "Diddly Squat - a Year on the Farm",
                        date = 2020,
                        publisher = "Penguin Random House UK",
                        user = "",
                        reserved = null,
                        leased = null
                    },

                    new Book
                    {
                        //Bookid = 3,
                        author = "Adam Mickiewicz",
                        title = "Dziady cz. 7",
                        date = 2022,
                        publisher = "Penguin Random House UK",
                        user = "",
                        reserved = null,
                        leased = null
                    },

                    new Book
                    {
                        //Bookid = 4,
                        author = "Juliusz S\u0142owacki",
                        title = "Balladyna",
                        date = 1880,
                        publisher = "PWN",
                        user = "",
                        reserved = null,
                        leased = null
                    },
                    new Book
                    {
                        //Bookid = 5,
                        author = "Margaret Atwood",
                        title = "Serce Umiera Ostatnie",
                        date = 2015,
                        publisher = "Brown Book Group",
                        user = "",
                        reserved = null,
                        leased = null
                    },
                    new Book
                    {
                        //Bookid = 6,
                        author = "Sylvia Plath",
                        title = "Szklany Klosz",
                        date = 1963,
                        publisher = "London Publishing Group",
                        user = "",
                        reserved = null,
                        leased = null
                    },
                    new Book
                    {
                        //Bookid = 7,
                        author = "Adam Mickiewicz",
                        title = "Pan Tadeusz",
                        date = 2013,
                        publisher = "PWN",
                        user = "",
                        reserved = null,
                        leased = null
                    },
                    new Book
                    {
                        //Bookid = 8,
                        author = "Zbigniew Herbert",
                        title = "Pan Cogito",
                        date = 2017,
                        publisher = "PWN",
                        user = "",
                        reserved = null,
                        leased = null
                    },
                    new Book
                    {
                        //Bookid = 9,
                        author = "Stanis\u0142aw Lem",
                        title = "Solaris",
                        date = 2007,
                        publisher = "PWN",
                        user = "",
                        reserved = null,
                        leased = null
                    },
                    new Book
                    {
                        //Bookid = 10,
                        author = "Stanis\u0142aw Lem",
                        title = "Solaris cz. 2",
                        date = 2009,
                        publisher = "PWN",
                        user = "",
                        reserved = null,
                        leased = null
                    }
                );

                context.Users.AddRange(
                    new User
                    {
                        user = "librarian",
                        pwd = "123"
                    },
                    new User
                    {
                        user = "jeremy",
                        pwd = "123"
                    },
                    new User
                    {
                        user = "james",
                        pwd = "123"
                    },
                    new User
                    {
                        user = "richard",
                        pwd = "123"
                    },
                    new User
                    {
                        user = "mary",
                        pwd = "123"
                    }

                );

                context.SaveChanges();
            }
        }
    }
}