using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace AssetMG.Models
{
    public class Department
    {
        [Key] 
        public int DId { get; set; }
        [Required] 
        public string DName { get; set; }
    }
}
