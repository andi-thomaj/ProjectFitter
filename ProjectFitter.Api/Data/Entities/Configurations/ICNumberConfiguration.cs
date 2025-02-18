using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectFitter.Api.Data.Entities;

namespace ProjectFitter.Api.Data.Entities.Configurations
{
    public class ICNumberConfiguration : IEntityTypeConfiguration<ICNumber>
    {
        public void Configure(EntityTypeBuilder<ICNumber> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Number)
                .IsRequired()
                .HasMaxLength(20);
        }
    }
}
