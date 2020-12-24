using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CafeAPI.Models
{
    public class MenusVM
    {
        public Menus Menus { get; set; }
        [Key]
        public int ID { get; set; }
        [Display(Name = "Menu Name")]
        public string MenuName { get; set; }
        public int Category { get; set; }
        [Display(Name = "Menu Price")]
        public int MenuPrice { get; set; }
        [Display(Name = "Stock")]
        public int MenuStock { get; set; }
        [Display(Name = "Menu Desc.")]
        public string MenuDesc { get; set; }
        [Display(Name = "Menu Image")]
        //public string MenuImg { get; set; }
        public IFormFile MenuImgVM { get; set; }

        [Display(Name = "Menu type")]
        public string MenuType { get; set; }

        [Display(Name = "Create Date")]
        public DateTime created_at { get; set; }

        [Display(Name = "Update Date")]
        public DateTime updated_at { get; set; }
        [Display(Name = "Category Name")]
        public virtual string CategoryName { get; set; }
    }
}
