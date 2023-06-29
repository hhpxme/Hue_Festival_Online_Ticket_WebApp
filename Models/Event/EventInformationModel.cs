namespace HF_WEB_API.Models.Event
{
    public class EventInformationModel
    {
        public string? Name { get; set; }

        public string? Description { get; set; }

        public string Location { get; set; } = string.Empty;

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }
    }
}
