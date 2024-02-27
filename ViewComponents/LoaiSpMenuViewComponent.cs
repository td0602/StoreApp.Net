using Microsoft.AspNetCore.Mvc;
using StoreApp.Repository;

namespace StoreApp.ViewComponents;

public class LoaiSpMenuViewComponent : ViewComponent {
    private readonly ILoaiSpRepository _loaiSp;
    public LoaiSpMenuViewComponent(ILoaiSpRepository loaiSp) {
        _loaiSp = loaiSp;
    }

    public IViewComponentResult Invoke() {
        var loaisp = _loaiSp.GetAllLoaiSp().OrderBy(item => item.Loai);
        return View(loaisp);
    }
}