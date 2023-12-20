using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace AssetMG.Models
{
    public class Assets
    {
        [Key] 
        public int Id { get; set; }
        [Required] 
        public string Aname { get;}
        [Required] 
        public int Quantity { get; set; }
        public int Cost { get; set; }
        public int Shelve {  get; set; }
        public string ImagePath {  get; set; }
        public int Locker { get; set; }
        [Required]
        public DateTime Date { get; set; }

        // Foreign key property
        [ForeignKey("Asset_Type")]
        public int AssetTypeId { get; set; }
        // Navigation property to the Asset_Type
        public Asset_Type AssetType { get; set; }

        // Foreign key property
        [ForeignKey("CreateByUsers")]
        public int CreateByUserId { get; set; }
        // Navigation property to the Asset_Type
        public Users CreatedByUser { get; set; }

        [ForeignKey("Department")]
        public int DId { get; set; }
        public Department Department { get; set; }

        [ForeignKey("Location")]
        public int LocationId { get; set; }
        public Asset_Location Location { get; set; }





    }
}
