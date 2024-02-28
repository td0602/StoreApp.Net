
using Microsoft.AspNetCore.Mvc;
using StoreApp.Models;

namespace StoreApp.Controllers;

public class AccessController : Controller
{
    private readonly QLBanVaLiContext _dbContext;
    public AccessController(QLBanVaLiContext dcContext) {
        _dbContext = dcContext;
    }

    [HttpGet]
    public IActionResult Login() {
        if(HttpContext.Session.GetString("UserName") == null) {
            return View();
        } else {
            return RedirectToAction("Index", "Home");
        } 
    }

    [HttpPost]
    public IActionResult Login(TUser user) {
        // ktra nếu chưa đăng nhập thì nạp tk này vào
        if(HttpContext.Session.GetString("UserName") == null) {
            // kiểm tra xem user có trong CSDL không?
            var u = _dbContext.TUsers.Where(item => item.Username.Equals(user.Username) && 
            item.Password.Equals(user.Password)).FirstOrDefault();
            if(u != null) {
                // sau khi đăng nhập thì nạp tài khoản đó vào Session
                HttpContext.Session.SetString("UserName", u.Username.ToString());
                return RedirectToAction("Index", "Home");
            }
        }
        return View();
    }

    public IActionResult Logout() {
        HttpContext.Session.Clear();
        HttpContext.Session.Remove("UserName");
        return RedirectToAction("Login", "Access");
    }
}