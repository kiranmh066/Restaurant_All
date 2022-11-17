using RestaurantEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantDAL.Repost
{
    public interface ICookRepost
    {
        void AddCook(Cook cook);
        void UpdateCook(Cook cook);

        void DeleteCook(int cookId);

        Cook GetCookById(int cookId);

        IEnumerable<Cook> GetCooks();

        IEnumerable<Cook> GetCooksBySpeciality(string speciality);        
    }
}
