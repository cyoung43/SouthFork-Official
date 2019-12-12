using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SouthFork.Models
{
    [Table("Project")]
    public class Project
    {
        [Key]
        public int ProjectID { get; set; }

        [Required(ErrorMessage = "Please enter a project name")]
        [StringLength(100, MinimumLength = 5)]
        [DisplayName("Project Name")]
        public string ProjectName { get; set; }

        [Required]
        [Range(1, 100)]
        [DisplayName("Budgeted Hours")]
        public int BudgetedHours { get; set; }

        [Range(1, 200)]
        [DisplayName("Actual Project Hours")]
        public int? ActualHours { get; set; }

        [Required]
        [Range(1, 20)]
        [DisplayName("Budgeted Days")]
        public int BudgetedDays { get; set; }

        [Range(1, 40)]
        [DisplayName("Actual Project Days")]
        public int? ActualDays { get; set; }

        [Required(ErrorMessage = "Please enter a deposit date")]
        [DataType("Date")]
        [DisplayName("Deposit Date")]
        public DateTime DepositDate { get; set; }

        [Required(ErrorMessage = "Please enter a begin date")]
        [DataType("Date")]
        [DisplayName("Begin Date")]
        public DateTime BeginDate { get; set; }

        [DataType("Date")]
        [DisplayName("Completed Date")]
        public DateTime? CompleteDate { get; set; }

        [DataType("Date")]
        [DisplayName("Date Paid")]
        public DateTime? PayDate { get; set; }

        [DataType("Date")]
        [DisplayName("Date Delivered")]
        public DateTime? DeliverDate { get; set; }

        [Range(1, 10000)]
        [DisplayName("Project Price")]
        public int? ProjectPrice { get; set; }

        [Required]
        [Range(1, 5000)]
        [DisplayName("Bid Price")]
        public int BidPrice { get; set; }

        [Required(ErrorMessage = "Please indicate with 'true' or 'false'")]
        [DisplayName("Client Paid?")]
        [StringLength(5, MinimumLength = 4, ErrorMessage = "Please indicate with @'true' or @'false'")]
        public string ClientPaid { get; set; }

        [Required]
        [DisplayName("Square Footage")]
        [Range(0, 2000000)]
        public int SquareFootage { get; set; }

        [Required(ErrorMessage = "Please select a client")]
        [DisplayName("Client")]
        public int ClientID { get; set; }

        [DisplayName("Builder")]
        public int? BuilderID { get; set; }

        [Required(ErrorMessage = "Please select an employee")]
        [DisplayName("Employee")]
        public int EmployeeID { get; set; }
    }
}