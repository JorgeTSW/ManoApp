using ManoApp.Domain.Interfaces;
using ManoApp.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace ManoApp.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HandController : ControllerBase
    {
        private readonly IGestureClassifier _gestureClassifier;

        public HandController(IGestureClassifier gestureClassifier)
        {
            _gestureClassifier = gestureClassifier;
        }

        [HttpPost]
        public IActionResult AnalyzeHand([FromBody] List<DetectedHand> hands)
        {
            var results = _gestureClassifier.Classify(hands);
            return Ok(results);
        }
    }
}