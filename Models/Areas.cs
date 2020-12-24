using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using System.ComponentModel.DataAnnotations.Schema;

namespace CafeAPI.Models
{
    public class Area : BaseEntity
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "*")]
        public string Code { get; set; }

        [Required(ErrorMessage = "*")]
        public string Name { get; set; }

        [Display(Name = "Create Date")]
        public DateTime CreatedAt { get; set; }

        [Display(Name ="Update Date")]
        public DateTime UpdatedAt { get; set; }

        //[NotMapped]
        //public SelectList ListArea { get; set; }

        //public void PopulateArea()
        //{
        //    using (IDbConnection dbConnection = Connection)
        //    {
        //        dbConnection.Open();
        //        var listArea = Connection.Query<Area>(@"SELECT id, name FROM Area");
        //        ListArea = new SelectList(listArea, "id", "name");
        //    }
        //}
    }   
}