using System.Collections.Generic;
using GolddiggerGmbh.Model;


namespace GolddiggerGmbh.Services
{
    public interface IUserService
    {
        User GetByUserAndPassword(string userName, string password);
    }
}

