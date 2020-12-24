using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CafeAPI.Models
{
    public class EventCalendar : BaseEntity
    {
        [Key]
        public int ID { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public System.DateTime StartEvent { get; set; }
        public Nullable<System.DateTime> EndEvent { get; set; }
        public string ThemeColor { get; set; }
        public bool IsFullDay { get; set; }
    }
}
