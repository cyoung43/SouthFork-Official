using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SouthFork.Models
{
    [Table("Position")]
    public class Position
    {
        [Key]
        public int PositionID { get; set; }

        [Required]
        [DisplayName("Title")]
        [StringLength(15)]
        public string PositionName { get; set; }
    }
}