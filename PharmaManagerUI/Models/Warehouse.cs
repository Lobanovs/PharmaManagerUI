using System.ComponentModel.DataAnnotations.Schema;

namespace PharmaManagerUI.Models
{
    [Table("Warehouse")]
    public class Warehouse
    {
        [Column("warehouse_id")]
        public int Id { get; set; }

        [Column("location")]
        public string Location { get; set; }

        [Column("capacity")]
        public int Capacity { get; set; }
    }
}