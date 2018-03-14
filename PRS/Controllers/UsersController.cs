using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

using PRS.Models;
using PRS.Utility;

namespace PRS.Controllers
{
    public class UsersController : BaseCtrl
    {

        public ActionResult List()
        {
            return Success(db.Users.ToList());
        }

        public ActionResult Get(int? ID)
        {
            if (ID == null)
            {
                return Failure("User ID is null.");
            }

            User User = db.Users.Find(ID);

            if (User == null)
            {
                return Failure("User ID does not exist.");
            }

            return Success(User);
        }

        public ActionResult ResetPassword([FromBody] string UserName, string NewPassword)
        {
            if(LoginManager.ResetPassword(db, UserName, NewPassword))
            {
                return Success("Password changed succesfully for " + UserName);
            }

            return Failure("Unable to change the password.");
        }

        public ActionResult Update(UserUpdate user)
        {

            User oldUser = db.Users.Find(user.Id);

            if (oldUser == null)
            {
                return Failure("Unable to find this User ID");
            }

            oldUser.IsAdmin = user.IsAdmin;
            oldUser.IsReviewer = user.IsReviewer;
            oldUser.LastName = user.LastName;
            oldUser.FirstName = user.FirstName;
            oldUser.Active = user.Active;
            oldUser.Email= user.Email;
            oldUser.UserName = user.UserName;
            oldUser.Phone = user.Phone;
            oldUser.PasswordNeedsChanged = user.PasswordNeedsChanged;
            oldUser.UpdateTime();

            if (!Save())
            {
                return BadSaveResult();
            }
            return Success("Succesfully changed User ID " + user.Id);
        }

        public ActionResult Remove(User User)
        {
            User existingUser = db.Users.Find(User.Id);
            if (existingUser == null)
            {
                return Failure("Unable to locate User ID.");
            }

            db.Users.Remove(existingUser);

            if (!Save())
            {
                return BadSaveResult();
            }
            return Success("Removed User ID " + User.Id);
        }

        public ActionResult Create(UserCreate createdUser)
        {
            User user = new User();
            user.DateCreated = DateTime.UtcNow;
            user.UpdateTime();

            user.IsAdmin = createdUser.IsAdmin;
            user.IsReviewer = createdUser.IsReviewer;
            user.LastName = createdUser.LastName;
            user.FirstName = createdUser.FirstName;
            user.Active = createdUser.Active;
            user.Email = createdUser.Email;
            user.UserName = createdUser.UserName;
            user.Phone = createdUser.Phone;

            user = LoginManager.CreateUserHash(user, createdUser.Password);

            User newUser = db.Users.Add(user);

            if (newUser == null)
            {
                return Failure("Unable to create new User.");
            }

            if (!Save())
            {
                return BadSaveResult();
            }
            return Success("New User created: ID " + newUser.Id);
        }
        
    }
}