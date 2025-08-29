using System;
using System.Collections.Generic;
using GolddiggerGmbh.DAL;
using GolddiggerGmbh.Model;



namespace GolddiggerGmbh.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository myRepository = new CustomerRepository();

        public void Add(Customer customer)
        {
            try
            {

                myRepository.Add(customer);
                Logger.LogInfo($"Employee added: {customer.FirstName} {customer.LastName}");
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
               
                throw;
            }
        }




        public void Update(Customer customer)
        {
            try
            {

                myRepository.Update(customer);
                Logger.LogInfo($"Employee updated: {customer.Id}");
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                throw;
            }
        }

        public void Delete(int id)
        {
            try
            {
                myRepository.Delete(id);
                Logger.LogInfo($"Customer deleted: {id}");
            }
            catch (Exception ex)
            {
               
                Logger.LogException(ex);
                throw;
            }
        }



        public Customer GetById(int id)
        {
            return myRepository.GetById(id);
        }

        public Customer GetByEmail(string email)
        {
            if (!ValidationHelper.IsValidEmail(email))
                throw new ArgumentException("Invalid Email");

            return myRepository.GetByEmail(email);
        }


        public List<Customer> GetAll()
        {
            return myRepository.GetAll();
        }
    }
}
