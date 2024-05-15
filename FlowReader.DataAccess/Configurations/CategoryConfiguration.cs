using FlowReader.Core.Entities;
using FlowReader.Core.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FlowReader.DataAccess.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("CategoryId");

            builder.Property(x => x.Code)                
                .IsRequired();

            builder.HasMany(x => x.Feeds)
                .WithMany(x => x.Categories)
                .UsingEntity(
                    l => l.HasOne(typeof(Feed)).WithMany().HasForeignKey("FeedId"),
                    r => r.HasOne(typeof(Category)).WithMany().HasForeignKey("CategoryId"));

            builder.HasMany(x => x.Users)
                .WithMany(x => x.Categories)
                .UsingEntity(
                    "UserFavorite",
                    l => l.HasOne(typeof(ApplicationUser)).WithMany().HasForeignKey("UserId"),
                    r => r.HasOne(typeof(Category)).WithMany().HasForeignKey("CategoryId"),
                    j =>
                    {
                        j.IndexerProperty<Guid>("Id");
                        j.HasKey("Id");
                    });
        }
    }
}
