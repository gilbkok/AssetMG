using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AssetMG.Models
{
    public class Asset_Operations
    {
        [Key] public int Operations_Id { get; set; }
        [Required] public DateTime Operations_Date {get; set;}

         public int Quantity { get; set;}

        public int Operations_Type { get; set;}

      
        [ForeignKey("Assets")]
        public int AssetId { get; set; }
        public Assets Assets { get; set; }

        [ForeignKey("users")]
        public int Uid { get; set; }
        public Users Users { get; set; }
    }
}
