using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace AssetMG.Models
{

    public class Users
    {
        [Key] public int Uid { get; set; }
        [Required] public string FirstName { get; set; }
        [Required] public string LastName { get; set; }
       
        [Required] public string Position { get; set; }
        [Required] public string Username { get; set; }
        [Required] public string Password { get; set; }
        [ForeignKey("Department")]
        public int DId { get; set; }
        public Department Department { get; set; }
    }
}
