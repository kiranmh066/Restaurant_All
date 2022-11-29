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
            return _HelpService.GetAllHelps();
        }

        [HttpPost("AddHelp")]
        public IActionResult AddHelp(Help help)
        {
            _HelpService.AddHelp(help);
            return Ok("Message send  successfully");
        }

        [HttpDelete("DeleteHelp")]
        public IActionResult DeleteHelp(int helpId)
        {
            _HelpService.DeleteHelp(helpId);
            return Ok("help Deleted successfully");
        }

        [HttpPut("UpdateHelp")]
        public IActionResult UpdateHelp(Help help)
        {
            _HelpService.UpdateHelp(help);
            return Ok("help updated successfully");
        }

        [HttpGet("GetHelpById")]
        public Help GetHelpById(int helpId)
        {
            return _HelpService.GetHelpById(helpId);
        }
    }
}
