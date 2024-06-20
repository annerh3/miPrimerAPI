using BlogUNAHApi.Database.Entities;
using BlogUNAHApi.Dtos.Categories;
using BlogUNAHApi.Services;
using BlogUNAHApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace BlogUNAHApi.Controllers
{  
    [ApiController] //  indica que esta clase es un controlador de API
    [Route("api/categories")] // Define la ruta base para los endpoints de este controlador.

    //  Controlador para manejar las peticiones relacionadas con categorías.
    public class CategoriesController : ControllerBase
    { 
        private readonly ICategoriesService _categoriesService; //dependencia

       // public List<Category> _categories { get; set; } // lista de categorias

        public CategoriesController(ICategoriesService categoriesService) // constructor
        {
            // _categories = new List<Category>();

            // se asigna el servicio recibido al campo "_categoriesService".
            this._categoriesService = categoriesService;
        }

        /*  --- Métodos: --- */

        //Devuelve todas las categorías.
        [HttpGet]
        public async Task<ActionResult> GetAll() 
        {
            return Ok( await _categoriesService.GetCategoriesListAsync());
        }


        // Devuelve una categoría específica por su Id.
        [HttpGet ("{Id}")]
        public async Task<ActionResult> Get(Guid id) 
        {
            var category = await _categoriesService.GetCategoryByIdAsync(id);

            if (category == null) 
            {
                return NotFound(new { Message = $"No se encontro la categoria: {id}"});
            }

            return Ok(category);
        }


        // Crea una nueva categoría, verificando primero que no exista una categoría con el mismo nombre.
        [HttpPost]
        public async Task<ActionResult> Create(CategoryCreateDto dto) 
        {
            //bool categoryExist = _categories
            //    .Any(x => x.Name.ToUpper().Trim().Contains(category.Name.ToUpper()));

            //if (!categoryExist)
            //{
            //    return BadRequest("La Categoria ya está registrada");
            //}

            //category.Id  = Guid.NewGuid();
            //_categories.Add (category);

            //if (String.IsNullOrEmpty(category.Name))
            //{
            //    return BadRequest(new { Message = "El nombre de la categoria es requerido." });
            //}

            //if(!String.IsNullOrEmpty(category.Description) && category.Description.Length < 10)
            //{
            //    return BadRequest(new { Message = "La Descripción debe tener al menos 10 caracteres." }); 
            //}

            bool flag = await _categoriesService.CreateAsync(dto);

            if (!flag)
            {
                return BadRequest("La categoria ya esta registrada.");
            }

            return StatusCode(201);

        }


        // Llama al servicio para editar una categoría existente de manera asíncrona. 
        [HttpPut ("{Id}")]
        public async Task<ActionResult> Edit(CategoryEditDto dto, Guid id)
        {
            var result = await _categoriesService.EditAsync(dto,id);

            if (!result)
            {
                return NotFound();
            }
            return Ok();
        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var categoryResponse = await _categoriesService.GetCategoryByIdAsync(id);

            if (categoryResponse is null)
            {
                return NotFound(new { Message = $"❌ No se encontro la categoria: {id}" });
            }

            await _categoriesService.DeleteAsync(id);
            return Ok(new { Message = $"✅ La categoria '{id}' ha sido eliminada exitosamente." });
        }
    }
}
