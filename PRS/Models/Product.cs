using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PRS.Models
{
    [Table("Product")]
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int VendorID { get; set; }

        public virtual Vendor Vendor { get; set; }

        [Required]
        [StringLength(50)]
        public string PartNumber { get; set; }

        [Required]
        [StringLength(150)]
        public string Name { get; set; }

        private decimal _Price;
        [Required]
        public decimal Price
        {
            get
            {
                return _Price;
            }
            set
            {
                if(value < 0)
                {
                    _Price = 0;
                }
                else
                {
                    _Price = value;
                }
            }
        }

        [Required]
        [StringLength(255)]
        public string Unit { get; set; }

        [StringLength(255)]
        public string PhotoPath { get; set; }

        [Required]
        public bool Active { get; set; }

        [Required]
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }

        [ForeignKey("UpdatedLastBy")]
        public int? UpdateLastUserID { get; set; }

        public virtual User UpdatedLastBy { get; set; }

        public Product Copy(Product newData)
        {
            this.Active = newData.Active;
            this.DateCreated = newData.DateCreated;
            this.DateUpdated = newData.DateUpdated;
            this.Name = newData.Name;
            this.PartNumber = newData.PartNumber;
            this.PhotoPath = newData.PhotoPath;
            this.Price = newData.Price;
            this.Unit = newData.Unit;
            this.UpdateLastUserID = newData.UpdateLastUserID;
            this.VendorID = newData.VendorID;
            return this;
        }
        public void UpdateTime()
        {
            this.DateUpdated = DateTime.UtcNow;
        }
    }
}