using ManoApp.Domain.Interfaces;
using ManoApp.Domain.Models;
using ManoApp.Domain.Services;
using Xunit;

namespace ManoApp.Domain.Tests
{
    public class GestureClassifierTests
    {
        private readonly IGestureClassifier _classifier;

        public GestureClassifierTests()
        {
            _classifier = new GestureClassifier();
        }

        [Fact]
        public void Classify_ManoAbierta_DebeRegresarCincoYNombreCorrecto()
        {
            var hand = CrearManoAbierta();
            var hands = new List<DetectedHand> { hand };

            var results = _classifier.Classify(hands);

            Assert.Single(results);
            Assert.Equal(5, results[0].FingerCount);
            Assert.Equal("Mano abierta", results[0].GestureName);
        }

        [Fact]
        public void Classify_Puno_DebeRegresarCeroYNombreCorrecto()
        {
            var hand = CrearPuno();
            var hands = new List<DetectedHand> { hand };

            var results = _classifier.Classify(hands);

            Assert.Single(results);
            Assert.Equal(0, results[0].FingerCount);
            Assert.Equal("Puño", results[0].GestureName);
        }

        [Fact]
        public void Classify_PulgarArriba_DebeRegresarUnoYNombreCorrecto()
        {
            var hand = CrearPulgarArriba();
            var hands = new List<DetectedHand> { hand };

            var results = _classifier.Classify(hands);

            Assert.Single(results);
            Assert.Equal(1, results[0].FingerCount);
            Assert.Equal("Pulgar arriba", results[0].GestureName);
        }

        private static DetectedHand CrearManoAbierta()
        {
            return new DetectedHand
            {
                Handedness = "Right",
                Score = 0.95,
                Landmarks = new List<HandLandmark>
                {
                    new() { X = 0.50, Y = 0.80, Z = 0.0 },
                    new() { X = 0.45, Y = 0.75, Z = 0.0 },
                    new() { X = 0.40, Y = 0.65, Z = 0.0 },
                    new() { X = 0.35, Y = 0.55, Z = 0.0 },
                    new() { X = 0.30, Y = 0.45, Z = 0.0 },
                    new() { X = 0.45, Y = 0.55, Z = 0.0 },
                    new() { X = 0.45, Y = 0.40, Z = 0.0 },
                    new() { X = 0.45, Y = 0.28, Z = 0.0 },
                    new() { X = 0.45, Y = 0.18, Z = 0.0 },
                    new() { X = 0.50, Y = 0.55, Z = 0.0 },
                    new() { X = 0.50, Y = 0.38, Z = 0.0 },
                    new() { X = 0.50, Y = 0.25, Z = 0.0 },
                    new() { X = 0.50, Y = 0.15, Z = 0.0 },
                    new() { X = 0.55, Y = 0.55, Z = 0.0 },
                    new() { X = 0.55, Y = 0.40, Z = 0.0 },
                    new() { X = 0.55, Y = 0.28, Z = 0.0 },
                    new() { X = 0.55, Y = 0.18, Z = 0.0 },
                    new() { X = 0.60, Y = 0.58, Z = 0.0 },
                    new() { X = 0.62, Y = 0.45, Z = 0.0 },
                    new() { X = 0.63, Y = 0.35, Z = 0.0 },
                    new() { X = 0.64, Y = 0.27, Z = 0.0 }
                }
            };
        }

        private static DetectedHand CrearPuno()
        {
            return new DetectedHand
            {
                Handedness = "Right",
                Score = 0.95,
                Landmarks = new List<HandLandmark>
                {
                    new() { X = 0.50, Y = 0.80, Z = 0.0 },
                    new() { X = 0.45, Y = 0.75, Z = 0.0 },
                    new() { X = 0.40, Y = 0.65, Z = 0.0 },
                    new() { X = 0.38, Y = 0.60, Z = 0.0 },
                    new() { X = 0.42, Y = 0.58, Z = 0.0 },
                    new() { X = 0.45, Y = 0.55, Z = 0.0 },
                    new() { X = 0.46, Y = 0.60, Z = 0.0 },
                    new() { X = 0.47, Y = 0.65, Z = 0.0 },
                    new() { X = 0.48, Y = 0.68, Z = 0.0 },
                    new() { X = 0.50, Y = 0.55, Z = 0.0 },
                    new() { X = 0.51, Y = 0.60, Z = 0.0 },
                    new() { X = 0.52, Y = 0.65, Z = 0.0 },
                    new() { X = 0.53, Y = 0.68, Z = 0.0 },
                    new() { X = 0.55, Y = 0.55, Z = 0.0 },
                    new() { X = 0.56, Y = 0.60, Z = 0.0 },
                    new() { X = 0.57, Y = 0.65, Z = 0.0 },
                    new() { X = 0.58, Y = 0.68, Z = 0.0 },
                    new() { X = 0.60, Y = 0.58, Z = 0.0 },
                    new() { X = 0.61, Y = 0.62, Z = 0.0 },
                    new() { X = 0.62, Y = 0.66, Z = 0.0 },
                    new() { X = 0.63, Y = 0.69, Z = 0.0 }
                }
            };
        }

        private static DetectedHand CrearPulgarArriba()
        {
            return new DetectedHand
            {
                Handedness = "Right",
                Score = 0.95,
                Landmarks = new List<HandLandmark>
                {
                    new() { X = 0.50, Y = 0.80, Z = 0.0 },
                    new() { X = 0.45, Y = 0.75, Z = 0.0 },
                    new() { X = 0.40, Y = 0.65, Z = 0.0 },
                    new() { X = 0.35, Y = 0.55, Z = 0.0 },
                    new() { X = 0.30, Y = 0.45, Z = 0.0 },
                    new() { X = 0.45, Y = 0.55, Z = 0.0 },
                    new() { X = 0.46, Y = 0.60, Z = 0.0 },
                    new() { X = 0.47, Y = 0.65, Z = 0.0 },
                    new() { X = 0.48, Y = 0.68, Z = 0.0 },
                    new() { X = 0.50, Y = 0.55, Z = 0.0 },
                    new() { X = 0.51, Y = 0.60, Z = 0.0 },
                    new() { X = 0.52, Y = 0.65, Z = 0.0 },
                    new() { X = 0.53, Y = 0.68, Z = 0.0 },
                    new() { X = 0.55, Y = 0.55, Z = 0.0 },
                    new() { X = 0.56, Y = 0.60, Z = 0.0 },
                    new() { X = 0.57, Y = 0.65, Z = 0.0 },
                    new() { X = 0.58, Y = 0.68, Z = 0.0 },
                    new() { X = 0.60, Y = 0.58, Z = 0.0 },
                    new() { X = 0.61, Y = 0.62, Z = 0.0 },
                    new() { X = 0.62, Y = 0.66, Z = 0.0 },
                    new() { X = 0.63, Y = 0.69, Z = 0.0 }
                }
            };
        }
    }
}