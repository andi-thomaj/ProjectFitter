namespace ProjectFitter.Api.Controllers.Customer.Requests
{
    public class ValidateSixDigitPinRequest
    {
        public string ICNumber { get; set; }
        public string SixDigitPin { get; set; }
    }
}
