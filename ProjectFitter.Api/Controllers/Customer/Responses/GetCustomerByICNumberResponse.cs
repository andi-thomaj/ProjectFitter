namespace ProjectFitter.Api.Controllers.Customer.Responses
{
    public class GetCustomerByICNumberResponse
    {
        public string FullName { get; set; }
        public string MobileNumber { get; set; }
        public string EmailAddress { get; set; }
        public string ICNumber { get; set; }
        public bool IsSMSVerified { get; set; }
        public bool IsEmailVerified { get; set; }
        public bool HasAcceptedTermsAndConditions { get; set; }
        public bool HasConfirmedSixDigitsPin { get; set; }
        public bool HasEnabledSeamlessLogin { get; set; }
        public bool IsDraft { get; set; }
    }
}
