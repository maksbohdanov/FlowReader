namespace FlowReader.Application.Models
{
    public class NewsResponseModel: BaseResponseModel
    {
        public string Title { get; set; } = string.Empty;
        public string Link { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
        public DateTime PublishingDate { get; set; }
    }
}
