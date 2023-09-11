using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Mapping;

public class AddressRegisterMap : IEntityTypeConfiguration<AddressRegister>
{
    public void Configure(EntityTypeBuilder<AddressRegister> builder)
    {
        builder.ToTable("Address");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Cep).IsRequired().HasMaxLength(10);
        builder.Property(x => x.Patio).IsRequired(); //logradouro
        builder.Property(x => x.Neighborhood).IsRequired();
        builder.Property(x => x.Locality).IsRequired();
        builder.Property(x => x.UF).IsRequired();
        builder.Property(x => x.Locality).IsRequired();
        builder.Property(x => x.CreatedAt).IsRequired().HasMaxLength(255);
        builder.Property(x => x.UpdateAt).IsRequired().HasMaxLength(255);

        builder.HasOne(ad => ad.User)
               .WithOne(s => s.AddressRegister)
               .HasForeignKey<AddressRegister>(ad => ad.UserId);
    }
}

