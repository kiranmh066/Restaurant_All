using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RestaurantEntity
{
    public class Admin
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AdminId { get; set; }

        public string AdminName { get; set; }

        public string AdminEmail { get; set; }

        public string AdminPassword { get; set; }


    }
}
