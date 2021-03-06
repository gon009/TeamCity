﻿namespace TeamCity.Code
{
    public interface ICustomerService
    {
        void Create(CustomerToCreateDto customerToCreateDto);
        Customer BuildCustomerObjectFrom(CustomerToCreateDto customerToCreateDto);
    }
}