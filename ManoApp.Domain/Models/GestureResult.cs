namespace ManoApp.Domain.Models
{
    public class GestureResult
    {
        public string Handedness { get; set; } = string.Empty;
        public int FingerCount { get; set; }
        public string GestureName { get; set; } = string.Empty;
    }
}
