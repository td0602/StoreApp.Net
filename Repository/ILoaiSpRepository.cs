using System.Collections;
using StoreApp.Models;

namespace StoreApp.Repository;

public interface ILoaiSpRepository {
    TLoaiSp Add(TLoaiSp loaiSp);
    TLoaiSp Update(TLoaiSp loaiSp);
    TLoaiSp Delete(String maloaiSp);
    TLoaiSp GetLoaiSp(String maloaiSp);
    IEnumerable<TLoaiSp> GetAllLoaiSp();
}