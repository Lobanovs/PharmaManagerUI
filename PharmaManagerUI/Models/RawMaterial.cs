using System.ComponentModel.DataAnnotations.Schema;

namespace PharmaManagerUI.Models
{
    [Table("Raw_Materials")]
    public class RawMaterial
    {
        [Column("material_id")]
        public int Id { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("supplier")]
        public string Supplier { get; set; }

        [Column("quantity_in_stock")]
        public int QuantityInStock { get; set; }

        [Column("unit")]
        public string Unit { get; set; }
    }
}