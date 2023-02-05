using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Lab2.Models;
using Lab2.Data;

namespace Lab2.Controllers;

public class UserController : Controller {
private readonly LibraryContext _context;
public UserController(LibraryContext context){
    _context = context;
}
public IActionResult LogIn(){return View();}

[HttpPost]
public IActionResult LogIn(string user, string pwd){
    var users = _context.Users;
    var usr = users!.FirstOrDefault(u => u.user == user);
    if(usr is null){
        TempData["error"] = "This user does not exist. Try again or Sign In.";
        return RedirectToAction("LogIn");
    }
    if(usr.pwd != pwd){
        TempData["error"] = "Incorrect password!";
        return RedirectToAction("LogIn");
    }

    var role = (user == "librarian") ? "librarian" : "user";

    var claims = new List<Claim>()
    {
        new Claim(ClaimTypes.Name, user),
        new Claim(ClaimTypes.Role, role),
    };

    var claimsIdentity = new ClaimsIdentity(
            claims, CookieAuthenticationDefaults.AuthenticationScheme);

    HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity));

    return LocalRedirect("/");
}

    public IActionResult LogOut(){
        HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        return LocalRedirect("/");
    }


public IActionResult SignIn(){return View();}
[HttpPost]
public IActionResult SignIn(string user, string pwd){
    var users = _context.Users;
    var usr = users!.FirstOrDefault(u => u.user == user);
    if (usr is not null){
            TempData["error"] = "Username already occupied!";
        return RedirectToAction("SignIn");
    }

    User new_user = new User();
    new_user.user = user;
    new_user.pwd = pwd;

    users.Add(new_user);
    _context.SaveChanges();
    return RedirectToAction("LogIn");
}

    public IActionResult AccountDisplay(){
        if (HttpContext.User.Identity.Name is null){
            return RedirectToAction("LogIn");
        }
        return View();
    }

    public IActionResult DelAccount(string username){
        var users = _context.Users;
        var usr = users!.FirstOrDefault(u => u.user == username);

        var books = _context.Books.ToList();
        var leased = books!.FirstOrDefault(b => b.user == username && b.isLeased());

        if (leased is not null){
            TempData["error"] = "You cannot delete an user with leased books.";
            return RedirectToAction("AccountDisplay");
        }

        foreach (var bk in books){
            if(bk.user == username && bk.isReserved()){
            bk.user = "";
            bk.reserved = "";
            }
        }
        users!.Remove(usr);
        _context.Users.Remove(usr);

        _context.SaveChanges();
        return RedirectToAction("LogOut");

    }

}