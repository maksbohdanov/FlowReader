using System.ComponentModel.DataAnnotations;

namespace FlowReader.Models
{
    public class UserViewModel
    {
        public string Id { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string UserName { get; set; }
    }
}
