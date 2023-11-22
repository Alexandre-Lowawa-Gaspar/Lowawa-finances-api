using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lowawa_finances_api.Dto.UserDto
{
    public class AddUserDto
    {
        public int Id { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }

    }
}