using RestaurantDAL.Repost;
using RestaurantEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantBLL.Services
{
    public class HallTableService
    {

        IHallTableRepost _hallTableRepository;


        //Unable to resolve ====>>>> Object issues
        public HallTableService(IHallTableRepost hallTableRepository)
        {
            _hallTableRepository = hallTableRepository;
        }


        //Update HallTable
        public void UpdateHallTable(HallTable hallTable)
        {
            _hallTableRepository.UpdateHallTable(hallTable);
        }

        //Delete HallTable
        public void DeleteHallTable(int hallTableId)
        {
            _hallTableRepository.DeleteHallTable(hallTableId);
        }

        //Get HallTableByID
        public HallTable GetHallTableById(int hallTableId)
        {
            return _hallTableRepository.GetHallTableById(hallTableId);
        }

        //Get HallTables
        public IEnumerable<HallTable> GetHallTable()
        {
            return _hallTableRepository.GetHallTables();
        }

        //Registering HallTable
        public void AddHallTable(HallTable hallTableInfo)
        {
            _hallTableRepository.AddHallTable(hallTableInfo);
        }
    }
}
