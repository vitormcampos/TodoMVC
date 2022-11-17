using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TarefasMvc.Models;

namespace TarefasMvc.EfConfigs;

public class TodoContextConfiguration : IEntityTypeConfiguration<Todo>
{
    public void Configure(EntityTypeBuilder<Todo> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasColumnName("id");

        builder.Property(x => x.Title)
            .HasColumnName("title")
            .HasColumnType("nvarchar(100)")
            .IsRequired();

        builder.Property(x => x.Date)
            .HasColumnName("date")
            .HasColumnType("date")
            .IsRequired();

        builder.Property(x => x.IsCompleted)
            .HasColumnName("completed")
            .HasColumnType("bit")
            .IsRequired();
    }
}