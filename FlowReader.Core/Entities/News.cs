using FlowReader.Core.Common;

namespace FlowReader.Core.Entities
{
    public class News : BaseEntity
    {
        public string Title { get; set; } = string.Empty;
        public string Link { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime PublishingDate { get; set; }
        public string ItemId { get; set; } = string.Empty;

        public virtual Feed? Feed { get; set; }
        public Guid FeedId { get; set; }
    }
}
