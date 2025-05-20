using System.ComponentModel.DataAnnotations.Schema;

namespace PharmaManagerUI.Models
{
    [Table("Equipment")]
    public class Equipment
    {
        [Column("equipment_id")]
        public int Id { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("type")]
        public string Type { get; set; }

        [Column("status")]
        public string Status { get; set; }
    }
}