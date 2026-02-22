using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VideoGamesCatalog.DataAccess.Models;

namespace VideoGamesCatalog.DataAccess.Persistence.Configurations;

internal sealed class GenreEntityConfiguration : IEntityTypeConfiguration<GenreEntity>
{
    public void Configure(EntityTypeBuilder<GenreEntity> entityTypeBuilder)
    {
        entityTypeBuilder.HasKey(genre => genre.Id);

        entityTypeBuilder.Property(genre => genre.Id)
             .HasDefaultValueSql("NEWSEQUENTIALID()");

        entityTypeBuilder.Property(genre => genre.Name)
            .IsRequired()
            .HasMaxLength(100);
    }
}
