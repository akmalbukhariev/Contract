using ContractAPI.ApplicableContracts.service;
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

namespace ContractAPI.ApplicableContracts.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicableContractController : ControllerBase
    {
        private IApplicableContractService _service;
        public ApplicableContractController(ContractMakerContext db, IApplicableContractService service)
        {
            _service = service;
            _service.dataBase = db;
        }

        [HttpGet("getApplicableContract/{phoneNumber}")]
        public async Task<IActionResult> getApplicableContract(string phoneNumber)
        {
            ResponseApplicableContract response = await _service.getApplicableContract(phoneNumber);
            return MakeResponse(response, response.error_code);
        }

        [HttpPost("setApplicableContract")]
        public async Task<IActionResult> setApplicableContract([FromBody] ApplicableContract info)
        {
            ResponseApplicableContract response = await _service.setApplicableContract(info);
            return MakeResponse(response, response.error_code);
        }

        [HttpDelete("deleteApplicableContract")]
        public async Task<IActionResult> deleteApplicableContract([FromBody] ApplicableContract info)
        {
            ResponseApplicableContract response = await _service.deleteApplicableContract(info);
            return MakeResponse(response, response.error_code);
        }

        [HttpDelete("deleteApplicableContractAndSetCanceledContract")]
        public async Task<IActionResult> deleteApplicableContractAndSetCanceledContract([FromBody] CanceledContract info)
        {
            ResponseCanceledContract response = await _service.deleteApplicableContractAndSetCanceledContract(info);
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
