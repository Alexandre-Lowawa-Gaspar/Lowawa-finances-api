using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lowawa_finances_api.Services.TransactionServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Lowawa_finances_api.Controller
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }
        [HttpGet("GetAll")]
        public async Task<ActionResult<ServicesResponse<List<GetTransactionDto>>>> GetAll()
        {
            return Ok(await _transactionService.GetAllTransaction());
        }
        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<ServicesResponse<GetTransactionDto>>> GetById(int id)
        {
             var response =await _transactionService.GetTransactionById(id);
             if (response is null)
                return NotFound(response);
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<ServicesResponse<List<GetTransactionDto>>>> Add(AddTransactionDto newTransation)
        {
            return Ok(await _transactionService.AddTransaction(newTransation));
        }
        [HttpPut]
        public async Task<ActionResult<ServicesResponse<List<GetTransactionDto>>>> Update(UpdateTransactionDto upadteTransation)
        {
            var response = await _transactionService.UpdateTransaction(upadteTransation);
                  if (response is null)
                return NotFound(response);
            return Ok(response);
        }
        [HttpDelete]
        public async Task<ActionResult<ServicesResponse<List<GetTransactionDto>>>> Delete(int id)
        {
            var response = await _transactionService.DeleteTransaction(id);
                  if (response is null)
                return NotFound(response);
            return Ok(response);
        }

    }
}