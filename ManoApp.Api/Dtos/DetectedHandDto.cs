namespace ManoApp.Api.Dtos
{
    public class DetectedHandDto
    {
        public string Handedness { get; set; } = string.Empty;
        public double Score { get; set; }
        public List<HandLandmarkDto> Landmarks { get; set; } = new List<HandLandmarkDto>();
    }
}