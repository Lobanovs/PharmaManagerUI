using System.ComponentModel.DataAnnotations.Schema;

namespace PharmaManagerUI.Models
{
    [Table("Production_Orders")]
    public class ProductionOrder
    {
        [Column("order_id")]
        public int Id { get; set; }

        [Column("client_id")]
        public int? ClientId { get; set; }

        [Column("order_date")]
        public DateTime? OrderDate { get; set; }

        [Column("delivery_date")]
        public DateTime? DeliveryDate { get; set; }

        [Column("status")]
        public string Status { get; set; }

        public Client Client { get; set; }
    }
}