using Microsoft.EntityFrameworkCore;
using RestaurantEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RestaurantDAL.Repost
{
    public class HelpRepost:IHelpRespost
    {
       
            RestaurantDbContext _dbContext;//default private

            public HelpRepost(RestaurantDbContext dbContext)
            {
                _dbContext = dbContext;
            }
            public void AddHelp(Help help)
            {
                #region Adding help details to database
                _dbContext.tbl_Help.Add(help);
                _dbContext.SaveChanges();
                #endregion
            }

            public void DeleteHelp(int helpId)
            {
                #region Deleting help details from database
                var help = _dbContext.tbl_Help.Find(helpId);
                _dbContext.tbl_Help.Remove(help);
                _dbContext.SaveChanges();
                #endregion
            }

            public IEnumerable<Help> GetAllHelps()
            {
            #region Show all existing helps
            return _dbContext.tbl_Help.ToList();
            #endregion
        }

            public Help GetHelpById(int helpId)
            {
                #region Search help details in database through helpID
                return _dbContext.tbl_Help.Find(helpId);
                #endregion
            }

            public void UpdateHelp(Help help)
            {
            #region Updating help details in database
            _dbContext.Entry(help).State = EntityState.Modified;
            _dbContext.SaveChanges();
            #endregion
        }
        }
    }


