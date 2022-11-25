using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantBLL.Services;
using RestaurantEntity;
using System.Collections.Generic;

namespace RestaurantAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssignWorkController : ControllerBase
    {
        private AssignWorkService _assignWorkService;

        public AssignWorkController(AssignWorkService assignWorkService)
        {
            _assignWorkService = assignWorkService;
        }

        [HttpGet("GetAssignWorks")]//
        public IEnumerable<AssignWork> GetAssignWorks()
        {
            return _assignWorkService.GetAssignWork();
        }



        [HttpDelete("DeleteAssignWork")]
        public IActionResult DeleteAssignWork(int assignWorkId)
        {
            _assignWorkService.DeleteAssignWork(assignWorkId);
            return Ok("AssignWork deleted Successfully");
        }

        [HttpPut("UpdateAssignWork")]
        public IActionResult UpdateAssignWork(AssignWork assignWork)
        {
            _assignWorkService.UpdateAssignWork(assignWork);
            return Ok("AssignWork Updated Successfully");
        }

        [HttpGet("GetAssignWorkById")]
        public AssignWork GetAssignWorkById(int assignWorkId)
        {
            return _assignWorkService.GetAssignWorkById(assignWorkId);
        }

        [HttpGet("GetAssignWorkBySpeciality")]
        public IEnumerable<AssignWork> GetAssignWorkBySpeciality(string speciality)
        {
            return _assignWorkService.GetAssignWorkBySpeciality(speciality);
        }

        [HttpGet("GetAssignWorkByEmpId")]
        public IEnumerable<AssignWork> GetAssignWorkByEmpId(int empId)
        {
            return _assignWorkService.GetAssignWorkByEmpId(empId);
        }

        [HttpPost("AddAssignWork")]
        public IActionResult AddAssignWork(AssignWork assignWorkInfo)
        {
            _assignWorkService.AddAssignWork(assignWorkInfo);
            return Ok("Register successfully!!");
        }
    }
}
