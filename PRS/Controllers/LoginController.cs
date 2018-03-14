using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using PRS.Models;
using PRS.Utility;

namespace PRS.Controllers
{
    public class LoginController : BaseCtrl
    {
        //
        // GET: /Account/Login
       
        public ActionResult Index([FromBody] string Username, string Password)
        {
            bool success = LoginManager.CheckPassword(db, Username, Password);
            if (!success)
            {
                return Failure("Bad username/password");
            }
            return Success("Logged in succesfully");
        }

        public ActionResult Logout()
        {
            return Success("Logged out");
        }

    }
}
