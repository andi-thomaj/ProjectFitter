using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectFitter.Api.Data.Entities;

namespace ProjectFitter.Api.Data.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.FullName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.MobileNumber)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(x => x.EmailAddress)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.IsSMSVerified)
                .IsRequired();

            builder.Property(x => x.IsEmailVerified)
                .IsRequired();

            builder.Property(x => x.HasAcceptedTermsAndConditions)
                .IsRequired();

            builder.Property(x => x.SixDigitsPin)
                .IsRequired()
                .HasMaxLength(6);

            builder.Property(x => x.HasConfirmedSixDigitsPin)
                .IsRequired();

            builder.Property(x => x.HasEnabledSeamlessLogin)
                .IsRequired();

            builder.HasOne(x => x.ICNumber)
                .WithOne(x => x.Customer)
                .HasForeignKey<ICNumber>(x => x.CustomerId);
        }
    }
}
