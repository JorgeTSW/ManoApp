namespace ManoApp.Domain.Models
{
    public class GestureLogEntry
    {
        public DateTime Timestamp { get; set; }
        public string RequestId { get; set; } = string.Empty;
        public string Handedness { get; set; } = string.Empty;
        public int FingerCount { get; set; }
        public string GestureName { get; set; } = string.Empty;
    }
}