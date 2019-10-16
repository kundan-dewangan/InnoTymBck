using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InnoTymBck.Api.Models
{
    public class PerformTransactionModels
    {
        public int userId { get; set; }
        public decimal userAmount { get; set; }
        public int loginUser { get; set; }
    }
}