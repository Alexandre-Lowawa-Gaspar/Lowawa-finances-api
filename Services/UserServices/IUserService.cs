using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lowawa_finances_api.Services.UserServices
{
    public interface IUserService
    {
        Task<ServicesResponse<List<GetUserDto>>> AddUser(AddUserDto newUser);
        Task<ServicesResponse<List<GetUserDto>>> GetAllUser();
        Task<ServicesResponse<GetUserDto>> GetUserById(int id);
        Task<ServicesResponse<List<GetUserDto>>> UpdateUser(UpdateUserDto updateUser);
        Task<ServicesResponse<List<GetUserDto>>> DeleteUser(int id);
    }
}