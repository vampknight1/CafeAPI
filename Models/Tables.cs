using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CafeAPI.Models
{
    public class Tables : BaseEntity
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Table Number")]
        public string TableNo { get; set; }

        [Required(ErrorMessage ="*")]
        [Display(Name = "Table Code")]
        public string TableCode { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Notes")]
        public string Note { get; set; }

        [Display(Name = "Create Date")]        
        public DateTime CreatedAt { get; set; }

        [Display(Name = "Update Date")]
        public DateTime UpdatedAt { get; set; }
    }
}
