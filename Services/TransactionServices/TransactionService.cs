using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Lowawa_finances_api.Services.TransactionServices
{
    public class TransactionService : ITransactionService
    {

        private readonly IMapper _mapper;
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TransactionService(IMapper mapper, DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            _context = context;
        }
        private int GetUserId() => int.Parse(_httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        public async Task<ServicesResponse<List<GetTransactionDto>>> AddTransaction(AddTransactionDto newtransaction)
        {
            var servicesResponse = new ServicesResponse<List<GetTransactionDto>>();
            try
            {

                var transaction = _mapper.Map<Transaction>(newtransaction);
                transaction.User = await _context.Users.FirstOrDefaultAsync(c => c.Id == GetUserId());
                _context.Transactions.Add(transaction);
                await _context.SaveChangesAsync();
                servicesResponse.Data = await _context.Transactions
                .Include(c => c.User)
                .Where(c => c.User!.Id == GetUserId())
                .Select(x => _mapper.Map<GetTransactionDto>(x)).ToListAsync();
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
                var transaction = await _context.Transactions.FirstOrDefaultAsync(x => x.Id == id && x.User!.Id == GetUserId());
                if (transaction is null)
                {
                    servicesResponse.Success = false;
                    servicesResponse.Message = new Exception($"The transaction with Id { id } was not found.").Message.Replace("'", "");
                    return servicesResponse;
                }
                _context.Transactions.Remove(transaction);
                await _context.SaveChangesAsync();
                servicesResponse.Data = await _context.Transactions
                .Include(c => c.User)
                .Where(c => c.User!.Id == GetUserId())
                .Select(x => _mapper.Map<GetTransactionDto>(x)).ToListAsync();
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
                servicesResponse.Data = await _context.Transactions
                .Include(c => c.User)
                .Where(t => t.User!.Id == GetUserId())
                .Select(x => _mapper.Map<GetTransactionDto>(x)).ToListAsync();
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
                var transaction = await _context.Transactions.FirstOrDefaultAsync(c => c.Id == id && c.User!.Id == GetUserId());
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
                var transaction = await _context.Transactions.FirstOrDefaultAsync(x => x.Id == updatetransaction.Id && x.User!.Id == GetUserId());
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