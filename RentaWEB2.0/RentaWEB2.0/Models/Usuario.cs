using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RentaWEB2._0.Models
{
    public class Usuario
    {
        private String username = "Admin";
        private String password = "muniValpo";

        public string Username { get => username; set => username = value; }
        public string Password { get => password; set => password = value; }
    }
}