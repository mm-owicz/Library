using Microsoft.AspNetCore.Mvc;
using Lab2.Models;
using Lab2.Data;
using Microsoft.EntityFrameworkCore;

namespace Lab2.Controllers;

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

        var books = _context.Books!.ToList();

        if (!String.IsNullOrEmpty(searchString)){
            searchString = searchString.ToLower().Trim();
            books = books!.Where(s => s.title!.ToLower().Trim().Contains(searchString)).ToList();
        }

        return View(books);
    }

    public IActionResult ReserveBook(int bookID){
        var books = _context.Books!.ToList();
        var bk = books!.FirstOrDefault(b => b.id == bookID);

        if (bk.isReserved() || bk.isLeased()){
            TempData["error"] = "This book is not available right now.";
            return RedirectToAction("SearchBooks");
        }

        var username = HttpContext.User.Identity.Name;
        bk.user = username;

        var res = DateTime.Now.AddDays(1);
        bk.reserved = res.ToString("yyyy-MM-dd");

        _context.SaveChanges();
        return RedirectToAction("ReservedBooks");
    }

    public IActionResult DeleteReservation(int bookID){
        var books = _context.Books!.ToList();
        var bk = books!.FirstOrDefault(b => b.id == bookID);

        if (!bk.isReserved() || bk.isLeased()){
            TempData["error"] = "You can't delete this reservation.";
            return RedirectToAction("ReservedBooks");
        }

        bk.reserved = "";
        bk.user = "";

        _context.SaveChanges();
        return RedirectToAction("ReservedBooks");
    }

    public IActionResult ReservedBooks(){
        var username = HttpContext.User.Identity.Name;

        if (username is null){
            return RedirectToAction("LogIn", "User");
        }
        var books = _context.Books!.ToList();
        var reserved_books = books!.Where(b => b.isReserved());

        if (username != "librarian"){
            var res = reserved_books.Where(b => b.user == username);
            return View(res.ToList());
        }

        return View(reserved_books.ToList());

    }

    public IActionResult LeaseBook(int bookID){
        var books = _context.Books!.ToList();
        var bk = books!.FirstOrDefault(b => b.id == bookID);

        if (bk.isLeased()){
            TempData["error"] = "This book is already leased.";
            return RedirectToAction("SearchBooks");
        }

        string ls = DateTime.Now.ToString("yyyy-MM-dd");
        bk.leased = ls;
        bk.reserved = "";
        _context.SaveChanges();
        return RedirectToAction("LeasedBooks");

    }

    public IActionResult MarkReturned(int bookID){
        var books = _context.Books!.ToList();
        var bk = books!.FirstOrDefault(b => b.id == bookID);

        bk.leased = "";
        bk.user = "";

        _context.SaveChanges();
        return RedirectToAction("LeasedBooks");
    }
    public IActionResult LeasedBooks(){
        var username = HttpContext.User.Identity.Name;

        if (username is null){
            return RedirectToAction("LogIn", "User");
        }
        var books = _context.Books!.ToList();
        var leased_books = books!.Where(b => b.isLeased());
        if (username != "librarian"){
            var ls = leased_books.Where(b => b.user == username);
            return View(ls.ToList());
        }
        return View(leased_books.ToList());
    }
}