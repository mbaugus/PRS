using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PRS.Models
{
    [Table("User")]
    public class User
    {
        [Key]
        public int Id { get; set; }
        [StringLength(20)]
        [Required]
        [Index("UserNameIndex", IsUnique = true)]
        [Index(IsUnique = true)]
        public string UserName { get; set; }
        [StringLength(100)]
        [Required]
        public string Password { get; set; }
        [StringLength(20)]
        [Required]
        public string FirstName { get; set; }
        [StringLength(20)]
        [Required]
        public string LastName { get; set; }
        [StringLength(12)]
        public string Phone { get; set; }
        [StringLength(100)]
        [Required]
        public string Email { get; set; }
        [Required]
        public bool IsReviewer { get; set; }
        [Required]
        public bool IsAdmin { get; set; }
        [Required]
        public bool Active { get; set; }

        [Required]
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }

        [ForeignKey("UpdatedLastBy")]
        public int? UpdateLastUserID { get; set; }

        [JsonIgnore]
        public virtual User UpdatedLastBy { get; set; }

        public User Copy(User newData)
        {
            this.Active = newData.Active;
            this.DateCreated = newData.DateCreated;
            this.DateUpdated = newData.DateUpdated;
            this.Email = newData.Email;
            this.FirstName = newData.FirstName;
            this.IsAdmin = newData.IsAdmin;
            this.IsReviewer = newData.IsReviewer;
            this.LastName = newData.LastName;
            this.Password = newData.Password;
            this.Phone = newData.Phone;
            this.UserName = newData.UserName;
            return this;
        }

        public void UpdateTime()
        {
            this.DateUpdated = DateTime.UtcNow;
        }
    }
}