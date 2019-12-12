using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SouthFork.Models
{
    [Table("Builder")]
    public class Builder
    {
        [Key]
        public int BuilderID { get; set; }

        [Required]
        [DisplayName("Company Name")]
        [StringLength(30)]
        public string CompanyName { get; set; }
    }
}