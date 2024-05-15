using FlowReader.Core.Common;
using FlowReader.Core.Identity;

namespace FlowReader.Core.Entities
{
    public class Feed : BaseEntity
    {
        public string UserTitle { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Link { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime? PublishingDate { get; set; }

        public virtual ApplicationUser? User { get; set; }
        public string? UserId { get; set; }
        public virtual ICollection<News> News { get; set; } = new HashSet<News>();
        public virtual ICollection<Category> Categories { get; set; } = new HashSet<Category>();
    }
}
