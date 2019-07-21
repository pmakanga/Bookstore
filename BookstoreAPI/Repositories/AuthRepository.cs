using System.Threading.Tasks;
using BookstoreAPI.Models;
using BookstoreAPI.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace BookstoreAPI.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly BookStoreContext _context;

        
        public AuthRepository(BookStoreContext context)
        {
            _context = context;
        }
        public async Task<TblUser> Login(string email, string password)
        {
            var user = await _context.TblUser.FirstOrDefaultAsync(x => x.Email == email);
                if(user == null)
                    return null;
                
                if(!VerifyPasswordHash(password, user.Password, user.Salt))
                    return null;
                
                return user; //auth succesful
            
        }

        public async Task<TblUser> Register(TblUser user, string password)
        {
            byte[] passwordHash, salt;
            CreatePasswordHash(password, out passwordHash, out salt);
            user.Password = passwordHash;
            user.Salt = salt;

            await _context.TblUser.AddAsync(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<bool> UserExists(string username)
        {
            if(await _context.TblUser.AnyAsync(x => x.Email == username))
                return true;
            return false;
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] salt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(salt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i]) return false;
                }
            }
            return true;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] salt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                salt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}