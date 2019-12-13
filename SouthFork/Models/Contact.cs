using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SouthFork.Models
{
    [Table("Contact")]
    public class Contact
    {
        [Key]
        public int PotentialID { get; set; }

        [DisplayName("Full Name")]
        public string FullName { get; set; }

        public string Email { get; set; }

        public string Notes { get; set; }
    }
}