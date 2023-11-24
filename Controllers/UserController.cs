using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lowawa_finances_api.Services.UserServices;
using Microsoft.AspNetCore.Mvc;

namespace Lowawa_finances_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost]
        public async Task<ActionResult<ServicesResponse<List<GetUserDto>>>> Add(AddUserDto newUser)
        {
            return Ok(await _userService.AddUser(newUser));
        }
        [HttpGet("GetAll")]
        public async Task<ActionResult<ServicesResponse<List<GetUserDto>>>> GetAll()
        {
            return Ok(await _userService.GetAllUser());
        }
        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<ServicesResponse<GetUserDto>>> GetById(int id)
        {
            var response = await _userService.GetUserById(id);
            if (response is null)
                return NotFound(response);
            return Ok(response);
        }
        [HttpPut]
        public async Task<ActionResult<ServicesResponse<List<GetUserDto>>>> Update(UpdateUserDto updateUser)
        {
            var response = await _userService.UpdateUser(updateUser);
            if (response is null)
                return NotFound(response);
            return Ok(response);
        }
        [HttpDelete]
        public async Task<ActionResult<ServicesResponse<List<GetUserDto>>>> Delete(int id)
        {
            var response = await _userService.DeleteUser(id);
            if(response is null)
            return NotFound(response);
            return Ok(response);
        }
    }
}