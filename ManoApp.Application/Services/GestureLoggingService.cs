using ManoApp.Domain.Interfaces;
using ManoApp.Domain.Models;

namespace ManoApp.Application.Services
{
    public class GestureLoggingService
    {
        private readonly IGestureClassifier _gestureClassifier;
        private readonly IGestureLogRepository _gestureLogRepository;

        public GestureLoggingService(IGestureClassifier gestureClassifier, IGestureLogRepository gestureLogRepository)
        {
            _gestureClassifier = gestureClassifier;
            _gestureLogRepository = gestureLogRepository;
        }

        public List<GestureResult> ClassifyAndLog(List<DetectedHand> hands)
        {
            var results = _gestureClassifier.Classify(hands);
            var requestId = Guid.NewGuid().ToString();
            var timestamp = DateTime.UtcNow;

            foreach (var result in results)
            {
                _gestureLogRepository.Save(new GestureLogEntry
                {
                    Timestamp = timestamp,
                    RequestId = requestId,
                    Handedness = result.Handedness,
                    FingerCount = result.FingerCount,
                    GestureName = result.GestureName
                });
            }

            return results;
        }
    }
}