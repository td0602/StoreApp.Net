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
        // khởi tạo mặc định cho 1 vài thuộc tính của sản phẩm để ta có thể select trong form
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

    [Route("suasanpham")]
    [HttpGet]
    public IActionResult SuaSanPham(string maSanPham) {
        // khởi tạo mặc định cho 1 vài thuộc tính của sản phẩm để ta có thể select trong form
        ViewBag.MaChatLieu = new SelectList(_dbContext.TChatLieus.ToList(), "MaChatLieu", "ChatLieu");
        ViewBag.MaHangSx = new SelectList(_dbContext.THangSxes.ToList(), "MaHangSx", "HangSx");
        ViewBag.MaNuocSx = new SelectList(_dbContext.TQuocGia.ToList(), "MaNuoc", "TenNuoc");
        ViewBag.MaLoai = new SelectList(_dbContext.TLoaiSps.ToList(), "MaLoai", "Loai");
        ViewBag.MaDt = new SelectList(_dbContext.TLoaiDts.ToList(), "MaDt", "TenLoai");
        var sanPham = _dbContext.TDanhMucSps.Find(maSanPham);
        return View(sanPham);
    }
    [Route("suasanpham")]
    [HttpPost]
    [ValidateAntiForgeryToken] //Validate du lieu
    public IActionResult SuaSanPham(TDanhMucSp sanPham) {
        if(ModelState.IsValid) {
            // _dbContext.Update(sanPham);
            _dbContext.Entry(sanPham).State = EntityState.Modified;
            _dbContext.SaveChanges();
            return RedirectToAction("DanhMucSanPham", "HomeAdmin");
        }
        return View(sanPham);
    }

    [Route("xoasanpham")]
    [HttpGet]
    public IActionResult XoaSanPham(string maSanPham) {
        TempData["Message"] = "";
        // kiểm tra xem chi tiết sp có tồn tại? k có mới xóa dc
        var chiTietSps = _dbContext.TChiTietSanPhams.Where(item => item.MaSp == maSanPham).ToList();
        if(chiTietSps.Count() > 0) {
            TempData["Message"] = "Không được xóa sản phẩm này";
            return RedirectToAction("DanhMucSanPham", "HomeAdmin");
        }
        var anhSps = _dbContext.TAnhSps.Where(item => item.MaSp == maSanPham).ToList();
        if(anhSps.Any()) _dbContext.RemoveRange(anhSps);
        _dbContext.Remove(_dbContext.TDanhMucSps.Find(maSanPham));
        _dbContext.SaveChanges();
        TempData["Message"] = "Xóa sản phẩm thành công";
        return RedirectToAction("DanhMucSanPham", "HomeAdmin");
    }
}