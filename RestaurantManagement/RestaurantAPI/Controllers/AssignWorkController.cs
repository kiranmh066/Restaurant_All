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
            #region Function for Getting all the Assign work 

            return _assignWorkService.GetAssignWork();
            #endregion
        }



        [HttpDelete("DeleteAssignWork")]
        public IActionResult DeleteAssignWork(int assignWorkId)
        {
            #region Function for Deleting  the Assign  given by the headcheif to the cheif by assign id

            _assignWorkService.DeleteAssignWork(assignWorkId);
            return Ok("AssignWork deleted Successfully");
            #endregion
        }

        [HttpPut("UpdateAssignWork")]
        public IActionResult UpdateAssignWork(AssignWork assignWork)
        {
            #region Function for Updating  the Assign work  given by the headcheif to the cheif

            _assignWorkService.UpdateAssignWork(assignWork);
            return Ok("AssignWork Updated Successfully");
            #endregion
        }

        [HttpGet("GetAssignWorkById")]
        public AssignWork GetAssignWorkById(int assignWorkId)
        {
            #region Function for getting  all the details of the Assign work given by  its  assign id

            return _assignWorkService.GetAssignWorkById(assignWorkId);
            #endregion
        }

        [HttpGet("GetAssignWorkBySpeciality")]
        public IEnumerable<AssignWork> GetAssignWorkBySpeciality(string speciality)
        {
            #region Function for getting   all the details of the Assign work given by  its Speciality

            return _assignWorkService.GetAssignWorkBySpeciality(speciality);
            #endregion
        }

        [HttpGet("GetAssignWorkByEmpId")]
        public IEnumerable<AssignWork> GetAssignWorkByEmpId(int empId)
        {
            #region Function for getting   all the details of the Assign work given by  its  Emp Id

            return _assignWorkService.GetAssignWorkByEmpId(empId);
            #endregion
        }

        [HttpPost("AddAssignWork")]
        public IActionResult AddAssignWork(AssignWork assignWorkInfo)
        {
            #region Function for Adding  the Assign work given by the headcheif to the cheif

            _assignWorkService.AddAssignWork(assignWorkInfo);
            return Ok("Register successfully!!");
            #endregion 
        }

        [HttpGet("GetAssignWorkByOrderId")]

        public AssignWork GetAssignWorkByOrderId(int OrderId)
        {
            #region Function for getting   the Assign work given

            return _assignWorkService.AssignWorkByOrderId(OrderId);
            #endregion
        }

    }
}
