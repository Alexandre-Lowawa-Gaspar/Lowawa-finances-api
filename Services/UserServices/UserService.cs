using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lowawa_finances_api.Services.UserServices
{
    public class UserService : IUserService
    {
        private static List<User> users = new List<User>();
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        public UserService(IMapper mapper, DataContext context)
        {
            _mapper = mapper;
            _context = context;

        }
        public async Task<ServicesResponse<List<GetUserDto>>> AddUser(AddUserDto newUser)
        {
            var servicesResponse = new ServicesResponse<List<GetUserDto>>();
            try
            {
                var user = _mapper.Map<User>(newUser);
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                servicesResponse.Data = await _context.Users.Select(c => _mapper.Map<GetUserDto>(c)).ToListAsync();
            }
            catch (Exception ex)
            {
                servicesResponse.Success = false;
                servicesResponse.Message = ex.Message.Replace("'", "");
            }
            return servicesResponse;
        }

        public async Task<ServicesResponse<List<GetUserDto>>> DeleteUser(int id)
        {
            var servicesResponse = new ServicesResponse<List<GetUserDto>>();
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(c => c.Id == id);
                if (user is null)
                {
                    servicesResponse.Success = false;
                    servicesResponse.Message = new Exception($"The user with Id {id} was not found.").Message.Replace("'", "");
                    return servicesResponse;
                }
                users.Remove(user);
                await _context.SaveChangesAsync();
                servicesResponse.Data = users.Select(c => _mapper.Map<GetUserDto>(c)).ToList();
            }
            catch (Exception ex)
            {
                servicesResponse.Success = false;
                servicesResponse.Message = ex.Message.Replace("'", "");
            }
            return servicesResponse;
        }

        public async Task<ServicesResponse<List<GetUserDto>>> GetAllUser()
        {
            var servicesResponse = new ServicesResponse<List<GetUserDto>>();

            try
            {
                servicesResponse.Data = await _context.Users.Select(c => _mapper.Map<GetUserDto>(c)).ToListAsync();
            }
            catch (Exception ex)
            {
                servicesResponse.Success = false;
                servicesResponse.Message = ex.Message.Replace("'", "");
            }
            return servicesResponse;
        }

        public async Task<ServicesResponse<GetUserDto>> GetUserById(int id)
        {
            var servicesResponse = new ServicesResponse<GetUserDto>();
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(c => c.Id == id);
                servicesResponse.Data = _mapper.Map<GetUserDto>(user);
            }
            catch (Exception ex)
            {
                servicesResponse.Success = false;
                servicesResponse.Message = ex.Message.Replace("'", "");
            }
            return servicesResponse;
        }

        public async Task<ServicesResponse<List<GetUserDto>>> UpdateUser(UpdateUserDto updateUser)
        {
            var servicesResponse = new ServicesResponse<List<GetUserDto>>();
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == updateUser.Id);
                if (user is null)
                    throw new Exception($"User with Id {updateUser.Id} not found.");
                _mapper.Map(updateUser, user);
                await _context.SaveChangesAsync();
                servicesResponse.Data = await _context.Users.Select(c => _mapper.Map<GetUserDto>(user)).ToListAsync();
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