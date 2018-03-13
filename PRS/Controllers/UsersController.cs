using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using PRS.Models;

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

        public ActionResult Update(User User)
        {
            User oldUser = db.Users.Find(User.Id);

            if (User == null)
            {
                return Failure("Unable to find this User ID");
            }

            oldUser.Copy(User);
            oldUser.UpdateTime();

            if (!Save())
            {
                return BadSaveResult();
            }
            return Success("Succesfully changed User ID " + User.Id);
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
        public ActionResult Create(User user)
        {
            user.DateCreated = DateTime.UtcNow;
            user.UpdateTime();

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