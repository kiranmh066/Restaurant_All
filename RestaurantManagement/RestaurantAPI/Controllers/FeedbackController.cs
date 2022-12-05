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
            #region Function for getting all the feedback


            return _feedbackService.GetAllFeedbacks();
            #endregion
        }

        [HttpPost("AddFeedback")]
        public IActionResult AddFeedback(Feedback feedback)
        {

            #region Function for Adding the Feedback by its object.

            _feedbackService.AddFeedback(feedback);
            return Ok("Feedback added successfully");
            #endregion
        }

        [HttpDelete("DeleteFeedback")]
        public IActionResult DeleteFeedback(int feedbackId)
        {
            #region Function for deleting the Feedback by its feedbackId.

            _feedbackService.DeleteFeedBack(feedbackId);
            return Ok("Feedback Deleted successfully");
            #endregion
        }

        [HttpPut("UpdateFeedback")]
        public IActionResult UpdateFeedback(Feedback feedback)
        {
            #region Function for Updating the Feedback by its object.

            _feedbackService.UpdateFeedback(feedback);
            return Ok("Feedback updated successfully");

            #endregion
        }

        [HttpGet("GetFeedbackById")]
        public Feedback GetFeedbackById(int feedbackId)
        {
            #region Function for Geeting the Feedback by its FeedbackId.

            return _feedbackService.GetFeedbackById(feedbackId);
            #endregion
        }
    }
}
