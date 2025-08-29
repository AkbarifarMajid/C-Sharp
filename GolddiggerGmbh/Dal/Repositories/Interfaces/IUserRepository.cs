using GolddiggerGmbh.Model;

namespace GolddiggerGmbh.DAL
{
    public interface IUserRepository
    {
        User GetByUserAndPassword(string userName, string password);
    }
}