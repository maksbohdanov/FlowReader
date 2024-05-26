using FlowReader.Application.Models;

namespace FlowReader.Models
{
    public class NewsViewModel
    {
        public IEnumerable<NewsResponseModel> News { get; set; }
        public IEnumerable<CategoryResponseModel> Categories { get; set; }
    }
}
