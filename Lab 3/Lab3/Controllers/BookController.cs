using Microsoft.AspNetCore.Mvc;
using Lab3.Models;
using Lab3.Data;
using Microsoft.EntityFrameworkCore;

namespace Lab3.Controllers;

public class BookController : Controller {

    private readonly LibraryContext _context;
    public BookController(LibraryContext context){
        _context = context;
    }

    public IActionResult SearchBooks(string searchString) {
        var username = HttpContext.User.Identity.Name;

        if (username is null){
            return RedirectToAction("LogIn", "User");
        }

        var books = _context.Books!.AsNoTracking().ToList();

        if (!String.IsNullOrEmpty(searchString)){
            searchString = searchString.ToLower().Trim();
            books = books!.Where(s => s.title!.ToLower().Trim().Contains(searchString)).ToList();
        }

        books = books!.Where(b => b.isReserved() == false && b.isLeased() == false).ToList();
        return View(books);
    }

    [HttpPost]
    public IActionResult ReserveBook(int bookID, byte[] rv){
        var books = _context.Books!.ToList();
        var bk = books!.FirstOrDefault(b => b.Bookid == bookID);

        var username = HttpContext.User.Identity.Name;
        bk.user = username;

        var res = DateTime.Now.AddDays(1);
        bk.reserved = res;

        _context.Entry(bk).Property("RowVersion").OriginalValue = rv;

        try{
            _context.Entry(bk).State = EntityState.Modified;
            _context.SaveChanges();
            return RedirectToAction("ReservedBooks");
        }catch(DbUpdateException){
            TempData["error"] = "Concurrency error. Someone already reserved this book.";
            return RedirectToAction("SearchBooks");
        }
    }

    [HttpPost]
    public IActionResult DeleteReservation(int bookID, byte[] rv){
        var books = _context.Books!;
        var bk = books!.FirstOrDefault(b => b.Bookid == bookID);

        bk.reserved = null;
        bk.user = null;

        _context.Entry(bk).Property("RowVersion").OriginalValue = rv;


        try{
            _context.Entry(bk).State = EntityState.Modified;
            _context.SaveChanges();
        }catch(DbUpdateConcurrencyException ex){
            TempData["error"] = "Concurrency error. Reservation already deleted.";
        }

        return RedirectToAction("ReservedBooks");
    }

    public IActionResult ReservedBooks(){
        var username = HttpContext.User.Identity.Name;

        if (username is null){
            return RedirectToAction("LogIn", "User");
        }

        var books = _context.Books!;
        var reserved_books = books!
            .AsNoTracking()
            .AsEnumerable()
            .Where(b => b.isReserved()).ToList();

        if (username != "librarian"){
            var res = reserved_books.Where(b => b.user == username);
            return View(res.ToList());
        }

        return View(reserved_books.ToList());

    }

    [HttpPost]
    public IActionResult LeaseBook(int bookID, byte[] rv){
        var books = _context.Books!.ToList();
        var bk = books!.FirstOrDefault(b => b.Bookid == bookID);

        var ls = DateTime.Now;
        bk.leased = ls;
        bk.reserved = null;

        _context.Entry(bk).Property("RowVersion").OriginalValue = rv;

        try{
            _context.Entry(bk).State = EntityState.Modified;
            _context.SaveChanges();
        }catch(DbUpdateConcurrencyException){
            TempData["error"] = "Concurrency error. Book already leased.";
        }

        return RedirectToAction("LeasedBooks");

    }

    [HttpPost]
    public IActionResult MarkReturned(int bookID, byte[] rv){
        var books = _context.Books!;
        var bk = books!.FirstOrDefault(b => b.Bookid == bookID);

        bk.leased = null;
        bk.user = "";

        _context.Entry(bk).Property("RowVersion").OriginalValue = rv;

        try{
            _context.Entry(bk).State = EntityState.Modified;
            _context.SaveChanges();
        } catch(DbUpdateConcurrencyException){
            TempData["error"] = "Concurrency error. Book already marked returned.";
        }
        return RedirectToAction("LeasedBooks");
    }
    public IActionResult LeasedBooks(){
        var username = HttpContext.User.Identity.Name;

        if (username is null){
            return RedirectToAction("LogIn", "User");
        }
        var books = _context.Books!;
        var leased_books = books!
            .AsNoTracking()
            .AsEnumerable()
            .Where(b => b.isLeased()).ToList();
        if (username != "librarian"){
            var ls = leased_books.Where(b => b.user == username);
            return View(ls.ToList());
        }
        return View(leased_books.ToList());
    }
}