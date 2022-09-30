using ContractAPI.CanceledContractInfo.service;
using ContractAPI.DataAccess;
using ContractAPI.Models;
using ContractAPI.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ContractAPI.CanceledContractInfo.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class CanceledContractController : ControllerBase
    {
        private ICanceledContractService _service;
        public CanceledContractController(ContractMakerContext db, ICanceledContractService service)
        {
            _service = service;
            _service.dataBase = db;
        }

        [HttpGet("getCanceledContract/{phoneNumber}")]
        public async Task<IActionResult> getCanceledContract(string phoneNumber)
        {
            ResponseCanceledContract response = await _service.getCanceledContract(phoneNumber);
            return MakeResponse(response, response.error_code);
        }

        [HttpPost("setCanceledContract")]
        public async Task<IActionResult> setCanceledContract([FromBody] CanceledContract info)
        {
            ResponseCanceledContract response = await _service.setCanceledContract(info);
            return MakeResponse(response, response.error_code);
        }

        [HttpDelete("deleteCanceledContract")]
        public async Task<IActionResult> deleteCanceledContract([FromBody] CanceledContract info)
        {
            ResponseCanceledContract response = await _service.deleteCanceledContract(info);
            return MakeResponse(response, response.error_code);
        }

        private IActionResult MakeResponse(object obj, int error_code)
        {
            switch (error_code)
            {
                case (int)HttpStatusCode.NotFound: return NotFound(obj);
                case (int)HttpStatusCode.BadRequest: return BadRequest(obj);
            }

            return Ok(obj);
        }
    }
}
