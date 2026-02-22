using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VideoGamesCatalog.DataAccess.Models;

namespace VideoGamesCatalog.DataAccess.Persistence.Configurations;

internal sealed class VideoGameEntityConfiguration : IEntityTypeConfiguration<VideoGameEntity>
{
    public void Configure(EntityTypeBuilder<VideoGameEntity> entityTypeBuilder)
    {
        entityTypeBuilder.HasKey(videoGame => videoGame.Id);

        entityTypeBuilder.Property(videoGame => videoGame.Id)
            .HasDefaultValueSql("NEWSEQUENTIALID()");

        entityTypeBuilder.Property(videoGame => videoGame.Title)
            .IsRequired()
            .HasMaxLength(200);

        entityTypeBuilder.Property(videoGame => videoGame.Description)
            .HasMaxLength(2000);
    }
}
