using RestaurantDAL.Repost;
using RestaurantEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantBLL.Services
{
    public class CookService
    {
        ICookRepost _CookRepository;


        //Unable to resolve ====>>>> Object issues
        public CookService(ICookRepost CookRepository)
        {
            _CookRepository = CookRepository;
        }


        //Update Cook
        public void UpdateCook(Cook cook)
        {
            _CookRepository.UpdateCook(cook);
        }

        //Delete Cook
        public void DeleteCook(int cookId)
        {
            _CookRepository.DeleteCook(cookId);
        }

        //Get CookByID
        public Cook GetCookById(int cookId)
        {
            return _CookRepository.GetCookById(cookId);
        }

        //Get Cooks
        public IEnumerable<Cook> GetCook()
        {
            return _CookRepository.GetCooks();
        }

        //Get Cooks By Speciality
        public IEnumerable<Cook> GetCookBySpeciality(string speciality)
        {
            return _CookRepository.GetCooksBySpeciality(speciality);
        }

        //Registering Cook
        public void AddCook(Cook cookInfo)
        {
            _CookRepository.AddCook(cookInfo);
        }
    }
}
