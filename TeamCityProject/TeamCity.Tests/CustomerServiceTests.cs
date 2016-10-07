using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NUnit.Framework;
using PluralSight.Moq.Code;
using System.Collections.Generic;

namespace PluralSight.Moq.Tests
{
    [TestFixture]
    public class CustomerServiceTests
    {
        [Test]
        public void the_repository_save_should_be_called()
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
        public void the_customer_repository_should_be_called_once_per_customer()
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
        public void the_customer_repository_should_be_called_once_per_customer_2()
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
            var customerService = new CustomerService(mockCustomerRepository.Object);
            //Act
            customerService.Create(listOfCustomerDtos);

            //Assert
            mockCustomerRepository.Verify(x => x.Save(It.IsAny<Customer>()), Times.Exactly(listOfCustomerDtos.Count));
        }

        [Test]
        [ExpectedException(typeof(InvalidCustomerMailingAddressException))]
        public void an_exception_should_be_thrown_if_the_addres_is_not_created()
        {
            //Arrange
            var customerToCreateDto = new CustomerToCreateDto()
            {
                Name = "Bob ",
                City = "Builder"
            };
            var mockAddressBuilder = new Mock<ICustomerAddressBuilder>();
            var mockCustomerRepository = new Mock<ICustomerRepository>();

            mockAddressBuilder
                .Setup(x => x.From(It.IsAny<CustomerToCreateDto>()))
                .Returns(() => null);

            var customerService = new CustomerService(
                mockCustomerRepository.Object, mockAddressBuilder.Object);

            //Act
            customerService.Create(customerToCreateDto);

            //Assert

        }

        [Test]
        public void the_customer_should_be_saved_if_the_address_was_created()
        {
            //Arrange
            var customerToCreateDto = new CustomerToCreateDto()
            {
                Name = "Bob ",
                City = "Builder"
            };
            var mockAddressBuilder = new Mock<ICustomerAddressBuilder>();
            var mockCustomerRepository = new Mock<ICustomerRepository>();

            mockAddressBuilder
                .Setup(x => x.From(It.IsAny<CustomerToCreateDto>()))
                .Returns(() => new Address());

            var customerService = new CustomerService(
                mockCustomerRepository.Object, mockAddressBuilder.Object);

            //Act
            customerService.Create(customerToCreateDto);

            //Assert
            mockCustomerRepository.Verify(y => y.Save(It.IsAny<Customer>()));
        }
    }
}