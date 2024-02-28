using System.Diagnostics;
using App.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoreApp.Models;
using X.PagedList;

namespace StoreApp.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    // QLBanVaLiContext _dbContext = new QLBanVaLiContext();
    private readonly QLBanVaLiContext _dbContext;

    public HomeController(ILogger<HomeController> logger, QLBanVaLiContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    public IActionResult Index(int? page)
    {
        int pageSize = 8;
        int pageNumber = page == null || page < 0 ? 1 : page.Value;
        var listsanpham = _dbContext.TDanhMucSps.AsNoTracking().OrderBy(item => item.TenSp);
        PagedList<TDanhMucSp> lst = new PagedList<TDanhMucSp>(listsanpham, pageNumber, pageSize);
        return View(lst);
    }

    public IActionResult SanPhamTheoLoai(String maloai, int? page) {
        int pageSize = 8;
        int pageNumber = page == null || page < 0 ? 1 : page.Value;
        var lstsanpham = _dbContext.TDanhMucSps.AsNoTracking().Where(item => item.MaLoai==maloai).OrderBy(item => item.TenSp);
        PagedList<TDanhMucSp> lst = new PagedList<TDanhMucSp>(lstsanpham, pageNumber, pageSize);
        // Truyen ma loai vao view
        ViewBag.MaLoai = maloai;
        return View(lst);
    }

    public IActionResult ChiTietSanPham(String maSp) {
        var sanPham = _dbContext.TDanhMucSps.SingleOrDefault(item => item.MaSp == maSp);
        var anhSanPham = _dbContext.TAnhSps.Where(item => item.MaSp == maSp).ToList();
        ViewBag.anhSanPham = anhSanPham;
        return View(sanPham);
    }

    public IActionResult ProductDetail(string maSp) {
        var sanPham = _dbContext.TDanhMucSps.SingleOrDefault(item => item.MaSp == maSp);
        var anhSanPham = _dbContext.TAnhSps.Where(item => item.MaSp == maSp).ToList();
        var homeProductDetailViewModel = new HomeProductDetailViewModel(){danhMucSp=sanPham, anhSps=anhSanPham};
        return View(homeProductDetailViewModel);
    }
    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
