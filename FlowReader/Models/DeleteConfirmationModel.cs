namespace FlowReader.Models
{
    public class DeleteConfirmationModel
    {
        public Guid Id { get; set; }
        public string ControllerName { get; set; } = string.Empty;
        public string ActionName { get; set; } = "Delete";
        public string ItemName { get; set; } = string.Empty;
    }
}
