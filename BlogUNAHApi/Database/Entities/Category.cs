using System.ComponentModel.DataAnnotations;
namespace BlogUNAHApi.Database.Entities;
// Define la entidad "Category" con propiedades como Id, Name y Description.
public class Category
{ 
    public Guid Id { get; set; }

    // Data Annotatations
    [Display(Name ="Nombre")]
    [Required(ErrorMessage = "El {0} de la categoria es requerido.")]


    public string Name { get; set; }

    [Display(Name ="Descripción")]
    [MinLength(10, ErrorMessage = "La {0} debe tener al menos {1} caracteres.")]
    public string Description { get; set; }
}
