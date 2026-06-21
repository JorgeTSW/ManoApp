namespace ManoApp.Api.Dtos
{
    public class GestureResultDto
    {
        public string Handedness { get; set; } = string.Empty;
        public int FingerCount { get; set; }
        public string GestureName { get; set; } = string.Empty;
    }
}