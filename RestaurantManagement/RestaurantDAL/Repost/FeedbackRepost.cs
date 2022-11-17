using Microsoft.EntityFrameworkCore;
using RestaurantEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RestaurantDAL.Repost
{
    public class FeedbackRepost:IFeedbackRepost
    {
        RestaurantDbContext _dbContext;//default private

        public FeedbackRepost(RestaurantDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void AddFeedback(Feedback feedback)
        {
            #region Adding feedback details to database
             _dbContext.tbl_Feedback.Add(feedback);
             _dbContext.SaveChanges();
            #endregion
        }

        public void DeleteFeedback(int feedbackId)
        {
            #region Deleting feedback details from database
            var feedback =  _dbContext.tbl_Feedback.Find(feedbackId);
             _dbContext.tbl_Feedback.Remove(feedback);
             _dbContext.SaveChanges();
            #endregion
        }

        public IEnumerable<Feedback> GetAllFeedbacks()
        {
            #region Show all existing feedbacks
            return  _dbContext.tbl_Feedback.ToList();
            #endregion
        }

        public Feedback GetFeedbackById(int feedbackId)
        {
            #region Search feedback details in database through feedbackID
            return  _dbContext.tbl_Feedback.Find(feedbackId);
            #endregion
        }

        public void UpdateFeedback(Feedback Feedback)
        {
            #region Updating feedback details in database
             _dbContext.Entry(Feedback).State = EntityState.Modified;
             _dbContext.SaveChanges();
            #endregion
        }
    }
}
