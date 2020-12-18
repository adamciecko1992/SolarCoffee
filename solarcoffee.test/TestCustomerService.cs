using Microsoft.EntityFrameworkCore;
using solarcoffee.services.Customer;
using solarcoffee.data;
using solarcoffee.data.models;
using Xunit;
using FluentAssertions;
using System.Linq;
using System.Collections.Generic;
using Moq;

namespace solarcoffee.test
{
    public class TestCustomerService
    {
        [Fact]
        public void CustomerService_GetsAllCustomers_GivenTheyExist()
        {
            //make copy of service and db for test purposes
            var options = new DbContextOptionsBuilder<solarDbContext>()
        
               .UseInMemoryDatabase("gets_all").Options;
            using var context = new solarDbContext(options);
            //systemUnderTesting
            var sut = new CustomerService(context);
            //create customers
            sut.CreateCustomer(new data.models.Customer { Id = 123123 });
            sut.CreateCustomer(new data.models.Customer { Id = 1233 });
            sut.CreateCustomer(new data.models.Customer { Id = 123126 });

            var allCustomers = sut.GetAllCustomers();

            //fluent assertions gives us access to this syntax
            allCustomers.Count.Should().Be(3);
        }

        [Fact]
        public void CustomerService_CreatesCustomer_GivenNewCustomerObject()
        {
            var options = new DbContextOptionsBuilder<solarDbContext>()
                .UseInMemoryDatabase("Add_writes_to_database").Options;

            using var context = new solarDbContext(options);
            var sut = new CustomerService(context);

            sut.CreateCustomer(new Customer { Id = 18645 });
            context.Customers.Single().Id.Should().Be(18645);
        }

        [Fact]
        public void CustomerService_DeletesCustomer_GivenId()
        {
            var options = new DbContextOptionsBuilder<solarDbContext>()
                .UseInMemoryDatabase("deletes_one")
                .Options;

            using var context = new solarDbContext(options);
            var sut = new CustomerService(context);

            sut.CreateCustomer(new Customer { Id = 18645 });

            sut.DeleteCustomer(18645);
            var allCustomers = sut.GetAllCustomers();
            allCustomers.Count.Should().Be(0);
        }

        [Fact]
        public void CustomerService_OrdersByLastName_WhenGetAllCustomersInvoked()
        {
            // Arrange
            var data = new List<Customer> {
                new Customer { Id = 123, LastName = "Zulu"},
                new Customer { Id = 323, LastName = "Alpha"},
                new Customer { Id = -89, LastName = "Xu"}
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Customer>>();

            mockSet.As<IQueryable<Customer>>()
                .Setup(m => m.Provider)
                .Returns(data.Provider);

            mockSet.As<IQueryable<Customer>>()
                .Setup(m => m.Expression)
                .Returns(data.Expression);

            mockSet.As<IQueryable<Customer>>()
                .Setup(m => m.ElementType)
                .Returns(data.ElementType);

            mockSet.As<IQueryable<Customer>>()
                .Setup(m => m.GetEnumerator())
                .Returns(data.GetEnumerator());

            var mockContext = new Mock<solarDbContext>();

            mockContext.Setup(c => c.Customers)
                .Returns(mockSet.Object);

            // Act
            var sut = new CustomerService(mockContext.Object);
            var customers = sut.GetAllCustomers();

            // Assert
            customers.Count.Should().Be(3);
            customers[0].Id.Should().Be(323);
            customers[1].Id.Should().Be(-89);
            customers[2].Id.Should().Be(123);
        }
    }

}

