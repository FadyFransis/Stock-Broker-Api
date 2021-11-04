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
    public class BrokerService : GenericService<Broker>, IBrokerService
    {

      
        public BrokerService(
            IGenericRepository<Broker> oRepository,
            Ilogger logger,
            IMapper mapper)
            : base(oRepository, logger, mapper)
        {

          
        }

        public async Task<IQueryable<BrokerModel>> LoadAllBrokers()
        {
            var res = await GetAll<Broker>("id", null);
            var Brokers = res.Result.Select(x => new BrokerModel
            {
                Id = x.Id,
                Name = x.Name,
            });
            return Brokers;
        }
        public async Task<BrokerModel> AddBroker(BrokerModel model)
        {
            var newBroker = mapper.Map<Broker>(model);
            var result = await Add(newBroker);
            var BrokerModel = mapper.Map<BrokerModel>(result);
            return BrokerModel;
        }
        public async Task<BrokerModel> EditBroker(BrokerModel model)
        {
            var Broker = mapper.Map<Broker>(model);
            var result = await Update(Broker.Id, Broker);
            var BrokerModel = mapper.Map<BrokerModel>(result);
            return BrokerModel;
        }
        public async Task<BrokerModel> GetBrokerById(long id)
        {
            var area = await GetById<Broker>(id);
            var BrokerModel = new BrokerModel
            {
                Id = area.Id,
                Name = area.Name,
            };

            return BrokerModel;
        }


    
    }
}
