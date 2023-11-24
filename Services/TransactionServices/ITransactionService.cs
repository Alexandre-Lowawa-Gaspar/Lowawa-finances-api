using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lowawa_finances_api.Services.TransactionServices
{
    public interface ITransactionService
    {
        Task<ServicesResponse<List<GetTransactionDto>>> AddTransaction(AddTransactionDto newTransaction);
        Task<ServicesResponse<List<GetTransactionDto>>> GetAllTransaction();
        Task<ServicesResponse<GetTransactionDto>> GetTransactionById(int id);
        Task<ServicesResponse<GetTransactionDto>> GetTransactionByUserId(int userId);
        Task<ServicesResponse<List<GetTransactionDto>>> UpdateTransaction(UpdateTransactionDto updateTransaction);
        Task<ServicesResponse<List<GetTransactionDto>>> DeleteTransaction(int id);

    }
}