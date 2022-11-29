using RestaurantDAL.Repost;
using RestaurantEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantBLL.Services
{
    public class HelpServise
    {
        IHelpRespost _help;
        public HelpServise (IHelpRespost help)
        {
            _help = help;
        }
        public void AddHelp(Help  help)
        {
            _help.AddHelp(help);
        }
        public void DeleteHelp(int helpId)
        {
            _help.DeleteHelp(helpId);
        }
        public void UpdateHelp(Help help)
        {
            _help.UpdateHelp(help);
        }
        public Help GetHelpById(int helpId)
        {
            return _help.GetHelpById(helpId);
        }
        public IEnumerable<Help> GetAllHelps()
        {
            return _help.GetAllHelps();
        }
    }
}
