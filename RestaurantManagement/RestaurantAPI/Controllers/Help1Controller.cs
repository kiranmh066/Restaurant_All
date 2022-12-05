using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantBLL.Services;
using RestaurantEntity;
using System.Collections.Generic;

namespace RestaurantAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Help1Controller : ControllerBase
    {
        private HelpServise _HelpService;


        public Help1Controller(HelpServise HelpService)
        {
            _HelpService = HelpService; ;
        }

        [HttpGet("GetAllHelps")]
        public IEnumerable<Help> GetAllHelps()
        {
            #region Function for Getting All the Help  by its object
            return _HelpService.GetAllHelps();
            #endregion
        }

        [HttpPost("AddHelp")]
        public IActionResult AddHelp(Help help)
        {
            #region Function for Adding the Help by its object
            _HelpService.AddHelp(help);
            return Ok("Message send  successfully");
            #endregion
        }

        [HttpDelete("DeleteHelp")]
        public IActionResult DeleteHelp(int helpId)
        {
            #region Function for Delete the Help by its helpId
            _HelpService.DeleteHelp(helpId);
            return Ok("help Deleted successfully");
            #endregion
        }

        [HttpPut("UpdateHelp")]
        public IActionResult UpdateHelp(Help help)
        {
            #region Function for Updating the Help by its object
            _HelpService.UpdateHelp(help);
            return Ok("help updated successfully");
            #endregion
        }

        [HttpGet("GetHelpById")]
        public Help GetHelpById(int helpId)
        {
            #region Function for Geeting the Help by its helpId
            return _HelpService.GetHelpById(helpId);
            #endregion
        }
    }
}
