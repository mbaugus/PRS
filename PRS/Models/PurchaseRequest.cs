using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PRS.Models
{
    [Table("PurchaseRequest")]
    public class PurchaseRequest
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [StringLength(100)]
        public string Description { get; set; }

        [StringLength(255)]
        [Required]
        public string Justification { get; set; }

        public DateTime DateNeeded { get; set; }

        [Required]
        [StringLength(25)]
        public string DeliveryMode { get; set; }

        [Required]
        [StringLength(15)]
        public string Status { get; set; }

        private decimal _Total;
        [Required]
        public decimal Total { get { return _Total; } set { if (value < 0) { _Total = 0; } else { _Total = value; } } }

        [Required]
        public bool Active { get; set; }

        [StringLength(100)]
        public string ReasonForRejection { get; set; }

        [Required]
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }

        [ForeignKey("UpdatedLastBy")]
        public int? UpdateLastUserID { get; set; }

        [JsonIgnore]
        public virtual User UpdatedLastBy { get; set; }

        public virtual ICollection<PurchaseRequestLineItem> PurchaseRequestLineItems { get; set; }

        // default constructor
        public PurchaseRequest()
        {
            Active = true;
        }

        public PurchaseRequest Copy(PurchaseRequest newData)
        {
            this.Active = newData.Active;
            this.DateNeeded = newData.DateNeeded;
            this.DeliveryMode = newData.DeliveryMode;
            this.Description = newData.Description;
            this.Justification = newData.Justification;
            this.ReasonForRejection = newData.ReasonForRejection;
            this.Status = newData.Status;
            this.Total = newData.Total;

            this.UserId = newData.UserId;

            if (newData.DateCreated != null)
            {
                DateCreated = newData.DateCreated;
            }
            if (newData.DateUpdated != null)
            {
                DateUpdated = newData.DateUpdated;
            }
            UpdateLastUserID = newData.UpdateLastUserID;
            return this;
        }
        public void UpdateTime()
        {
            this.DateUpdated = DateTime.UtcNow;
        }
    }

}
