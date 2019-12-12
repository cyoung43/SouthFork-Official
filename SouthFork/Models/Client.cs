using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SouthFork.Models
{
    [Table("Client")]
    public class Client
    {
        [Key]
        public int ClientID { get; set; }

        [Required]
        [DisplayName("First Name")]
        [StringLength(25, MinimumLength = 2)]
        public string FirstName { get; set; }

        [Required]
        [DisplayName("Last Name")]
        [StringLength(25, MinimumLength = 2)]
        public string LastName { get; set; }

        [Required]
        [DisplayName("Email")]
        [StringLength(50, MinimumLength = 5)]
        [EmailAddress]
        public string Email { get; set; }

        [DisplayName("Password")]
        [StringLength(15)]
        public string Password { get; set; }

        //[Required]
        [DisplayName("Phone")]
        [StringLength(15)]
        [Phone]
        public string Phone { get; set; }

        //[Required]
        [DisplayName("Address")]
        [StringLength(50)]
        public string Address { get; set; }

        //[Required]
        [DisplayName("City")]
        [StringLength(20)]
        public string City { get; set; }

        //[Required]
        [DisplayName("State")]
        [StringLength(2, MinimumLength = 2, ErrorMessage = "Please enter state abbreviation")]
        public string State { get; set; }

        //[Required]
        [DisplayName("Zip")]
        [StringLength(5)]
        public string Zip { get; set; }

        [DisplayName("Notes")]
        [StringLength(100)]
        public string Notes { get; set; }
    }
}