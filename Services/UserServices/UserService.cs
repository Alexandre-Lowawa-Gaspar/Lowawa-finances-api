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

        public UserService(IMapper mapper)
        {
            _mapper = mapper;
        }
        public async Task<ServicesResponse<List<GetUserDto>>> AddUser(AddUserDto newUser)
        {
            var servicesResponse = new ServicesResponse<List<GetUserDto>>();
            var user = _mapper.Map<User>(newUser);
            if(users.Count==0)
            user.Id = 1;
            else
            user.Id=users.Max(x => x.Id)+1;
            users.Add(user);
            servicesResponse.Data = users.Select(c => _mapper.Map<GetUserDto>(c)).ToList();
            return servicesResponse;
        }

        public async Task<ServicesResponse<List<GetUserDto>>> DeleteUser(int id)
        {
            var servicesResponse = new ServicesResponse<List<GetUserDto>>();
            var user = users.First(c => c.Id == id);
            users.Remove(user);
            servicesResponse.Data = users.Select(c => _mapper.Map<GetUserDto>(c)).ToList();
            return servicesResponse;
        }

        public async Task<ServicesResponse<List<GetUserDto>>> GetAllUser()
        {
            var servicesResponse = new ServicesResponse<List<GetUserDto>>();

            try
            {
            servicesResponse.Data = users.Select(c => _mapper.Map<GetUserDto>(c)).ToList();
            }catch(Exception ex){
                servicesResponse.Success = false;
                servicesResponse.Message = ex.Message.Replace("'", "");
            }
            return servicesResponse;
        }

        public async Task<ServicesResponse<GetUserDto>> GetUserById(int id)
        {
            var servicesResponse = new ServicesResponse<GetUserDto>();
            var user = users.FirstOrDefault(c => c.Id == id);
            servicesResponse.Data = _mapper.Map<GetUserDto>(user);
            return servicesResponse;
        }

        public async Task<ServicesResponse<List<GetUserDto>>> UpdateUser(UpdateUserDto updateUser)
        {
            var servicesResponse = new ServicesResponse<List<GetUserDto>>();
            var user = users.FirstOrDefault(x => x.Id == updateUser.Id);
            if (user is null)
                throw new Exception($"User with Id {updateUser.Id} not found.");
            _mapper.Map(updateUser, user);
            servicesResponse.Data = users.Select(c => _mapper.Map<GetUserDto>(user)).ToList();
            return servicesResponse;
        }
    }
}