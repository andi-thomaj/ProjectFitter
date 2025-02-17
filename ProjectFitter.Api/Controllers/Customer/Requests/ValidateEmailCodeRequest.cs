namespace ProjectFitter.Api.Controllers.Customer.Requests
{
    public class ValidateEmailCodeRequest
    {
        public string ICNumber { get; set; }
        public string EmailCode { get; set; }
    }
}
