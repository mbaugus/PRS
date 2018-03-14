using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

using PRS.Models;

namespace PRS.Utility
{
    public static class LoginManager
    {
        public static User CreateUserHash(User user,string password)
        {
            PasswordHash newpw = new PasswordHash(password);
            user.Password = Convert.ToBase64String(newpw.Hash);
            user.Salt = Convert.ToBase64String(newpw.Salt);
            return user;
        }

        public static bool CheckPassword(AppDbContext ctx, string username, string password)
        {
            User User = ctx.Users.First(e => e.UserName == username);

            if(User == null)
            {
                return false;
            }

            string Salt = User.Salt;
            string RealPassword = User.Password;

            byte[] hashpw = Convert.FromBase64String(RealPassword);
            byte[] salt = Convert.FromBase64String(Salt);

            PasswordHash thehash = new PasswordHash(salt, hashpw);

            if (thehash.Verify(password))
            {
                return true;
            }
            return false;
        }
        // this actually verifys password
        public static bool ChangePassword(AppDbContext ctx, string user, string oldpassword, string password)
        {
            try
            {
                User User = ctx.Users.Single(m => m.UserName == user);

                if (User == null) { return false; }
                if (!CheckPassword(ctx, user, oldpassword))
                {
                    return false;
                }
                PasswordHash newpw = new PasswordHash(password);
                string newHash = Convert.ToBase64String(newpw.Hash);
                string newSalt = Convert.ToBase64String(newpw.Salt);
                User.Password = newHash;
                User.Salt = newSalt;
                ctx.SaveChanges();
            }
            catch(Exception e)
            {
                return false;
            }
            return true;
        }

        public static bool ResetPassword(AppDbContext ctx, string user, string newpassword = "password123")
        {
            try
            {
                User User = ctx.Users.Single(m => m.UserName == user);

                if (User == null) { return false; }

                PasswordHash newpw = new PasswordHash(newpassword);
                string newHash = Convert.ToBase64String(newpw.Hash);
                string newSalt = Convert.ToBase64String(newpw.Salt);
                User.Password = newHash;
                User.Salt = newSalt;
                User.PasswordNeedsChanged = true;
                ctx.SaveChanges();
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }
    }
}