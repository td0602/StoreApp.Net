using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoreApp.Models;
using X.PagedList;

namespace StoreApp.Areas.Admin.Controllers;

[Area("Admin")]
[Route("admin")]
// [Route("admin/homeadmin")]
public class HomeAdminController : Controller
{
    private readonly QLBanVaLiContext _dbContext;

    public HomeAdminController(QLBanVaLiContext dbContext) {
        _dbContext = dbContext;
    }

    [Route("")]
    [Route("index")]
    public IActionResult Index() {
        return View();
    }

    [Route("danhmucsanpham")]
    public IActionResult DanhMucSanPham(int? page) {
        int pageNumber = page == null || page < 1 ? 1 : page.Value;
        int pageSize = 12;
        var lstDanhMucSp = _dbContext.TDanhMucSps.AsNoTracking().OrderBy(item => item.TenSp);
        PagedList<TDanhMucSp> lst = new PagedList<TDanhMucSp>(lstDanhMucSp, pageNumber, pageSize);
        return View(lst);
    }
}