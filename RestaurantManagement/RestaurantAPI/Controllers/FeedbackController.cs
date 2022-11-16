using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantBLL.Services;
using RestaurantEntity;
using System.Collections.Generic;

namespace RestaurantAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        FeedbackService _feedbackService;
        public FeedbackController(FeedbackService feedbackService)
        {
            _feedbackService = feedbackService;
        }

        [HttpGet("GetAllFeedbacks")]
        public IEnumerable<Feedback> GetAllFeedbacks()
        {
            return _feedbackService.GetAllFeedbacks();
        }

        [HttpPost("AddFeedback")]
        public IActionResult AddFeedback(Feedback feedback)
        {
            _feedbackService.AddFeedback(feedback);
            return Ok("Feedback added successfully");
        }

        [HttpDelete("DeleteFeedback")]
        public IActionResult DeleteFeedback(int feedbackId)
        {
            _feedbackService.DeleteFeedBack(feedbackId);
            return Ok("Feedback Deleted successfully");
        }

        [HttpPut("UpdateFeedback")]
        public IActionResult UpdateFeedback(Feedback feedback)
        {
            _feedbackService.UpdateFeedback(feedback);
            return Ok("Feedback updated successfully");
        }

        [HttpGet("GetFeedbackById")]
        public Feedback GetFeedbackById(int feedbackId)
        {
            return _feedbackService.GetFeedbackById(feedbackId);
        }
    }
}
