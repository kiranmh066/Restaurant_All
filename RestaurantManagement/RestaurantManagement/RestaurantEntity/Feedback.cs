using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RestaurantEntity
{
    public class Feedback
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FeedbackId { get; set; }

        [ForeignKey("HallTable")]
        public int HallTableId { get; set; }

        public HallTable HallTable { get; set; }

        public int Rating { get; set; }

        public string Comments { get; set; }

        public bool FeedbackStatus { get; set; }

    }
}
