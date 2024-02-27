using StoreApp.Models;

namespace StoreApp.Repository;

public class LoaiSpRepository : ILoaiSpRepository
{
    private readonly QLBanVaLiContext _dbContext;
    public LoaiSpRepository(QLBanVaLiContext dbContext) {
        _dbContext = dbContext;
    }
    public TLoaiSp Add(TLoaiSp loaiSp)
    {
        _dbContext.TLoaiSps.Add(loaiSp);
        _dbContext.SaveChanges();
        return loaiSp;
    }

    public TLoaiSp Delete(string maloaiSp)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<TLoaiSp> GetAllLoaiSp()
    {
        return _dbContext.TLoaiSps;
    }

    public TLoaiSp GetLoaiSp(string maloaiSp)
    {
        return _dbContext.TLoaiSps.Find(maloaiSp);
    }

    public TLoaiSp Update(TLoaiSp loaiSp)
    {
        _dbContext.TLoaiSps.Update(loaiSp);
        _dbContext.SaveChanges();
        return loaiSp;
    }
}