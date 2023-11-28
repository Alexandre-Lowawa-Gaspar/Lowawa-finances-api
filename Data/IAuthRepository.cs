using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lowawa_finances_api.Data
{
    public interface IAuthRepository
    {
       Task<ServicesResponse<int>> Register(User user, string password);
       Task<ServicesResponse<string>> Login(string userName, string password);
       Task<bool> UserExist(string userName);
    }
}