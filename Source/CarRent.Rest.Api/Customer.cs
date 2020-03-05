using System;
using System.Collections.Generic;
using System.Linq;

namespace CarRent.Rest.Api
{
    public struct Address
    {
        public string Name;
        public string Street;
        public int PostCode;
        public string TownName;
        public string Country;
    }
    
    public interface ICustomer
    {
        string Name { get; set; }
        Address Address { get; set; }
        string CustomerNumber { get; set; }
    }

    public class Customer : ICustomer
    {
        public string Name { get; set; }
        public Address Address { get; set; }
        public string CustomerNumber { get; set; }
    }
    
    public static class CustomerController
    {
        public static void AddCustomer(ICustomer customer)
        {
            AddCustomers(new[] {customer});
        }

        public static void AddCustomers(IEnumerable<ICustomer> customers)
        {
            throw new NotImplementedException();
        }
        
        public static ICustomer GetCustomer(string query)
        {
            var customers = GetCustomers(query);
            return customers?.First();
        }

        public static IEnumerable<ICustomer> GetCustomers(string query)
        {
            throw new NotImplementedException();
        }

        public static void UpdateCustomers(string query)
        {
            throw new NotImplementedException();
        }

        public static void RemoveCustomer(ICustomer customer)
        {
            RemoveCustomers(new[] {customer});
        }

        public static void RemoveCustomers(IEnumerable<ICustomer> customers)
        {
            throw new NotImplementedException();
        }
    }
}