using System.ComponentModel.DataAnnotations.Schema;

namespace PharmaManagerUI.Models
{
    [Table("Users")]
    public class User
    {
        [Column("user_id")]
        public int Id { get; set; }

        [Column("login")]
        public string Login { get; set; }

        [Column("password_hash")]
        public byte[] PasswordHash { get; set; }

        [Column("salt")]
        public byte[] Salt { get; set; }

        [Column("role")]
        public string Role { get; set; }
    }
}