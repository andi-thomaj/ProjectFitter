namespace ProjectFitter.Api.Controllers.Customer.Requests
{
    public class CreateSixDigitPinRequest
    {
        public string ICNumber { get; set; }
        public string SixDigitPin { get; set; }
    }
}
