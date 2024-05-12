using FlowReader.Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace FlowReader.Core.Identity
{
    public class ApplicationUser : IdentityUser 
    {
        public virtual ICollection<Category> Categories { get; set; } = new HashSet<Category>();

        public virtual ICollection<Feed> Feeds { get; set; } = new HashSet<Feed>();
    }
}
