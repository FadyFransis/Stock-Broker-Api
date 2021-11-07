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
    public class StockController : ControllerBase
    {
        private readonly IStockService _service;
        private readonly IMapper _mapper;
        protected readonly Ilogger _logger;// = new LoggerService();

        public StockController(Ilogger logger, IMapper mapper, IStockService stockService)
        {
            _logger = logger;
            _service = stockService;
            _mapper = mapper;
          
        }

        [HttpGet]
        [Route("api/[controller]/[action]")]
        public async Task<ResponseModel<List<StockDTO>>> GetAll()
        {
            try
            {
                var result = await _service.LoadAllStocks();
                var list = _mapper.Map<List<StockDTO>>(result.ToList());
                var responseModel = HelperClass<List<StockDTO>>.CreateResponseModel(list, false, "");
                return responseModel;

            }
            catch (Exception ex)
            {
                _logger.Error("Error occured StockController\\GetAll" + " with EX: " + ex.ToString());
                return HelperClass<List<StockDTO>>.CreateResponseModel(null, true, ex.Message);
            }
        }
        [HttpGet]
        [Route("api/[controller]/[action]")]
        public async Task<ResponseModel<StockDTO>> GetStock(long Id)
        {

            try
            {
                var result = await _service.GetStockById(Id);
                var StockDTO = _mapper.Map<StockDTO>(result);
                var Stock = HelperClass<StockDTO>.CreateResponseModel(StockDTO, false, "");
                return Stock;
            }
            catch (Exception ex)
            {
                _logger.Error("Error occured StockController\\GetById" + Id + " with EX: " + ex.ToString());
                return HelperClass<StockDTO>.CreateResponseModel(null, true, ex.Message);
            }
        }
        [HttpPost]
        [Route("api/[controller]/[action]")]
        public async Task<ResponseModel<StockDTO>> AddStock([FromBody] StockDTO model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return HelperClass<StockDTO>.CreateResponseModel(null, true,
                      string.Join(",", ModelState.Values
                      .SelectMany(v => v.Errors)
                      .Select(e => e.ErrorMessage)));

                var StockModel = _mapper.Map<StockModel>(model);
                var result = await _service.AddStock(StockModel);
                var StockDTO = _mapper.Map<StockDTO>(result);
                var broker = HelperClass<StockDTO>.CreateResponseModel(StockDTO, false, "");
                return broker;
            }

            catch (Exception ex)
            {
                _logger.Error("Error occured StockController\\Add" + " with EX: " + ex.Message);
                return HelperClass<StockDTO>.CreateResponseModel(null, true, ex.Message);
            }
        }
        [HttpPost]
        [Route("api/[controller]/[action]")]
        public async Task<ResponseModel<StockDTO>> UpdateStock([FromBody] StockDTO model)
        {
            try
            {
                var StockModel = _mapper.Map<StockModel>(model);
                var result = await _service.EditStock(StockModel);
                var StockDTO = _mapper.Map<StockDTO>(result);
                var broker = HelperClass<StockDTO>.CreateResponseModel(StockDTO, false, "");
                return broker;
            }

            catch (Exception ex)
            {
                _logger.Error("Error occured StockController\\UpdateOrder" + " with EX: " + ex.Message);
                return HelperClass<StockDTO>.CreateResponseModel(null, true, ex.Message);
            }
        }
        [HttpGet]
        [Route("api/[controller]/[action]")]
        public async Task<ResponseModel<BooleanDescriptionResultDTO>> DeleteStock(long Id)
        {
            try
            {

                var result = await _service.ChangeStatus(Id,false);
                var broker = HelperClass<BooleanDescriptionResultDTO>.CreateResponseModel(null, false, "Deleted");
                return broker;
            }

            catch (Exception ex)
            {
                _logger.Error("Error occured StockController\\CancelOrder" + " with EX: " + ex.Message);
                return HelperClass<BooleanDescriptionResultDTO>.CreateResponseModel(null, true, ex.Message);
            }
        }
    }
}
