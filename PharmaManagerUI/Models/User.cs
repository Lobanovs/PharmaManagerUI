using System.ComponentModel.DataAnnotations;

namespace PharmaManagerUI.Models
{
    public class User
    {
        public int UserId { get; set; }
        [Required]
        public string Login { get; set; }
        [Required]
        public byte[] PasswordHash { get; set; }
        [Required]
        public byte[] Salt { get; set; }
        [Required]
        public string Role { get; set; }
    }
}