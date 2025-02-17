namespace ProjectFitter.Api.Controllers.Customer.Requests
{
    public class ValidateSMSCodeRequest
    {
        public string ICNumber { get; set; }
        public string SMSCode { get; set; }
    }
}
