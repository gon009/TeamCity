namespace TeamCity.Code
{
    public class Customer
    {
        private string _name;
        private string _city;
        public Customer(string city, string name)
        {
            _city = city;
            _name = name;
        }

        public object MailingAddress { get; set; }
    }
}