using RestaurantEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantDAL.Repost
{
   public  interface IHelpRespost
    {
        void AddHelp(Help help);
        void UpdateHelp(Help help);
        void DeleteHelp(int helpId);
        Help GetHelpById(int helpId);
        IEnumerable<Help> GetAllHelps();
    }
}
