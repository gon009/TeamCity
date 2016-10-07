using System;
using System.Collections.Generic;

namespace PluralSight.Moq.Code
{
    public class CustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ICustomerAddressBuilder _customerAddressBuilder;
        public CustomerService(ICustomerRepository customerRepository, ICustomerAddressBuilder customerAddressBuilder)
        {
            _customerRepository = customerRepository;
            _customerAddressBuilder = customerAddressBuilder;
        }

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }


        public void Create(CustomerToCreateDto customerToCreateDto)
        {
            var customer = BuildCustomerObjectFrom(customerToCreateDto);

            _customerRepository.Save(customer);
        }
        //public void Create(IEnumerable<CustomerToCreateDto> customersToCreate)
        //{
        //    foreach (var customerToCreate in customersToCreate)
        //    {
        //        _customerRepository.Save(
        //            new Customer(
        //                customerToCreate.Name,
        //                customerToCreate.City));
        //    }
        //}


        public Customer BuildCustomerObjectFrom(CustomerToCreateDto customerToCreateDto)
        {
            return new Customer(customerToCreateDto.Name, customerToCreateDto.City);
        }

        public void Create(IEnumerable<CustomerToCreateDto> customerToCreate)
        {
            foreach (var customerToCreateDto in customerToCreate)
                _customerRepository.Save(new Customer(customerToCreateDto.Name, customerToCreateDto.City));
        }
    }

    public class InvalidCustomerMailingAddressException : Exception
    {
    }
}