using FlowReader.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FlowReader.DataAccess.Configurations
{
    public class FeedConfiguration : IEntityTypeConfiguration<Feed>
    {
        public void Configure(EntityTypeBuilder<Feed> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("FeedId");

            builder.Property(x => x.Title)                
                .IsRequired();

            builder.Property(x => x.Link)
                .IsRequired();
        }
    }
}
