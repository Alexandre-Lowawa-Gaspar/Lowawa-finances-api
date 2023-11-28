using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Lowawa_finances_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;
        public AuthController(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }
        [HttpPost("Register")]
        public async Task<ActionResult<ServicesResponse<int>>> Register(UserRegisterDto userRegister)
        {
            var response = await _authRepository.Register(new User { UserName = userRegister.UserName }, userRegister.Password);
            if (response.Success)
                return Ok(response);
            return BadRequest(response);
        }
        [HttpPost("Login")]
        public async Task<ActionResult<ServicesResponse<int>>> Login(UserLoginDto userLogin)
        {
            var response = await _authRepository.Login(userLogin.UserName, userLogin.Password);
            if (response.Success)
                return Ok(response);
            return BadRequest(response);
        }
    }
}