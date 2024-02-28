using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StoreApp.Models;

namespace StoreApp.Areas_Admin02_Controllers
{
    public class HomeAdmin : Controller
    {
        private readonly QLBanVaLiContext _context;

        public HomeAdmin(QLBanVaLiContext context)
        {
            _context = context;
        }

        // GET: HomeAdmin
        public async Task<IActionResult> Index()
        {
            var qLBanVaLiContext = _context.TDanhMucSps.Include(t => t.MaChatLieuNavigation).Include(t => t.MaDtNavigation).Include(t => t.MaHangSxNavigation).Include(t => t.MaLoaiNavigation).Include(t => t.MaNuocSxNavigation);
            return View(await qLBanVaLiContext.ToListAsync());
        }

        // GET: HomeAdmin/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.TDanhMucSps == null)
            {
                return NotFound();
            }

            var tDanhMucSp = await _context.TDanhMucSps
                .Include(t => t.MaChatLieuNavigation)
                .Include(t => t.MaDtNavigation)
                .Include(t => t.MaHangSxNavigation)
                .Include(t => t.MaLoaiNavigation)
                .Include(t => t.MaNuocSxNavigation)
                .FirstOrDefaultAsync(m => m.MaSp == id);
            if (tDanhMucSp == null)
            {
                return NotFound();
            }

            return View(tDanhMucSp);
        }

        // GET: HomeAdmin/Create
        public IActionResult Create()
        {
            ViewData["MaChatLieu"] = new SelectList(_context.TChatLieus, "MaChatLieu", "MaChatLieu");
            ViewData["MaDt"] = new SelectList(_context.TLoaiDts, "MaDt", "MaDt");
            ViewData["MaHangSx"] = new SelectList(_context.THangSxes, "MaHangSx", "MaHangSx");
            ViewData["MaLoai"] = new SelectList(_context.TLoaiSps, "MaLoai", "MaLoai");
            ViewData["MaNuocSx"] = new SelectList(_context.TQuocGia, "MaNuoc", "MaNuoc");
            return View();
        }

        // POST: HomeAdmin/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaSp,TenSp,MaChatLieu,NganLapTop,Model,CanNang,DoNoi,MaHangSx,MaNuocSx,MaDacTinh,Website,ThoiGianBaoHanh,GioiThieuSp,ChietKhau,MaLoai,MaDt,AnhDaiDien,GiaNhoNhat,GiaLonNhat")] TDanhMucSp tDanhMucSp)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tDanhMucSp);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaChatLieu"] = new SelectList(_context.TChatLieus, "MaChatLieu", "MaChatLieu", tDanhMucSp.MaChatLieu);
            ViewData["MaDt"] = new SelectList(_context.TLoaiDts, "MaDt", "MaDt", tDanhMucSp.MaDt);
            ViewData["MaHangSx"] = new SelectList(_context.THangSxes, "MaHangSx", "MaHangSx", tDanhMucSp.MaHangSx);
            ViewData["MaLoai"] = new SelectList(_context.TLoaiSps, "MaLoai", "MaLoai", tDanhMucSp.MaLoai);
            ViewData["MaNuocSx"] = new SelectList(_context.TQuocGia, "MaNuoc", "MaNuoc", tDanhMucSp.MaNuocSx);
            return View(tDanhMucSp);
        }

        // GET: HomeAdmin/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.TDanhMucSps == null)
            {
                return NotFound();
            }

            var tDanhMucSp = await _context.TDanhMucSps.FindAsync(id);
            if (tDanhMucSp == null)
            {
                return NotFound();
            }
            ViewData["MaChatLieu"] = new SelectList(_context.TChatLieus, "MaChatLieu", "MaChatLieu", tDanhMucSp.MaChatLieu);
            ViewData["MaDt"] = new SelectList(_context.TLoaiDts, "MaDt", "MaDt", tDanhMucSp.MaDt);
            ViewData["MaHangSx"] = new SelectList(_context.THangSxes, "MaHangSx", "MaHangSx", tDanhMucSp.MaHangSx);
            ViewData["MaLoai"] = new SelectList(_context.TLoaiSps, "MaLoai", "MaLoai", tDanhMucSp.MaLoai);
            ViewData["MaNuocSx"] = new SelectList(_context.TQuocGia, "MaNuoc", "MaNuoc", tDanhMucSp.MaNuocSx);
            return View(tDanhMucSp);
        }

        // POST: HomeAdmin/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("MaSp,TenSp,MaChatLieu,NganLapTop,Model,CanNang,DoNoi,MaHangSx,MaNuocSx,MaDacTinh,Website,ThoiGianBaoHanh,GioiThieuSp,ChietKhau,MaLoai,MaDt,AnhDaiDien,GiaNhoNhat,GiaLonNhat")] TDanhMucSp tDanhMucSp)
        {
            if (id != tDanhMucSp.MaSp)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tDanhMucSp);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TDanhMucSpExists(tDanhMucSp.MaSp))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaChatLieu"] = new SelectList(_context.TChatLieus, "MaChatLieu", "MaChatLieu", tDanhMucSp.MaChatLieu);
            ViewData["MaDt"] = new SelectList(_context.TLoaiDts, "MaDt", "MaDt", tDanhMucSp.MaDt);
            ViewData["MaHangSx"] = new SelectList(_context.THangSxes, "MaHangSx", "MaHangSx", tDanhMucSp.MaHangSx);
            ViewData["MaLoai"] = new SelectList(_context.TLoaiSps, "MaLoai", "MaLoai", tDanhMucSp.MaLoai);
            ViewData["MaNuocSx"] = new SelectList(_context.TQuocGia, "MaNuoc", "MaNuoc", tDanhMucSp.MaNuocSx);
            return View(tDanhMucSp);
        }

        // GET: HomeAdmin/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.TDanhMucSps == null)
            {
                return NotFound();
            }

            var tDanhMucSp = await _context.TDanhMucSps
                .Include(t => t.MaChatLieuNavigation)
                .Include(t => t.MaDtNavigation)
                .Include(t => t.MaHangSxNavigation)
                .Include(t => t.MaLoaiNavigation)
                .Include(t => t.MaNuocSxNavigation)
                .FirstOrDefaultAsync(m => m.MaSp == id);
            if (tDanhMucSp == null)
            {
                return NotFound();
            }

            return View(tDanhMucSp);
        }

        // POST: HomeAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.TDanhMucSps == null)
            {
                return Problem("Entity set 'QLBanVaLiContext.TDanhMucSps'  is null.");
            }
            var tDanhMucSp = await _context.TDanhMucSps.FindAsync(id);
            if (tDanhMucSp != null)
            {
                _context.TDanhMucSps.Remove(tDanhMucSp);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TDanhMucSpExists(string id)
        {
          return (_context.TDanhMucSps?.Any(e => e.MaSp == id)).GetValueOrDefault();
        }
    }
}
