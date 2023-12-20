using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace AssetMG.Models
{
    public class Asset_Location
    {
        [Key] public int ALId { get; set; }
        public string ALName { get; set; }
    }
}
