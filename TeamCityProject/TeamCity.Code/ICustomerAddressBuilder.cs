namespace TeamCity.Code
{
    public interface ICustomerAddressBuilder
    {
        Address From(CustomerToCreateDto customerToCreateDto);
    }
}