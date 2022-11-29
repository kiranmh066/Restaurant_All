using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RestaurantEntity
{
    public class Help
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int HelpId { get; set; }

        [ForeignKey("HallTable")]
        public int HallTableId { get; set; }

        public HallTable HallTable { get; set; }
        public string UserName { get; set; }

        public string UserEmail { get; set; }


        public string issue { get; set; }


    }
}
