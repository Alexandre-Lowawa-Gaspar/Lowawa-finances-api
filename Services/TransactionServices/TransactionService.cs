using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lowawa_finances_api.Services.TransactionServices
{
    public class TransactionService : ITransactionService
    {
        private static List<Transaction> transictions = new List<Transaction>();
        private readonly IMapper _mapper;

        public TransactionService(IMapper mapper)
        {
            _mapper = mapper;
        }
        public async Task<ServicesResponse<List<GetTransactionDto>>> AddTransiction(AddTransactionDto newTransiction)
        {
            var servicesResponse = new ServicesResponse<List<GetTransactionDto>>();

            var transiction = _mapper.Map<Transaction>(newTransiction);
            transiction.Id = transictions.Max(x => x.Id) + 1;
            transictions.Add(transiction);
            servicesResponse.Data = transictions.Select(x => _mapper.Map<GetTransactionDto>(x)).ToList();
            return servicesResponse;
        }

        public async Task<ServicesResponse<List<GetTransactionDto>>> DeleteTransiction(int id)
        {
            var servicesResponse = new ServicesResponse<List<GetTransactionDto>>();
            var transiction = transictions.First(x => x.Id == id);
            transictions.Remove(transiction);
            servicesResponse.Data = transictions.Select(c => _mapper.Map<GetTransactionDto>(c)).ToList();
            return servicesResponse;
        }

        public async Task<ServicesResponse<List<GetTransactionDto>>> GetAllTransiction()
        {
            var servicesResponse = new ServicesResponse<List<GetTransactionDto>>();
            servicesResponse.Data = transictions.Select(x => _mapper.Map<GetTransactionDto>(x)).ToList();
            return servicesResponse;
        }

        public async Task<ServicesResponse<GetTransactionDto>> GetTransictionById(int id)
        {
            var servicesResponse = new ServicesResponse<GetTransactionDto>();
            var transiction = transictions.FirstOrDefault(c => c.Id == id);
            servicesResponse.Data = _mapper.Map<GetTransactionDto>(transiction);
            return servicesResponse;
        }

        public async Task<ServicesResponse<GetTransactionDto>> GetTransictionByUser(int userId)
        {
            var servicesResponse = new ServicesResponse<GetTransactionDto>();
            var transiction = transictions.FirstOrDefault(x => x.UserId == userId);
            servicesResponse.Data = _mapper.Map<GetTransactionDto>(transiction);
            return servicesResponse;
        }

        public async Task<ServicesResponse<List<GetTransactionDto>>> UpdateTransiction(UpdateTransactionDto updateTransiction)
        {
            var servicesResponse = new ServicesResponse<List<GetTransactionDto>>();
            var transiction = transictions.FirstOrDefault(x => x.Id == updateTransiction.Id);
            if (transiction is null)
                throw new Exception($"Transition with Id{updateTransiction.Id} not found.");
            _mapper.Map(updateTransiction, transiction);
            servicesResponse.Data = transictions.Select(x => _mapper.Map<GetTransactionDto>(x)).ToList();
            return servicesResponse;
        }
    }
}