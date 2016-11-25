using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using TeamCity.Code;

namespace TeamCity.Tests
{

    [TestFixture]
    public class CustomerServiceTestsNew
    {
        [Test]
        public void the_repository_save_should_be_called_but_this_is_new()
        {
            //Arrange
            var mockRepository = new Mock<ICustomerRepository>();

            mockRepository.Setup(x => x.Save(It.IsAny<Customer>()));

            var customerService = new CustomerService(mockRepository.Object);

            //Act
            customerService.Create(new CustomerToCreateDto());

            //Assert
            mockRepository.VerifyAll();
        }

        [Test]
        public void the_customer_repository_should_be_called_once_per_customer_but_this_is_new()
        {
            //Arrange
            var listOfCustomerDtos = new List<CustomerToCreateDto>
            {
                new CustomerToCreateDto
                {
                    Name = "Sam",
                    City = "Sampson"
                },
                new CustomerToCreateDto
                {
                    Name = "Bob",
                    City = "Builder"
                },
                new CustomerToCreateDto
                {
                    Name = "Doug",
                    City = "Digger"
                }
            };

            var mockCustomerRepository = new Mock<ICustomerRepository>();
            mockCustomerRepository.Setup(x => x.Save(It.IsAny<Customer>()));
            var customerService = new CustomerService(mockCustomerRepository.Object);
            //Act
            customerService.Create(listOfCustomerDtos);

            //Assert
            mockCustomerRepository.Verify();
        }

        [Test]
        public void this_fail()
        {
           

            Assert.AreEqual("Diego","Gonzalo");
        }

    }
}
