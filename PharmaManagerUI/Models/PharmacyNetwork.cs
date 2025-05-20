using System.ComponentModel.DataAnnotations.Schema;

namespace PharmaManagerUI.Models
{
    [Table("Pharmacy_Network")]
    public class PharmacyNetwork
    {
        [Column("pharmacy_id")]
        public int Id { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("address")]
        public string Address { get; set; }

        [Column("contact_info")]
        public string ContactInfo { get; set; }
    }
}