using ManoApp.Api.Dtos;
using ManoApp.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using ManoApp.Domain.Interfaces;
using ManoApp.Application.Services;
using Microsoft.AspNetCore.Authorization;

namespace ManoApp.Api.Controllers
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
        public IActionResult AnalyzeHand([FromBody] List<DetectedHandDto> handsDto)
        {
            var hands = handsDto.Select(MapToDomain).ToList();
            var results = _gestureLoggingService.ClassifyAndLog(hands);
            var resultsDto = results.Select(MapToDto).ToList();

            return Ok(resultsDto);
        }

        [HttpGet("logs")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetLogs()
        {
            var logs = _gestureLogRepository.GetAll();
            return Ok(logs);
        }

        private static DetectedHand MapToDomain(DetectedHandDto dto)
        {
            return new DetectedHand
            {
                Handedness = dto.Handedness,
                Score = dto.Score,
                Landmarks = dto.Landmarks.Select(l => new HandLandmark
                {
                    X = l.X,
                    Y = l.Y,
                    Z = l.Z
                }).ToList()
            };
        }

        private static GestureResultDto MapToDto(GestureResult result)
        {
            return new GestureResultDto
            {
                Handedness = result.Handedness,
                FingerCount = result.FingerCount,
                GestureName = result.GestureName
            };
        }
    }
}