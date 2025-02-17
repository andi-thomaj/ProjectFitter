namespace ProjectFitter.Api.Data.Entities
{
    public class ICNumber : BaseEntity
    {
        public string Number { get; set; }
        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}
