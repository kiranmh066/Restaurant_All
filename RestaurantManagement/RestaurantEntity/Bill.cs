using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RestaurantEntity
{
    public class Bill
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BillId { get; set; }

        [ForeignKey("HallTable")]
        public int HallTableId { get; set; }

        public HallTable HallTable { get; set; }

        public bool BillStatus { get; set; }


        [ForeignKey("Order")]
        public List<Order> OrderId { get; set; }

        public Order Order { get; set; }

       
    }
}
