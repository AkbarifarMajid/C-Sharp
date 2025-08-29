using System;
using System.Collections.Generic;
using GolddiggerGmbh.DAL;
using GolddiggerGmbh.Model;

//using System.Reflection.Emit;

namespace GolddiggerGmbh.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository myRepository = new UserRepository();





        public User GetByUserAndPassword(string userName, string password)
        {
            return myRepository.GetByUserAndPassword(userName, password);
        }
    }
}
