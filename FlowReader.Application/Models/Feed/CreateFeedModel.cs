namespace FlowReader.Application.Models
{
    public class CreateFeedModel
    {
        public string Title { get; set; } = string.Empty;
        public string Link { get; set; } = string.Empty;
        public bool IsPublic { get; set; }
    }
}
