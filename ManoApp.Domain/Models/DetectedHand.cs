namespace ManoApp.Domain.Models
{
    public class DetectedHand
    {
        public string Handedness { get; set; } = string.Empty;
        public double Score { get; set; }
        public List<HandLandmark> Landmarks { get; set; } = new List<HandLandmark>();
    }
}