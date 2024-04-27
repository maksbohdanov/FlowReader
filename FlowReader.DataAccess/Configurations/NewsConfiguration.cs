using FlowReader.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FlowReader.DataAccess.Configurations
{
    public class NewsConfiguration : IEntityTypeConfiguration<News>
    {
        public void Configure(EntityTypeBuilder<News> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("NewsId");

            builder.Property(x => x.Title)                
                .IsRequired();

            builder.Property(x => x.Description)
                .IsRequired();

            builder.HasOne(x => x.Feed)
                .WithMany(x => x.News)
                .HasForeignKey(x => x.FeedId);
        }
    }
}
