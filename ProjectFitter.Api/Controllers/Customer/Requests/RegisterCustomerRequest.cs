namespace ProjectFitter.Api.Controllers.Customer.Requests
{
    public class RegisterCustomerRequest
    {
        public string FullName { get; set; }
        public string MobileNumber { get; set; }
        public string EmailAddress { get; set; }
        public string ICNumber { get; set; }
    }
}
