using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace AssetMG.Models
{
    public class Asset_Type
    {
        [Key] public int ATId { get; set; }
        [Required] public string ATName { get; set; }
    }
}
