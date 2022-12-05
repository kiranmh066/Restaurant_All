using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RestaurantEntity
{
    public class Food
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FoodId { get; set; }

        public string FoodName { get; set; }

        public string FoodType { get; set; }

        public double FoodCost { get; set; }

        public string FoodCuisine { get; set; }

        public byte[] FoodImage { get; set; }

    }
}
