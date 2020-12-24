using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CafeAPI.Models
{
    public class KlhkSents : BaseEntity
    {
        [Key]
        public string ID { get; set; }

        public string TableCode { get; set; }

        //[Required]
        public int DateTime { get; set; }

        public int pH { get; set; }

        //[Required]
        public int COD { get; set; }

        public int TSS { get; set; }
        //[Required]

        public int NH3 { get; set; }

        public int Debit { get; set; }
    }
}
