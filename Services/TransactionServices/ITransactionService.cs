using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lowawa_finances_api.Services.TransactionServices
{
    public interface ITransactionService
    {
        Task<ServicesResponse<List<GetTransactionDto>>> AddTransiction(AddTransactionDto newTransiction);
        Task<ServicesResponse<List<GetTransactionDto>>> GetAllTransiction();
        Task<ServicesResponse<GetTransactionDto>> GetTransictionById(int id);
        Task<ServicesResponse<GetTransactionDto>> GetTransictionByUser(int userId);
        Task<ServicesResponse<List<GetTransactionDto>>> UpdateTransiction(UpdateTransactionDto updateTransiction);
       Task<ServicesResponse<List<GetTransactionDto>>> DeleteTransiction(int id);


    }
}