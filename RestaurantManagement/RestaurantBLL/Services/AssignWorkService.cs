using RestaurantDAL.Repost;
using RestaurantEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantBLL.Services
{
    public class AssignWorkService
    {
        IAssignWorkRepost _assignWorkRepository;


        //Unable to resolve ====>>>> Object issues
        public AssignWorkService(IAssignWorkRepost assignWorkRepository)
        {
            _assignWorkRepository = assignWorkRepository;
        }


        //Update AssignWork
        public void UpdateAssignWork(AssignWork assignWork)
        {
            _assignWorkRepository.UpdateAssignWork(assignWork);
        }

        //Delete AssignWork
        public void DeleteAssignWork(int assignWorkId)
        {
            _assignWorkRepository.DeleteAssignWork(assignWorkId);
        }

        //Get AssignWorkByID
        public AssignWork GetAssignWorkById(int assignWorkId)
        {
            return _assignWorkRepository.GetAssignWorkById(assignWorkId);
        }
        public IEnumerable<AssignWork> GetAssignWorkByEmpId(int empId)
        {
            return _assignWorkRepository.GetAssignWorkByEmpId(empId);
        }

        //Get AssignWorks
        public IEnumerable<AssignWork> GetAssignWork()
        {
            return _assignWorkRepository.GetAssignWorks();
        }

        //Get AssignWorks By Speciality
        public IEnumerable<AssignWork> GetAssignWorkBySpeciality(string speciality)
        {
            return _assignWorkRepository.GetAssignWorksBySpeciality(speciality);
        }

        //Registering AssignWork
        public void AddAssignWork(AssignWork assignWorkInfo)
        {
            _assignWorkRepository.AddAssignWork(assignWorkInfo);
        }
    }
}
