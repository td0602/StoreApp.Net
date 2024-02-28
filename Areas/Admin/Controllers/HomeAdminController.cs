using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

    [Route("themsanphammoi")]
    [HttpGet]
    public IActionResult ThemSanPhamMoi() {
        // khởi tạo mặc định cho 1 vài thuộc tính của sản phẩm
        ViewBag.MaChatLieu = new SelectList(_dbContext.TChatLieus.ToList(), "MaChatLieu", "ChatLieu");
        ViewBag.MaHangSx = new SelectList(_dbContext.THangSxes.ToList(), "MaHangSx", "HangSx");
        ViewBag.MaNuocSx = new SelectList(_dbContext.TQuocGia.ToList(), "MaNuoc", "TenNuoc");
        ViewBag.MaLoai = new SelectList(_dbContext.TLoaiSps.ToList(), "MaLoai", "Loai");
        ViewBag.MaDt = new SelectList(_dbContext.TLoaiDts.ToList(), "MaDt", "TenLoai");
        return View();
    }
    [Route("themsanphammoi")]
    [HttpPost]
    [ValidateAntiForgeryToken] //Validate du lieu
    public IActionResult ThemSanPhamMoi(TDanhMucSp sanPham) {
        if(ModelState.IsValid) {
            _dbContext.TDanhMucSps.Add(sanPham);
            _dbContext.SaveChanges();
            return RedirectToAction("DanhMucSanPham");
        }
        return View(sanPham);
    }
}