using InnoTymBck.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InnoTymBck.Api.Models
{
    public class TransListModels
    {
        public int TransId { get; set; }
        public string UName { get; set; }
        public string NameRef { get; set; }
        public string TransType { get; set; }
        public decimal InisialAmount { get; set; }
        public decimal TransAmount { get; set; }
        public System.DateTime TransDate { get; set; }
        public virtual User User { get; set; }
        public virtual User User1 { get; set; }
    }
}