using System.ComponentModel.DataAnnotations.Schema;

namespace PharmaManagerUI.Models
{
    [Table("Quality_Control")]
    public class QualityControl
    {
        [Column("qc_id")]
        public int Id { get; set; }

        [Column("drug_id")]
        public int DrugId { get; set; }

        public Drug Drug { get; set; }

        [Column("test_date")]
        public DateTime TestDate { get; set; }

        [Column("test_type")]
        public string TestType { get; set; }

        [Column("result")]
        public string Result { get; set; }

        [Column("comments")]
        public string Comments { get; set; }
    }
}