using Microsoft.AspNetCore.Mvc;
using Lab1.Models;

namespace Lab1.Controllers;

public class BookController : Controller {
    public IActionResult SearchBooks(string searchString) {
        var books = JSONModel.ReadFromJSONBooks();

        if (!String.IsNullOrEmpty(searchString)){
            searchString = searchString.ToLower().Trim();
            books = books!.Where(s => s.title!.ToLower().Trim().Contains(searchString)).ToList();
        }

        return View(books);
    }

    public IActionResult ReserveBook(int bookID){
        var books = JSONModel.ReadFromJSONBooks();
        var bk = books!.FirstOrDefault(b => b.id == bookID);

        if (bk.isReserved() || bk.isLeased()){
            TempData["error"] = "This book is not available right now.";
            return RedirectToAction("SearchBooks");
        }

        var username = HttpContext.User.Identity.Name;
        bk.user = username;

        var res = DateTime.Now.AddDays(1);
        bk.reserved = res.ToString("yyyy-MM-dd");

        JSONModel.WriteToJSONBooks(books);
        return RedirectToAction("ReservedBooks");
    }

    public IActionResult DeleteReservation(int bookID){
        var books = JSONModel.ReadFromJSONBooks();
        var bk = books!.FirstOrDefault(b => b.id == bookID);

        if (!bk.isReserved() || bk.isLeased()){
            TempData["error"] = "You can't delete this reservation.";
            return RedirectToAction("ReservedBooks");
        }

        bk.reserved = "";
        bk.user = "";

        JSONModel.WriteToJSONBooks(books);
        return RedirectToAction("ReservedBooks");
    }

    public IActionResult ReservedBooks(){
        var reserved_books = JSONModel.ReadFromJSONBooks()!.Where(b => b.isReserved());

        var username = HttpContext.User.Identity.Name;

        if (username != "librarian"){
            var res = reserved_books.Where(b => b.user == username);
            return View(res.ToList());
        }

        return View(reserved_books.ToList());

    }

    public IActionResult LeaseBook(int bookID){
        var books = JSONModel.ReadFromJSONBooks();
        var bk = books!.FirstOrDefault(b => b.id == bookID);

        if (bk.isLeased()){
            TempData["error"] = "This book is already leased.";
            return RedirectToAction("SearchBooks");
        }

        string ls = DateTime.Now.ToString("yyyy-MM-dd");
        bk.leased = ls;
        bk.reserved = "";
        JSONModel.WriteToJSONBooks(books);
        return RedirectToAction("LeasedBooks");

    }

    public IActionResult MarkReturned(int bookID){
        var books = JSONModel.ReadFromJSONBooks();
        var bk = books!.FirstOrDefault(b => b.id == bookID);

        bk.leased = "";
        bk.user = "";

        JSONModel.WriteToJSONBooks(books);
        return RedirectToAction("LeasedBooks");
    }
    public IActionResult LeasedBooks(){
        var leased_books = JSONModel.ReadFromJSONBooks()!.Where(b => b.isLeased());
        var username = HttpContext.User.Identity.Name;
        if (username != "librarian"){
            var ls = leased_books.Where(b => b.user == username);
            return View(ls.ToList());
        }
        return View(leased_books.ToList());
    }
}