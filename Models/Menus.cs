using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CafeAPI.Models
{
    public class Menus : BaseEntity
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Menu Name")]
        public string MenuName { get; set; }

        [Required(ErrorMessage = "*")]
        public int? Category { get; set; }

        [DisplayFormat(DataFormatString = "{0:#,##}", ApplyFormatInEditMode = false)]
        [Required(ErrorMessage = "*")]
        [Display(Name = "Menu Price")]
        public int MenuPrice { get; set; }

        [DisplayFormat(DataFormatString = "{0:#,##}", ApplyFormatInEditMode = false)]
        [Required(ErrorMessage = "*")]
        [Display(Name = "Stock")]
        public int MenuStock { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Menu Desc.")]
        public string MenuDesc { get; set; }
       
        [Display(Name = "Menu Image")]       
        public string MenuImg { get; set; }

        [Display(Name = "Menu type")]
        public string MenuType { get; set; }

        [Display(Name = "Create Date")]
        public DateTime created_at { get; set; }

        [Display(Name ="Update Date")]
        public DateTime updated_at { get; set; }

        [Display(Name = "Category Name")]
        public virtual string CategoryName { get; set; }
    }
}
