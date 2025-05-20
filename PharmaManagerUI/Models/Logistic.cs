using System.ComponentModel.DataAnnotations.Schema;

namespace PharmaManagerUI.Models
{
    [Table("Logistics")]
    public class Logistic
    {
        [Column("logistics_id")]
        public int Id { get; set; }

        [Column("order_id")]
        public int? OrderId { get; set; }

        [Column("warehouse_id")]
        public int? WarehouseId { get; set; }

        [Column("delivery_date")]
        public DateTime? DeliveryDate { get; set; }

        [Column("transport_type")]
        public string TransportType { get; set; }

        [Column("status")]
        public string Status { get; set; }

        public ProductionOrder Order { get; set; }
        public Warehouse Warehouse { get; set; }
    }
}