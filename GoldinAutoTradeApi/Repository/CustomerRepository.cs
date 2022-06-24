using GlodinAutoTradeModel.Models;
using GoldinAutoTradeApi.Inteface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GoldinAutoTradeApi.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        Customer ICustomerRepository.AddEditCustomer(Customer customer)
        {
            try
            {
                using (var context = new EF.GoldinAutoEntities())
                {
                    var existingCustomer = context.Customers.Where(a => a.Email == customer.Email).FirstOrDefault();
                    if (existingCustomer == null)
                    {

                        var cust = new EF.Customer
                        {
                            Address1 = customer.Address1,
                            Address2 = customer.Address2,
                            Address3 = customer.Address3,
                            Address4 = customer.Address4,
                            CardNumber = customer.CardNumber,
                            CardType = customer.CardType,
                            Email = customer.Email,
                            ExpiryDate = customer.ExpiryDate,
                            FirstName = customer.FirstName,
                            LastName = customer.LastName,
                            Telephone = customer.Telephone
                        };
                        context.Customers.Add(cust);
                        context.SaveChanges();
                        return customer;
                    }
                    else
                    {
                        existingCustomer.Address4 = customer.Address4;
                        existingCustomer.CardNumber = customer.CardNumber;
                        existingCustomer.CardType = customer.CardType;
                        existingCustomer.Email = customer.Email;
                        existingCustomer.ExpiryDate = customer.ExpiryDate;
                        existingCustomer.FirstName = customer.FirstName;
                        existingCustomer.LastName = customer.LastName;
                        existingCustomer.Telephone = customer.Telephone;
                        existingCustomer.Address1 = customer.Address1;
                        existingCustomer.Address2 = customer.Address2;
                        existingCustomer.Address3 = customer.Address3;
                        context.SaveChanges();
                        return customer;
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        Customer ICustomerRepository.GetCustomer(string email)
        {
            using (var context = new EF.GoldinAutoEntities())
            {
                return (from a in context.Customers
                        where a.Email == email
                        select new Customer
                        {
                            Email = a.Email,
                            FirstName = a.FirstName,
                            Address1 = a.Address1,
                            Address2 = a.Address2,
                            Address3 = a.Address3,
                            Address4 = a.Address4,
                            CardNumber = a.CardNumber,
                            CardType = a.CardType,
                            ExpiryDate = (DateTime)a.ExpiryDate,
                            LastName = a.LastName,
                            Telephone = a.Telephone,
                            CID = a.CID

                        }).FirstOrDefault();
            }
        }
    }
}