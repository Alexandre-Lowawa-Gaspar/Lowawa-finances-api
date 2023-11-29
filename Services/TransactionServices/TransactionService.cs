using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lowawa_finances_api.Services.TransactionServices
{
    public class TransactionService : ITransactionService
    {

        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public TransactionService(IMapper mapper, DataContext context)
        {
            _mapper = mapper;
            _context = context;
        }
        public async Task<ServicesResponse<List<GetTransactionDto>>> AddTransaction(AddTransactionDto newtransaction)
        {
            var servicesResponse = new ServicesResponse<List<GetTransactionDto>>();
            try
            {

                var transaction = _mapper.Map<Transaction>(newtransaction);
                if (transaction.User.Id <= 0)
                {
                    servicesResponse.Success = false;
                    servicesResponse.Message = new Exception("You need a User to make a transaction.").Message;
                    return servicesResponse;
                }

                _context.Transactions.Add(transaction);
                await _context.SaveChangesAsync();
                servicesResponse.Data = await _context.Transactions.Select(x => _mapper.Map<GetTransactionDto>(x)).ToListAsync();
            }
            catch (Exception ex)
            {
                servicesResponse.Success = false;
                servicesResponse.Message = ex.Message.Replace("'", "");
            }
            return servicesResponse;
        }

        public async Task<ServicesResponse<List<GetTransactionDto>>> DeleteTransaction(int id)
        {
            var servicesResponse = new ServicesResponse<List<GetTransactionDto>>();
            try
            {
                var transaction = await _context.Transactions.FirstOrDefaultAsync(x => x.Id == id);
                if(transaction is null){
                    servicesResponse.Success=false;
                    servicesResponse.Message = new Exception($"The transaction with Id {id} was not found.").Message.Replace("'","");
                    return servicesResponse;
                }
                _context.Transactions.Remove(transaction);
                await _context.SaveChangesAsync();
                servicesResponse.Data = await _context.Transactions.Select(c => _mapper.Map<GetTransactionDto>(c)).ToListAsync();
            }
            catch (Exception ex)
            {
                servicesResponse.Success = false;
                servicesResponse.Message = ex.Message.Replace("'", "");
            }
            return servicesResponse;
        }

        public async Task<ServicesResponse<List<GetTransactionDto>>> GetAllTransaction()
        {
            var servicesResponse = new ServicesResponse<List<GetTransactionDto>>();
            try
            {
                servicesResponse.Data = await _context.Transactions.Select(x => _mapper.Map<GetTransactionDto>(x)).ToListAsync();
            }
            catch (Exception ex)
            {
                servicesResponse.Success = false;
                servicesResponse.Message = ex.Message.Replace("'", "");
            }
            return servicesResponse;
        }


        public async Task<ServicesResponse<GetTransactionDto>> GetTransactionById(int id)
        {
            var servicesResponse = new ServicesResponse<GetTransactionDto>();
            try
            {
                var transaction = await _context.Transactions.FirstOrDefaultAsync(c => c.Id == id);
                servicesResponse.Data = _mapper.Map<GetTransactionDto>(transaction);
            }
            catch (Exception ex)
            {
                servicesResponse.Success = false;
                servicesResponse.Message = ex.Message.Replace("'", "");
            }
            return servicesResponse;
        }

        public async Task<ServicesResponse<GetTransactionDto>> GetTransactionByUserId(int userId)
        {
            var servicesResponse = new ServicesResponse<GetTransactionDto>();
            try
            {
                var transaction = await _context.Transactions.FirstOrDefaultAsync(x => x.User.Id == userId);
                servicesResponse.Data = _mapper.Map<GetTransactionDto>(transaction);
            }
            catch (Exception ex)
            {
                servicesResponse.Success = false;
                servicesResponse.Message = ex.Message.Replace("'", "");
            }
            return servicesResponse;
        }

        public async Task<ServicesResponse<List<GetTransactionDto>>> UpdateTransaction(UpdateTransactionDto updatetransaction)
        {
            var servicesResponse = new ServicesResponse<List<GetTransactionDto>>();
            try
            {
                var transaction = await _context.Transactions.FirstOrDefaultAsync(x => x.Id == updatetransaction.Id);
                if (transaction is null)
                    throw new Exception($"Transation with Id{updatetransaction.Id} not found.");
                _mapper.Map(updatetransaction, transaction);
                await _context.SaveChangesAsync();
                servicesResponse.Data = await _context.Transactions.Select(x => _mapper.Map<GetTransactionDto>(x)).ToListAsync();
            }
            catch (Exception ex)
            {
                servicesResponse.Success = false;
                servicesResponse.Message = ex.Message.Replace("'", "");
            }
            return servicesResponse;
        }
    }
}