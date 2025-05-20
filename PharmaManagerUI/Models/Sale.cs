using System.ComponentModel.DataAnnotations.Schema;

namespace PharmaManagerUI.Models
{
    [Table("Sales")]
    public class Sale
    {
        [Column("sale_id")]
        public int Id { get; set; }

        [Column("pharmacy_id")]
        public int PharmacyId { get; set; }

        [Column("drug_id")]
        public int DrugId { get; set; }

        [Column("quantity")]
        public int Quantity { get; set; }

        [Column("sale_date")]
        public DateTime SaleDate { get; set; }

        [Column("total_price")]
        public decimal TotalPrice { get; set; }

        public PharmacyNetwork Pharmacy { get; set; }
        public Drug Drug { get; set; }
    }
}