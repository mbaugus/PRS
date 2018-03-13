using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PRS.Models
{
    [Table("PurchaseRequestLineItem")]
    public class PurchaseRequestLineItem
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int PurchaseRequestID { get; set; }
        [Required]
        public int ProductID { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public bool Active { get; set; }
       
        [Required]
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }

        [ForeignKey("UpdatedLastBy")]
        public int? UpdateLastUserID { get; set; }

        [JsonIgnore]
        public virtual User UpdatedLastBy { get; set; }
        [JsonIgnore]
        public virtual PurchaseRequest PurchaseRequest { get; set; }
        [JsonIgnore]
        public virtual Product Product { get; set; }


        public PurchaseRequestLineItem Copy(PurchaseRequestLineItem newData)
        {
            ProductID = newData.ProductID;
            Quantity = newData.Quantity;
            Active = newData.Active;
            DateCreated = newData.DateCreated;
            DateUpdated = newData.DateUpdated;
            return this;
        }

        public void UpdateTime()
        {
            this.DateUpdated = DateTime.UtcNow;
        }
    }
}