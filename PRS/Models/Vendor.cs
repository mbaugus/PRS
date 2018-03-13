using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PRS.Models
{
    [Table("Vendor")]
    public class Vendor
    {
        [Key]
        public int Id { get; set; }

        [StringLength(10)]
        [Required]
        [Index("VendorCodeIndex", IsUnique = true)]
        public string Code { get; set; }

        [StringLength(255)]
        [Required]
        public string Name { get; set; }

        [StringLength(255)]
        [Required]
        public string Address { get; set; }

        [StringLength(255)]
        [Required]
        public string City { get; set; }

        [StringLength(2)]
        [MinLength(2)]
        [Required]
        public string State { get; set; }

        [StringLength(5)]
        [Required]
        public string PostalCode { get; set; }

        [StringLength(100)]
        [Required]
        public string Email { get; set; }

        public bool IsPreApproved { get; set; }

        [Required]
        public bool Active { get; set; }

        [Required]
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }

        [ForeignKey("UpdatedLastBy")]
        public int? UpdateLastUserID { get; set; }

        [JsonIgnore]
        public virtual User UpdatedLastBy { get; set; }

        public Vendor Copy(Vendor newData)
        {
            this.Active = newData.Active;
            this.Address = newData.Address;
            this.City = newData.City;
            this.Code = newData.Code;
            this.DateCreated = newData.DateCreated;
            this.DateUpdated = newData.DateUpdated;
            this.Email = newData.Email;
            this.IsPreApproved = newData.IsPreApproved;
            this.Name = newData.Name;
            this.PostalCode = newData.PostalCode;
            this.State = newData.State;
            return this;
        }
        public void UpdateTime()
        {
            this.DateUpdated = DateTime.UtcNow;
        }
    }
}