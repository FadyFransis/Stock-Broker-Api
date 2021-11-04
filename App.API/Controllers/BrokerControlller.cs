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
        private readonly IBrokerService _service;
        private readonly IMapper _mapper;
        protected readonly Ilogger _logger;// = new LoggerService();

        public BrokerControlller(Ilogger logger, IMapper mapper, IBrokerService brokerService)
        {
            _logger = logger;
            _service = brokerService;
            _mapper = mapper;
          
        }

        [HttpGet]
        [Route("api/[controller]/[action]")]
        public async Task<ResponseModel<List<BrokerDTO>>> GetAll()
        {
            try
            {
                var result = await _service.LoadAllBrokers();
                var list = _mapper.Map<List<BrokerDTO>>(result.ToList());
                var responseModel = HelperClass<List<BrokerDTO>>.CreateResponseModel(list, false, "");
                return responseModel;

            }
            catch (Exception ex)
            {
                _logger.Error("Error occured AreaController\\GetAll" + " with EX: " + ex.ToString());
                return HelperClass<List<BrokerDTO>>.CreateResponseModel(null, true, ex.Message);
            }
        }
        [HttpGet]
        [Route("api/[controller]/[action]")]
        public async Task<ResponseModel<BrokerDTO>> GetBroker(long Id)
        {

            try
            {
                var result = await _service.GetBrokerById(Id);
                var BrokerDTO = _mapper.Map<BrokerDTO>(result);
                var Broker = HelperClass<BrokerDTO>.CreateResponseModel(BrokerDTO, false, "");
                return Broker;
            }
            catch (Exception ex)
            {
                _logger.Error("Error occured AreaController\\GetById" + Id + " with EX: " + ex.ToString());
                return HelperClass<BrokerDTO>.CreateResponseModel(null, true, ex.Message);
            }
        }
        [HttpPost]
        [Route("api/[controller]/[action]")]
        public async Task<ResponseModel<BrokerDTO>> AddBroker([FromBody] BrokerDTO model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return HelperClass<BrokerDTO>.CreateResponseModel(null, true,
                      string.Join(",", ModelState.Values
                      .SelectMany(v => v.Errors)
                      .Select(e => e.ErrorMessage)));

                var BrokerModel = _mapper.Map<BrokerModel>(model);
                var result = await _service.AddBroker(BrokerModel);
                var BrokerDTO = _mapper.Map<BrokerDTO>(result);
                var broker = HelperClass<BrokerDTO>.CreateResponseModel(BrokerDTO, false, "");
                return broker;
            }

            catch (Exception ex)
            {
                _logger.Error("Error occured OrderController\\Add" + " with EX: " + ex.Message);
                return HelperClass<BrokerDTO>.CreateResponseModel(null, true, ex.Message);
            }
        }
        [HttpPost]
        [Route("api/[controller]/[action]")]
        public async Task<ResponseModel<BrokerDTO>> UpdateBroker([FromBody] BrokerDTO model)
        {
            try
            {
                var BrokerModel = _mapper.Map<BrokerModel>(model);
                var result = await _service.EditBroker(BrokerModel);
                var BrokerDTO = _mapper.Map<BrokerDTO>(result);
                var broker = HelperClass<BrokerDTO>.CreateResponseModel(BrokerDTO, false, "");
                return broker;
            }

            catch (Exception ex)
            {
                _logger.Error("Error occured OrderController\\UpdateOrder" + " with EX: " + ex.Message);
                return HelperClass<BrokerDTO>.CreateResponseModel(null, true, ex.Message);
            }
        }
        [HttpGet]
        [Route("api/[controller]/[action]")]
        public async Task<ResponseModel<BooleanDescriptionResultDTO>> DeleteBroker(long Id)
        {
            try
            {

                var result = await _service.ChangeStatus(Id,false);
                var order = HelperClass<BooleanDescriptionResultDTO>.CreateResponseModel(null, false, "Deleted");
                return order;
            }

            catch (Exception ex)
            {
                _logger.Error("Error occured OrderController\\CancelOrder" + " with EX: " + ex.Message);
                return HelperClass<BooleanDescriptionResultDTO>.CreateResponseModel(null, true, ex.Message);
            }
        }
    }
}
