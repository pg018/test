using System.ComponentModel.DataAnnotations;

namespace ConsoleApp2.Core.DTO.TabTwo.ProfileInfo
{
    public class ProfileInfoItemTwoDTO
    {
        [Required(ErrorMessage = "Mandatory Property is Missing")]
        public bool Mandatory { get; set; }
        [Required(ErrorMessage = "Show Property is Missing")]
        public bool Show { get; set; }
    }
}
