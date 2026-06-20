using ManoApp.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace ManoApp.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HandController : ControllerBase
    {
        [HttpPost]
        public IActionResult AnalyzeHand([FromBody] List<DetectedHand> hands)
        {
            var results = new List<GestureResult>();

            foreach (var hand in hands)
            {
                var landmarks = hand.Landmarks;

                var extendedFingers = new Dictionary<string, bool>
                {
                    { "Thumb", false },
                    { "Index", false },
                    { "Middle", false },
                    { "Ring", false },
                    { "Pinky", false }
                };

                // Dedos verticales: punta mas arriba (Y menor) que la articulacion intermedia
                extendedFingers["Index"] = landmarks[8].Y < landmarks[6].Y;
                extendedFingers["Middle"] = landmarks[12].Y < landmarks[10].Y;
                extendedFingers["Ring"] = landmarks[16].Y < landmarks[14].Y;
                extendedFingers["Pinky"] = landmarks[20].Y < landmarks[18].Y;

                // Pulgar: se mueve lateralmente, depende de la mano
                if (hand.Handedness == "Right")
                {
                    extendedFingers["Thumb"] = landmarks[4].X < landmarks[2].X;
                }
                else
                {
                    extendedFingers["Thumb"] = landmarks[4].X > landmarks[2].X;
                }

                int fingerCount = extendedFingers.Values.Count(v => v);

                bool isFist = !extendedFingers["Thumb"] && !extendedFingers["Index"] &&
                              !extendedFingers["Middle"] && !extendedFingers["Ring"] &&
                              !extendedFingers["Pinky"];

                bool isThumbsUp = extendedFingers["Thumb"] && !extendedFingers["Index"] &&
                                  !extendedFingers["Middle"] && !extendedFingers["Ring"] &&
                                  !extendedFingers["Pinky"];

                bool isPeaceSign = !extendedFingers["Thumb"] && extendedFingers["Index"] &&
                                   extendedFingers["Middle"] && !extendedFingers["Ring"] &&
                                   !extendedFingers["Pinky"];

                bool isOpenHand = extendedFingers["Thumb"] && extendedFingers["Index"] &&
                                  extendedFingers["Middle"] && extendedFingers["Ring"] &&
                                  extendedFingers["Pinky"];

                string gestureName;

                if (isFist)
                {
                    gestureName = "Puño";
                }
                else if (isThumbsUp)
                {
                    gestureName = "Pulgar arriba";
                }
                else if (isPeaceSign)
                {
                    gestureName = "Paz";
                }
                else if (isOpenHand)
                {
                    gestureName = "Mano abierta";
                }
                else
                {
                    gestureName = $"{fingerCount} dedos";
                }

                results.Add(new GestureResult
                {
                    Handedness = hand.Handedness,
                    FingerCount = fingerCount,
                    GestureName = gestureName
                });
            }

            return Ok(results);
        }
    }
}