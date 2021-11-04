using App.Common.Models;
using App.Common.Services.Logger;
using App.Common.Services.Mail;
using App.Core.Entities;
using App.Core.Entities.Base;
using App.Core.Helper;
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

        private readonly IMailNotification _mailnotification;
        public BrokerService(IMailNotification mailnotification,
            IGenericRepository<Broker> oRepository,
            Ilogger logger,
            IMapper mapper)
            : base(oRepository, logger, mapper)
        {

            _mailnotification = mailnotification;
        }

    }
}
