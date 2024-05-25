namespace FlowReader.Application.Models
{
    public class FeedCategoriesModel
    {
        public Guid FeedId { get; set; }
        public List<Guid> CategoryIds { get; set; }
    }
}
