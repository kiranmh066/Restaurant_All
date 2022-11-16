using RestaurantEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantDAL.Repost
{
    public interface IAssignWorkRepost
    {
        void AddAssignWork(AssignWork assignWork);
        void UpdateAssignWork(AssignWork assignWork);

        void DeleteAssignWork(int assignWorkId);

        AssignWork GetAssignWorkById(int assignWorkId);

        IEnumerable<AssignWork> GetAssignWorkByEmpId(int empId);

        IEnumerable<AssignWork> GetAssignWorks();

        IEnumerable<AssignWork> GetAssignWorksBySpeciality(string speciality);
    }
}
