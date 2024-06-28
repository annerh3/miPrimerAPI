using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogUNAHApi.Database.Entities
{
    public class BaseEntity
    {
        [Key] //para indicar que esto será la PrimaryKey
        [Column("id")]
        public Guid Id { get; set; }
        // ----------------------
        [StringLength(100)]
        [Column("created_by")]
        public string CreatedBy { get; set; }
       // ----------------------
        [Column("created_date")]
        public DateTime CreatedDate { get; set; }
        // ----------------------
        [StringLength (100)]
        [Column("updated_by")]
        public string UpdatedBy { get; set; }
        // ----------------------
        [Column("updated_date")]
        public DateTime UpdatedDate { get; set; }
        //public bool IsDeleted { get; set; }
    }
}
