using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace AssetMG.Models
{
    public class Asset_Mvmt_Type
    {
        [Key] public int AMId { get; set; }
        [Required] public string Asset_Mvmt_Type_Name { get; set;}
        [Required] public string MDescription { get; set; }
    }
}
