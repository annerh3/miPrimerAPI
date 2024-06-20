using System.ComponentModel.DataAnnotations;

namespace BlogUNAHApi.Dtos.Categories
{
    public class CategoryCreateDto
    {
        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "El {0} de la categoria es requerida.")]
        public string Name { get; set; }


        [Display(Name = "Descripcion")]
        [MinLength(10, ErrorMessage = "La {0} debe tener al menos {1} caracteres.")]
        public string Description { get; set; }

    }
}
