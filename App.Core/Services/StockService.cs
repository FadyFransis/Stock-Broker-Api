using App.Common.Models;
using App.Common.Services.Logger;
using App.Common.Services.Mail;
using App.Core.Entities;
using App.Core.Entities.Base;
using App.Core.Interfaces.Repository;
using App.Core.Interfaces.Services;
using App.Core.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace App.Core.Services
{
    public class StockService : GenericService<Stock>, IStockService
    {

      
        public StockService(
            IGenericRepository<Stock> oRepository,
            Ilogger logger,
            IMapper mapper)
            : base(oRepository, logger, mapper)
        {

          
        }

        public async Task<IQueryable<StockModel>> LoadAllStocks()
        {
            var res = await GetAll<Stock>("id", null);
            var Stocks = res.Result.Select(x => new StockModel
            {
                Id = x.Id,
                Name = x.Name,
            });
            return Stocks;
        }
        public async Task<StockModel> AddStock(StockModel model)
        {
            var newStock = mapper.Map<Stock>(model);
            var result = await Add(newStock);
            var StockModel = mapper.Map<StockModel>(result);
            return StockModel;
        }
        public async Task<StockModel> EditStock(StockModel model)
        {
            var Stock = mapper.Map<Stock>(model);
            var result = await Update(Stock.Id, Stock);
            var StockModel = mapper.Map<StockModel>(result);
            return StockModel;
        }
        public async Task<StockModel> GetStockById(long id)
        {
            var area = await GetById<Stock>(id);
            var StockModel = new StockModel
            {
                Id = area.Id,
                Name = area.Name,
            };

            return StockModel;
        }


    
    }
}
