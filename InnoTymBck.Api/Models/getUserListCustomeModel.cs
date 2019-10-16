using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InnoTymBck.Api.Models
{

    public class getUserListCustome
    {
        public string emailId { get; set; }
        public string password { get; set; }

    }

    public class UserReturnModelLogin
    {
        public int Id { get; set; }
        public string UName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Gender { get; set; }
        public string PasswordHash { get; set; }
        public decimal Amount { get; set; }
        public string ImageUrl { get; set; }
        public System.DateTime CreateDate { get; set; }
        public bool IsAdmin { get; set; }

    }

   
}