namespace ProjectFitter.Api.Data.Entities
{
    public class Customer : BaseEntity
    {
        public string FullName { get; set; }
        public string MobileNumber { get; set; }
        public string EmailAddress { get; set; }
        public Guid ICNumberId { get; set; }
        public ICNumber ICNumber { get; set; }
        public bool IsSMSVerified { get; set; }
        public bool IsEmailVerified { get; set; }
        public bool HasAcceptedTermsAndConditions { get; set; }
        public string SixDigitsPin { get; set; }
        public bool HasConfirmedSixDigitsPin { get; set; }
        public bool HasEnabledSeamlessLogin { get; set; }
        public bool IsDraft { get; set; }
    }
}
