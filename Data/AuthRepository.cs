using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Lowawa_finances_api.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _context;
        public AuthRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<ServicesResponse<string>> Login(string userName, string password)
        {
            var response = new ServicesResponse<string>();
            var user = await _context.Users
            .FirstOrDefaultAsync(x => x.UserName.ToLower().Equals(userName.ToLower()));
            if (user is null)
            {
                response.Success = false;
                response.Message = "User not found";
            }
            else if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            {
                response.Success = false;
                response.Message = "wrong password";
            }
            else
            {
                response.Data = user.Id.ToString();
            }
            return response;
        }

        public async Task<ServicesResponse<int>> Register(User user, string password)
        {
            var response = new ServicesResponse<int>();
            if (await UserExist(user.UserName))
            {
                response.Success = false;
                response.Message = "The user is already exists.";
                return response;
            }
            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            response.Data = user.Id;
            return response;
        }

        public async Task<bool> UserExist(string userName)
        {
           if(await _context.Users.AnyAsync(x=>x.UserName.ToLower()==userName.ToLower()))
           return true;
           return false;
        }
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }
        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return computeHash.SequenceEqual(passwordHash);
            }
        }
    }
}