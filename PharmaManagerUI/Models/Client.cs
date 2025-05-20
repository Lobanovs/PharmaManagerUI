using System.ComponentModel.DataAnnotations.Schema;

namespace PharmaManagerUI.Models
{
    [Table("Clients")]
    public class Client
    {
        [Column("client_id")]
        public int Id { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("address")]
        public string Address { get; set; }

        [Column("contact_info")]
        public string ContactInfo { get; set; }
    }
}