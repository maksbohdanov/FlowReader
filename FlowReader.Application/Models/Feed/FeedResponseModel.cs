namespace FlowReader.Application.Models
{
    public class FeedResponseModel: BaseResponseModel
    {
        public string UserTitle { get; set; } = string.Empty;
        public string Link { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime? PublishingDate { get; set; }
    }
}
