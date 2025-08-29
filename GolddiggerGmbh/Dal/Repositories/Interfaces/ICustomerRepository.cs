using System.Collections.Generic;
using GolddiggerGmbh.Model;


namespace GolddiggerGmbh.DAL
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Customer GetByEmail(string email);

    }
}
