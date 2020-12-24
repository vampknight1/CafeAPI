using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CafeAPI.Models
{
    public class Customers : BaseEntity
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage ="*")]
        [Display(Name ="Table Code")]
        public string TableCode { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name ="Customer Name")]
        public string CustomerName { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Customer Address")]
        public string CustomerAddress { get; set; }

        [Display(Name = "City")]
        public int? City { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Customer Phone")]
        public string CustomerPhone { get; set; }

        [Display(Name = "Create Date")]        
        public DateTime CreatedAt { get; set; }

        [Display(Name = "Update Date")]
        public DateTime UpdatedAt { get; set; }
       
        public virtual IEnumerable<SelectListItem> ListArea { get; set; }
        public virtual string CityName { get; set; }
    }
}
