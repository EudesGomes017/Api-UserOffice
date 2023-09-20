using Domain.Enums;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.Json.Serialization;

namespace Data.Mapping;

public class UserMap : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("User");
        builder.HasKey(x => x.Id); //CHAVE PRIMARIA
        builder.Property(c => c.Id).HasMaxLength(10).IsRequired();
        builder.Property(x => x.Name).IsRequired().HasMaxLength(255); //PROPRIEADE
        builder.Property(x => x.Email).IsRequired().HasMaxLength(255);
        builder.Property(x => x.Password).IsRequired();
        builder.Property(x => x.FancyName).IsRequired();
        builder.Property(x => x.UserDesativado).IsRequired();
        builder.Property(x => x.CPF).IsRequired();
        builder.Property(x => x.CNPJ).IsRequired();
        builder.Property(x => x.Person).IsRequired();
       // builder.Property(x => x.departmentId).HasMaxLength(255).IsRequired();
        builder.Property(x => x.CreatedAt).IsRequired().HasMaxLength(255);
        builder.Property(x => x.UpdateAt).IsRequired().HasMaxLength(255);

        builder.HasOne(s => s.AddressRegister)
            .WithOne(ad => ad.User)
            .HasForeignKey<AddressRegister>(ad => ad.UserId);

        builder.HasMany(u => u.Departments)
            .WithOne(mt => mt.user) 
            .HasForeignKey(mt => mt.UserId);

      

    }
}
