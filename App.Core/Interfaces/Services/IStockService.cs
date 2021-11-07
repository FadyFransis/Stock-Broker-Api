using App.Core.Entities;
using App.Core.Models;
using System.Linq;
using System.Threading.Tasks;

namespace App.Core.Interfaces.Services
{
    public interface IStockService : IGenericService<Stock>
    {
        Task<IQueryable<StockModel>> LoadAllStocks();
        Task<StockModel> GetStockById(long id);
        Task<StockModel> AddStock(StockModel model);
        Task<StockModel> EditStock(StockModel model);
    }
}
