using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace AssetMG.Models
{
    public class Asset_Mvmt
    {
        [Key]
        public int MId { get; set; }
        [Required]
        public string MReasons { get; set; }

        public DateTime Date_mmvmt { get; set; }

        [ForeignKey("Assets")]
         public int AssetId { get; set; }
        public Assets Assets { get; set; }

        [ForeignKey("Movement_Type")]
        public int AMId { get; set; }
        public Asset_Mvmt_Type Type { get; set; }
        [ForeignKey("users")]
        public int Uid { get; set; }
        public Users Users { get; set; }
    }
}
