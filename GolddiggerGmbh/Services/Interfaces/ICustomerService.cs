using System.Collections.Generic;
using GolddiggerGmbh.Model;

namespace GolddiggerGmbh.Services
{
    public interface ICustomerService : IBaseService<Customer>
    {
        Customer GetByEmail(string email);
        
    }
}