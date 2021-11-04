﻿using App.Core.Entities;
using App.Core.Models;
using System.Linq;
using System.Threading.Tasks;

namespace App.Core.Interfaces.Services
{
    public interface IBrokerService : IGenericService<Order>
    {
        Task<IQueryable<BrokerModel>> LoadAllBrokers();
        Task<BrokerModel> GetBrokerById(long id);
        Task<BrokerModel> AddBroker(BrokerModel model);
        Task<BrokerModel> EditBroker(BrokerModel model);
        Task<BrokerModel> DeleteBroker(BrokerModel model);
    }
}
