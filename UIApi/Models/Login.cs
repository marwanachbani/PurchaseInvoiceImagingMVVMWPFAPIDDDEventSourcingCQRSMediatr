using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIApi.Models
{
    public class Login
    {
        public Login(string username, string pasword)
        {
            userName = username;
            password = pasword;
        }

        public string userName { get; set; }
        public string password { get; set; }
    }
}
