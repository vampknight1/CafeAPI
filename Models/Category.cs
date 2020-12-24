using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CafeAPI.Models
{
    public class Category : BaseEntity
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage ="*")]
        [Display(Name = "Category Name")]
        public string CategoryName { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Category Desc.")]
        public string CategoryDesc { get; set; }

        [Display(Name = "Create Date")]        
        public DateTime CreatedAt { get; set; }

        [Display(Name = "Update Date")]
        public DateTime UpdatedAt { get; set; }
    }
}
