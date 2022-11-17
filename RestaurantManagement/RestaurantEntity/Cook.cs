using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RestaurantEntity
{
    public class Cook
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CookId { get; set; }

        [ForeignKey("Employee")]
        public int EmpId { get; set; }

        public Employee Employee { get; set; }

        public int Quantity { get; set; }

        [ForeignKey("Food")]
        public int FoodId { get; set; }

        public Food Food { get; set; }
        
        public string Speciality { get; set; }


        public bool CookStatus { get; set; }
    }
}
