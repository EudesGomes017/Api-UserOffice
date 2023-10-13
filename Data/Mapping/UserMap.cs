using Domain.Models;
using Domain.Services.serviceUser.Criptorgrafia;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

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
        builder.Property(x => x.Documento).IsRequired();
        builder.Property(x => x.Role).IsRequired();
        builder.Property(x => x.IsActive).IsRequired();
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



        builder.HasData(new List<User>() {
        new User()
        {
            Id = 1,
            Name = "John 1",
            //     UserName = "johndoe",
            CreatedAt = DateTime.Now.ToLocalTime(),
            UpdateAt = DateTime.Now.ToLocalTime(),
            Email = "johndoe@gmail.com",
            Password = new EncryptPassword("vJyf-9$27j#0").encrypt("12341234"),
            Documento = "100.100.100-19",
            FancyName = "",
            //Cep = "85263789",
            //State = "SC",
            //City = "Joinville",
            //Complement = "casa",
            //Neighborhood = "Comasa",
            //NumberDocument = "18535602000109",
            //TypeUser = TypeUserEnum.Administrador,
            //TypeDocument = TypeDocumentEnum.CNPJ,
            Role = "Administrador",
            IsActive = true
        },
            new User()
            {
                Id = 2,
                Name = "John 2",
                //     UserName = "johndoe",
                CreatedAt = DateTime.Now.ToLocalTime(),
                UpdateAt = DateTime.Now.ToLocalTime(),
                Email = "johndo2@gmail.com",
                Password = new EncryptPassword("vJyf-9$27j#0").encrypt("12341234"),
                Documento = "100.514.624-19",
                FancyName = "",
                //Cep = "85263789",
                //State = "SC",
                //City = "Joinville",
                //Complement = "casa",
                //Neighborhood = "Comasa",
                //NumberDocument = "18535602000109",
                //TypeUser = TypeUserEnum.Administrador,
                //TypeDocument = TypeDocumentEnum.CNPJ,
                Role = "Usuario",
                IsActive = true
            }
             }
              );

    }
}
