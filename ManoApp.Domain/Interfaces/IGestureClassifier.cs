using ManoApp.Domain.Models;

namespace ManoApp.Domain.Interfaces
{
    public interface IGestureClassifier
    {
        List<GestureResult> Classify(List<DetectedHand> hands);
    }
}