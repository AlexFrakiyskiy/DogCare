using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DogCare.Models
{
    public class DetailsModel
    {
        public DetailsModel()
        {
            //CreationDate = DateTime.Now.ToString("s");
        }
        public int Id { set; get; }
        public int CustomerId { set; get; }
        public string UserName { set; get; }
        public string FirstName { set; get; }
        public string AppointDate { set; get; }
        public string CreationDate { set; get; }
    }
}