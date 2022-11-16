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

        public string UserName { get; set; }

        public string UserEmail { get; set; }

        public DateTime BillDate { get; set; }




        [ForeignKey("Order")]
        public List<Order> OrderId { get; set; }

        public Order Order { get; set; }

       
    }
}
