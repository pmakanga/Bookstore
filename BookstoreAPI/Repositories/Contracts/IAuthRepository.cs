using System.Threading.Tasks;
using BookstoreAPI.Models;

namespace BookstoreAPI.Repositories.Contracts
{
    public interface IAuthRepository
    {
        Task<TblUser> Register(TblUser user, string password);
        Task<TblUser> Login(string email, string password);
        Task<bool> UserExists(string username);
    }
}