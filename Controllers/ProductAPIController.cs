
using Microsoft.AspNetCore.Mvc;
using StoreApp.Models;

namespace StoreApp.Controllers;

[Route("api/[controller]")]
[ApiController] 
public class ProductAPIController : Controller
{
    private readonly QLBanVaLiContext _dbContext;
    public ProductAPIController(QLBanVaLiContext dbContext) {
        _dbContext = dbContext;
    }

    [HttpGet]
    public IEnumerable<TDanhMucSp> GetAllProducts() {
        return _dbContext.TDanhMucSps.ToList();
    }
}