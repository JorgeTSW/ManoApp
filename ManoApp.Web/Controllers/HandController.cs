using ManoApp.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using ManoApp.Domain.Interfaces;
using ManoApp.Application.Services;
using Microsoft.AspNetCore.Authorization;

namespace ManoApp.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HandController : ControllerBase
    {
        private readonly GestureLoggingService _gestureLoggingService;
        private readonly IGestureLogRepository _gestureLogRepository;

        public HandController(GestureLoggingService gestureLoggingService, IGestureLogRepository gestureLogRepository)
        {
            _gestureLoggingService = gestureLoggingService;
            _gestureLogRepository = gestureLogRepository;
        }

        [HttpPost]
        public IActionResult AnalyzeHand([FromBody] List<DetectedHand> hands)
        {
            var results = _gestureLoggingService.ClassifyAndLog(hands);
            return Ok(results);
        }

        [HttpGet("logs")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetLogs()
        {
            var logs = _gestureLogRepository.GetAll();
            return Ok(logs);
        }
    }
}