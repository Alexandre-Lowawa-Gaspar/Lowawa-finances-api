using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lowawa_finances_api.Services.TransactionServices
{
    public class TransactionService : ITransactionService
    {
        private static List<Transaction> transactions = new List<Transaction>();
        private readonly IMapper _mapper;

        public TransactionService(IMapper mapper)
        {
            _mapper = mapper;
        }
        public async Task<ServicesResponse<List<GetTransactionDto>>> AddTransaction(AddTransactionDto newtransaction)
        {
            var servicesResponse = new ServicesResponse<List<GetTransactionDto>>();

            var transaction = _mapper.Map<Transaction>(newtransaction);
            if (transaction.UserId <= 0)
            {
                servicesResponse.Success = false;
                servicesResponse.Message= new Exception("You need a User to make a transaction.").Message;
                return servicesResponse;
            }
            if (transactions.Count == 0)
                transaction.Id = 1;
            else
                transaction.Id = transactions.Max(x => x.Id) + 1;
            transactions.Add(transaction);
            servicesResponse.Data = transactions.Select(x => _mapper.Map<GetTransactionDto>(x)).ToList();
            return servicesResponse;
        }

        public async Task<ServicesResponse<List<GetTransactionDto>>> DeleteTransaction(int id)
        {
            var servicesResponse = new ServicesResponse<List<GetTransactionDto>>();
            var transaction = transactions.First(x => x.Id == id);
            transactions.Remove(transaction);
            servicesResponse.Data = transactions.Select(c => _mapper.Map<GetTransactionDto>(c)).ToList();
            return servicesResponse;
        }

        public async Task<ServicesResponse<List<GetTransactionDto>>> GetAllTransaction()
        {
            var servicesResponse = new ServicesResponse<List<GetTransactionDto>>();
            servicesResponse.Data = transactions.Select(x => _mapper.Map<GetTransactionDto>(x)).ToList();
            return servicesResponse;
        }


        public async Task<ServicesResponse<GetTransactionDto>> GetTransactionById(int id)
        {
            var servicesResponse = new ServicesResponse<GetTransactionDto>();
            var transaction = transactions.FirstOrDefault(c => c.Id == id);
            servicesResponse.Data = _mapper.Map<GetTransactionDto>(transaction);
            return servicesResponse;
        }

        public async Task<ServicesResponse<GetTransactionDto>> GetTransactionByUserId(int userId)
        {
            var servicesResponse = new ServicesResponse<GetTransactionDto>();
            var transaction = transactions.FirstOrDefault(x => x.UserId == userId);
            servicesResponse.Data = _mapper.Map<GetTransactionDto>(transaction);
            return servicesResponse;
        }

        public async Task<ServicesResponse<List<GetTransactionDto>>> UpdateTransaction(UpdateTransactionDto updatetransaction)
        {
            var servicesResponse = new ServicesResponse<List<GetTransactionDto>>();
            var transaction = transactions.FirstOrDefault(x => x.Id == updatetransaction.Id);
            if (transaction is null)
                throw new Exception($"Transation with Id{updatetransaction.Id} not found.");
            _mapper.Map(updatetransaction, transaction);
            servicesResponse.Data = transactions.Select(x => _mapper.Map<GetTransactionDto>(x)).ToList();
            return servicesResponse;
        }
    }
}