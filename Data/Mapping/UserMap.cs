﻿using Domain.Models;
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
        builder.Property(x => x.Role).IsRequired();
        builder.Property(x => x.Password).IsRequired();
        builder.Property(x => x.Email).IsRequired().HasMaxLength(255);
        builder.Property(x => x.FancyName).IsRequired();
        builder.Property(x => x.Document).IsRequired();
        builder.Property(x => x.IsActive).IsRequired();
        builder.Property(x => x.Person).IsRequired();
        builder.Property(x => x.cep).IsRequired();
        builder.Property(x => x.logradouro).IsRequired();
        builder.Property(x => x.complemento);
        builder.Property(x => x.bairro).IsRequired();
        builder.Property(x => x.localidade).IsRequired();
        builder.Property(x => x.uf).IsRequired();
        builder.Property(x => x.numero_da_casa).IsRequired();
        builder.Property(x => x.CreatedAt).IsRequired().HasMaxLength(255);
        builder.Property(x => x.UpdateAt).IsRequired().HasMaxLength(255);

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
            Document = "100.100.100-19",
            FancyName = "",
            cep = "85263789",
            logradouro = "teste",
            complemento = "casa",
            bairro = "comasa",
            localidade = "joinville",
            uf = "sc",
            numero_da_casa = "5",
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
                Document = "100.514.624-19",
                FancyName = "",
                cep = "85263789",
                logradouro = "teste",
                complemento = "casa",
                bairro = "comasa",
                localidade = "joinville",
                uf = "sc",
                numero_da_casa = "5",
                Role = "Usuario",
                IsActive = true
            }
        }
       );
    }
}
