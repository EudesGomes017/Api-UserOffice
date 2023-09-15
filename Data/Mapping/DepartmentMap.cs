using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Mapping;

public class DepartmentMap : IEntityTypeConfiguration<Department>
{

    public void Configure(EntityTypeBuilder<Department> builder)
    {
        builder.ToTable("User");
        builder.HasKey(x => x.Id); 
        builder.Property(x => x.Namedepartment).IsRequired().HasMaxLength(255);
        builder.Property(x => x.Responsible).IsRequired().HasMaxLength(255);
        builder.Property(x => x.UserId).IsRequired();
        builder.Property(x => x.Qulification).IsRequired();
        builder.Property(x => x.CreatedAt).IsRequired().HasMaxLength(255);
        builder.Property(x => x.UpdateAt).IsRequired().HasMaxLength(255);

        builder.HasOne(s => s.user)
        .WithMany(g => g.Departments)
        .HasForeignKey(s => s.UserId);
    }
}

