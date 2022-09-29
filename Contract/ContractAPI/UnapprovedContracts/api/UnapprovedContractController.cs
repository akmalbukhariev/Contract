using ContractAPI.Controllers.UnapprovedContracts.service;
using ContractAPI.DataAccess;
using ContractAPI.Models;
using ContractAPI.Response;
using ContractAPI.UnapprovedContracts.service.impl;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ContractAPI.UnapprovedContracts.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnapprovedContractController : ControllerBase
    {
        private IUnapprovedContractService _service;
        public UnapprovedContractController(ContractMakerContext db, IUnapprovedContractService service)
        {
            _service = service;
            _service.dataBase = db;
        }

        [HttpGet("getUnapprovedContract/{phoneNumber}")]
        public async Task<IActionResult> getUnapprovedContract(string phoneNumber)
        {
            ResponseUnapprovedContract response = await _service.getUnapprovedContract(phoneNumber);
            return MakeResponse(response, response.error_code);
        }

        [HttpPost("setUnapprovedContract")]
        public async Task<IActionResult> setUnapprovedContract([FromBody] UnapprovedContract info)
        {
            ResponseUnapprovedContract response = await _service.setUnapprovedContract(info);
            return MakeResponse(response, response.error_code);
        }

        [HttpDelete("deleteUnapprovedContract")]
        public async Task<IActionResult> deleteUnapprovedContract([FromBody] UnapprovedContract info)
        {
            ResponseUnapprovedContract response = await _service.deleteUnapprovedContract(info);
            return MakeResponse(response, response.error_code);
        }

        [HttpDelete("deleteUnapprovedContractAndSetCanceledContract")]
        public async Task<IActionResult> deleteUnapprovedContractAndSetCanceledContract([FromBody] CanceledContract info)
        {
            ResponseCanceledContract response = await _service.deleteUnapprovedContractAndSetCanceledContract(info);
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
