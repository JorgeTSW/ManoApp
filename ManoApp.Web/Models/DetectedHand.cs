namespace ManoApp.Web.Models
{
    public class DetectedHand
    {
        public string Handedness { get; set; }
        public double Score { get; set; }
        public List<HandLandmark> Landmarks { get; set; } = new List<HandLandmark>();
    }
}