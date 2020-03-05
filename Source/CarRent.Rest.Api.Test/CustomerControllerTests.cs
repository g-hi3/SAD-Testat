using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace CarRent.Rest.Api.Test
{
    [TestFixture]
    public class CustomerControllerTests
    {
        [Test]
        public void AddCustomer_Default_NoException()
        {
            // Arrange
            var customer = new Customer();
            
            // Act
            CustomerController.AddCustomer(customer);

            // Assert
        }
        
        [Test]
        public void AddCustomer_NullArgument_NoException()
        {
            // Arrange
            
            // Act
            static void TestDelegate()
                => CustomerController.AddCustomer(null);

            // Assert
            Assert.That(TestDelegate, Throws.TypeOf<Exception>());
        }
        
        [Test]
        public void AddCustomers_Default_NoException()
        {
            // Arrange
            var customers = new[]
            {
                new Customer(),
                new Customer(),
                new Customer()
            };
            
            // Act
            CustomerController.AddCustomers(customers);

            // Assert

        }
        
        [Test]
        public void AddCustomers_NullArgument_NoException()
        {
            // Arrange
            
            // Act
            CustomerController.AddCustomers(null);

            // Assert

        }
        
        [Test]
        public void AddCustomers_EmptyEnumerable_NoException()
        {
            // Arrange
            var customers = new ICustomer[0];
            
            // Act
            CustomerController.AddCustomers(customers);

            // Assert

        }

        [Test]
        public void GetCustomer_Default_ReturnsCustomer()
        {
            // Arrange
            const string customerNumber = "123456";
            var query = $"SELECT * FROM Customer WHERE CustomerNumber = '{customerNumber}'";
            
            // Act
            var customer = CustomerController.GetCustomer(query);

            // Assert
            Assert.That(customer, Is.InstanceOf<ICustomer>());
            Assert.That(customer.CustomerNumber, Is.EqualTo(customerNumber));
        }

        [Test]
        public void GetCustomer_NullArgument_ReturnsCustomer()
        {
            // Arrange
            
            // Act
            static ICustomer TestDelegate()
                => CustomerController.GetCustomer(null);

            // Assert
            Assert.That(TestDelegate, Throws.TypeOf<Exception>());
        }

        [Test]
        public void GetCustomers_Default_ReturnsCustomer()
        {
            // Arrange
            const string customerNumber = "123456";
            var query = $"SELECT * FROM Customer WHERE CustomerNumber = '{customerNumber}'";
            
            // Act
            var customer = CustomerController.GetCustomers(query);

            // Assert
            Assert.That(customer, Is.InstanceOf<IEnumerable<ICustomer>>());
        }

        [Test]
        public void GetCustomers_NullArgument_ReturnsCustomer()
        {
            // Arrange
            
            // Act
            static IEnumerable<ICustomer> TestDelegate()
                => CustomerController.GetCustomers(null);

            // Assert
            Assert.That(TestDelegate, Throws.TypeOf<Exception>());
        }

        [Test]
        public void UpdateCustomers_Default_NoException()
        {
            // Arrange
            const string updatedCustomerNumber = "999999";
            var query =
                $"UPDATE Customer WHERE CustomerNumber = '888888' SET CustomerNumber = '{updatedCustomerNumber}'";
            
            // Act
            CustomerController.UpdateCustomers(query);
            
            // Assert
            
        }

        [Test]
        public void UpdateCustomers_NullArgument_ReturnsCustomer()
        {
            // Arrange
            
            // Act
            static void TestDelegate()
                => CustomerController.UpdateCustomers(null);

            // Assert
            Assert.That(TestDelegate, Throws.TypeOf<Exception>());
        }
        [Test]
        public void RemoveCustomer_Default_NoException()
        {
            // Arrange
            var customer = new Customer();
            
            // Act
            CustomerController.RemoveCustomer(customer);

            // Assert
            
        }
        
        [Test]
        public void RemoveCustomer_NullArgument_NoException()
        {
            // Arrange
            
            // Act
            static void TestDelegate()
                => CustomerController.RemoveCustomer(null);

            // Assert
            Assert.That(TestDelegate, Throws.TypeOf<Exception>());
        }
    }
}