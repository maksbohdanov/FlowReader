using FlowReader.Core.Common;
using FlowReader.Core.Identity;

namespace FlowReader.Core.Entities
{
    public class Category: BaseEntity
    {
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;

        public virtual ICollection<Feed> Feeds { get; set; } = new HashSet<Feed>();
        public virtual ICollection<ApplicationUser> Users { get; set; } = new HashSet<ApplicationUser>();
    }
}
