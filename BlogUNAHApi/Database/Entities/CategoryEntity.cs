using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogUNAHApi.Database.Entities
{ // Define la entidad "Category" con propiedades como Id, Name y Description.

    [Table("categories", Schema = "dbo")]
    public class CategoryEntity : BaseEntity
    { 

        // Data Annotatations
        [Display(Name ="Nombre")]
        [Required(ErrorMessage = "El {0} de la categoria es requerido.")]
        [StringLength(50)]
        [Column("name")]
        public string Name { get; set; }

        [Display(Name ="Descripción")]
        [MinLength(10, ErrorMessage = "La {0} debe tener al menos {1} caracteres.")]
        [StringLength(250)]
        [Column("description")]
        public string Description { get; set; }

        public IEnumerable<PostEntity> Posts { get; set; }
        // el "IEnumerable" es solo lectura.
    }
}
