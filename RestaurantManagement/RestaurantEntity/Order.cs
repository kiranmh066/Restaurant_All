using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RestaurantEntity
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderId { get; set; }


        [ForeignKey("Food")]
        public int FoodId { get; set; }

        public Food Food { get; set; }

        [ForeignKey("HallTable")]
        public int HallTableId { get; set; }

        public HallTable HallTable { get; set; }

        public int Quantity { get; set; }

        public int OrderTotal { get; set; }

        public bool OrderStatus { get; set; }

        public DateTime OrderDate { get; set; }
       
    }
}
