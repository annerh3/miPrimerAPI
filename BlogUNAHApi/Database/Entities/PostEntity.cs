using Swashbuckle.AspNetCore.SwaggerGen;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogUNAHApi.Database.Entities
{
    [Table("posts", Schema="dbo")]
    public class PostEntity : BaseEntity
    {   //-------
        [StringLength(100)]
        [Column("title")]
        [Required]
        public string Title { get; set; }
        //-------
    // TO-DO: Definir la relación entre usuario y post
        [Column("author_id")]
        public string AuthorId { get; set; }
        //-------
        [Column("publication_datetime")]
        public DateTime PublicationDate { get; set; }
        //-------
        [Column("category_id")]
        public Guid CategoryId { get; set; }

        //Propiedad de Navegación
        [ForeignKey(nameof(CategoryId))] // es lo "mismo" que [ForeignKey("CategoryId"))]
        public CategoryEntity Category { get; set; }
        //-------
        [Column("content")]
        public string Content { get; set; }
    }
}
