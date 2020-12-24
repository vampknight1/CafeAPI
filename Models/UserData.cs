using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CafeAPI.Models
{
    public class UserData
    {
        private string token;
        public string Username { get; set; }
        public string Password { get; set; }
        public void SetToken ( string _t)
        {
            token = _t;
        }
        public string Token
        { get => token;
          private set => token = value;
        }
    }

    public class Kunci
    {
        public const string Aman = "Oke09inirahasia55663";
    }
}
