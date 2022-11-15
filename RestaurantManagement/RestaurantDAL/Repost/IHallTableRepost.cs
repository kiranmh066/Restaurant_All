using RestaurantEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantDAL.Repost
{
    public interface IHallTableRepost
    {
        void AddHallTable(HallTable hallTable);
        void UpdateHallTable(HallTable hallTable);

        void DeleteHallTable(int hallTableId);

        HallTable GetHallTableById(int hallTableId);

        IEnumerable<HallTable> GetHallTables();
    }
}
