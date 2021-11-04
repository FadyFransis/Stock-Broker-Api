using App.API.DTOs;
using App.API.Helper;
using App.Common.Services.Logger;
using App.Core.Entities;
using App.Core.Interfaces.Services;
using App.Core.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

namespace App.API.Controllers
{
    public class BrokerControlller : ControllerBase
    {
        private readonly IOrderService _service;
        private readonly IMapper _mapper;
        protected readonly Ilogger _logger;// = new LoggerService();

        public BrokerControlller(Ilogger logger, IMapper mapper, IOrderService OrderService)
        {
            _logger = logger;
            _service = OrderService;
            _mapper = mapper;
          
        }

        [HttpGet]
        [Route("api/[controller]/[action]")]
        public async Task<ResponseModel<List<Broker>>> GetAll()
        {
            var result = await _service.GetAll<Broker>("id", null);
            return result;
        }
        [HttpGet]
        [Route("api/[controller]/[action]")]
        public async Task<ResponseModel<Broker>> GetBroker(long Id)
        {
            return await _service.GetById<Broker>(Id);
        }
        [HttpPost]
        [Route("api/[controller]/[action]")]
        public async Task<ResponseModelBase<LookUpModelBase>> AddBroker([FromBody] LookUpModelBase model)
        {
            ResponseModelBase<LookUpModelBase> retModel = new ResponseModelBase<LookUpModelBase>()
            {
                IsSubmitted = true,
                IsSubmittedSuccessfully = false
            };

            if (!ModelState.IsValid)
            {
                retModel.Errors = Errors.GetErrorsInModelState(ModelState);
                return retModel;
            }

            var Broker = _mapper.Map<Broker>(model);
            var ResponseModel = await _service.Add<Broker, Broker>(Broker);

            if (!ResponseModel.Success)
            {
                retModel.Errors = Errors.GetErrorsInServiceReponse(ResponseModel.Errors.ToList());
            }
            else
            {
                retModel.IsSubmitted = retModel.IsSubmittedSuccessfully = true;
                var newmodel = _mapper.Map<LookUpModelBase>(ResponseModel.Result);
                retModel.Model = newmodel;
            }

            return retModel;
        }
        [HttpPost]
        [Route("api/[controller]/[action]")]
        public async Task<ResponseModelBase<LookUpModelBase>> UpdateBroker([FromBody] LookUpModelBase model)
        {
            ResponseModelBase<LookUpModelBase> retModel = new ResponseModelBase<LookUpModelBase>()
            {
                IsSubmitted = true,
                IsSubmittedSuccessfully = false
            };
            if (!model.Id.HasValue)
            {
                retModel.Errors.Add("Id Property is required");
                return retModel;
            }

            if (!ModelState.IsValid)
            {
                retModel.Errors = Errors.GetErrorsInModelState(ModelState);
                return retModel;
            }

            var Broker = _mapper.Map<Broker>(model);
            var ResponseModel = await _service.Update<Broker, Broker>(model.Id.Value, Broker);

            if (!ResponseModel.Success)
            {
                retModel.Errors = Errors.GetErrorsInServiceReponse(ResponseModel.Errors.ToList());
            }
            else
            {
                retModel.IsSubmitted = retModel.IsSubmittedSuccessfully = true;
                var newmodel = _mapper.Map<LookUpModelBase>(ResponseModel.Result);
                retModel.Model = newmodel;
            }

            return retModel;
        }
        [HttpGet]
        [Route("api/[controller]/[action]")]
        public async Task<ResponseModelBase<LookUpModelBase>> DeleteBroker(long Id)
        {
            ResponseModelBase<LookUpModelBase> retModel = new ResponseModelBase<LookUpModelBase>()
            {
                IsSubmitted = true,
                IsSubmittedSuccessfully = false
            };
            //logical delete 
            var ResponseModel = await _service.ChangeStatus(Id, false);

            if (!ResponseModel.Success)
            {
                retModel.Errors = Errors.GetErrorsInServiceReponse(ResponseModel.Errors.ToList());
            }
            else
            {
                retModel.IsSubmitted = retModel.IsSubmittedSuccessfully = true;
            }

            return retModel;
        }
    }
}
