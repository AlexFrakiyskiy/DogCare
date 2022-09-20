using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace DogCare.Models
{
    public class LoginModel
    {
        protected string _password = string.Empty;
        public string UserName { set; get; }
        public string Password
        {
            set { _password = encryptpass(value); }
            get { return _password; }
         }

        private string encryptpass(string password)
        {
            string msg = "";
            byte[] encode = new byte[password.Length];
            encode = Encoding.UTF8.GetBytes(password);
            msg = Convert.ToBase64String(encode);
            return msg;
        }
    }
}