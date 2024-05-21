namespace FlowReader.Application.Models
{
    public class ToggleFavoriteCategoryModel
    {
        public Guid CategoryId { get; set; }
        public bool IsSubscribed { get; set; }
    }
}
