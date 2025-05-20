using System.ComponentModel.DataAnnotations.Schema;

namespace PharmaManagerUI.Models
{
    [Table("Drugs")]
    public class Drug
    {
        [Column("drug_id")]
        public int Id { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("composition")]
        public string Composition { get; set; }

        [Column("production_technology")]
        public string ProductionTechnology { get; set; }

        [Column("packaging_type")]
        public string PackagingType { get; set; }

        [Column("labeling_info")]
        public string LabelingInfo { get; set; }

        [Column("storage_conditions")]
        public string StorageConditions { get; set; }
    }
}