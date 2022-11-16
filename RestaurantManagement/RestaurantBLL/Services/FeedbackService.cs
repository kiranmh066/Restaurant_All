using RestaurantDAL.Repost;
using RestaurantEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantBLL.Services
{
    public class FeedbackService
    {
        IFeedbackRepost _feedback;
        public FeedbackService(IFeedbackRepost feedback)
        {
            _feedback = feedback;
        }
        public void AddFeedback(Feedback feedback)
        {
            _feedback.AddFeedback(feedback);
        }
        public void DeleteFeedBack(int feedbackId)
        {
            _feedback.DeleteFeedback(feedbackId);
        }
        public void UpdateFeedback(Feedback feedback)
        {
            _feedback.UpdateFeedback(feedback);
        }
        public Feedback GetFeedbackById(int feedbackId)
        {
            return _feedback.GetFeedbackById(feedbackId);
        }
        public IEnumerable<Feedback> GetAllFeedbacks()
        {
            return _feedback.GetAllFeedbacks();
        }
    }
}
