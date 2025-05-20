using System.ComponentModel.DataAnnotations.Schema;

namespace PharmaManagerUI.Models
{
    [Table("Staff")]
    public class Staff
    {
        [Column("employee_id")]
        public int Id { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("position")]
        public string Position { get; set; }

        [Column("department")]
        public string Department { get; set; }
    }
}