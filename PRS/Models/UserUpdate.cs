using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PRS.Models
{
    public class UserUpdate
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public bool IsReviewer { get; set; }
        public bool IsAdmin { get; set; }
        public bool Active { get; set; }
        public bool PasswordNeedsChanged { get; set; }
    }

    public class UserCreate : UserUpdate
    {
        public string Password { get; set; }
    }
}