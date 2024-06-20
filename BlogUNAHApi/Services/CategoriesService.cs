using BlogUNAHApi.Database.Entities;
using BlogUNAHApi.Dtos.Categories;
using BlogUNAHApi.Services.Interfaces;
using Newtonsoft.Json; // se usa para trabajar con archivos JSON.
// Este archivo contiene la implementación del servicio de categorías.
namespace BlogUNAHApi.Services
{
    public class CategoriesService : ICategoriesService // Clase que sigue las reglas de la interfaz: 'ICategoriesService'.
    {
        public readonly string _JSON_FILE; 
        // constante que almacena la ruta del archivo JSON donde se guardan las categorías.

        public CategoriesService() // Método Constructor
        { // Inicializa "_JSON_FILE" con la ruta del archivo JSON.
            _JSON_FILE = "SeedData/categories.json";
        }
        public async Task< List<CategoryDto>> GetCategoriesListAsync()
        {
            return await ReadCategoriesFromFilesAsync();
        }
        public async Task<CategoryDto> GetCategoryByIdAsync(Guid id)
        {
            var categories = await ReadCategoriesFromFilesAsync();
            return categories.FirstOrDefault(c => c.Id == id);
            //(-) Es una expresión lambda que toma cada categoría c y verifica si c.Id es igual a id.
        }
        public async Task<bool> CreateAsync(CategoryCreateDto dto)
        {
            var categoriesDtos = await ReadCategoriesFromFilesAsync();

            bool flag = await CheckCategory(dto);

            if (!flag)
            {
                return false;
            }

            var categoryDto = new CategoryDto
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Description = dto.Description,
            };

            categoriesDtos.Add(categoryDto);

            //pasar de Category Dto a Category Entity
            var categories = categoriesDtos.Select(c => new Category
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description,
            }).ToList();

            ////Otra manera de pasar Category Dto a Category Entity
            //var categoriesList = new List<Category>();

            //for (int i = 0; i < categoriesList.Count; i++)
            //{
            //    var cat = new Category
            //    {
            //        Id = categoriesList[i].Id,
            //        Name = categoriesList[i].Name,
            //        Description = categoriesList[i].Description,
            //    };
            //    categories.Add(cat);
            //}

            await WriteCategoriesToFileAsync(categories);

            return true;
        }
        public async Task<bool> EditAsync(CategoryEditDto dto, Guid id)
        {
            var categoriesDto = await ReadCategoriesFromFilesAsync();

            var existingCategory = categoriesDto.FirstOrDefault(c => c.Id == id);

            bool flag = await CheckCategory(dto);


            if (existingCategory is null  || !flag) 
            {
                return false;
            }
            for (int i = 0; i < categoriesDto.Count; i++)
            {
                if (categoriesDto[i].Id == id)
                {
                    categoriesDto[i].Name = dto.Name;
                    categoriesDto[i].Description = dto.Description;
                }
            }

            //pasar de Category Dto a Category Entity
            var categories = categoriesDto.Select(c => new Category
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description,
            }).ToList();


            await WriteCategoriesToFileAsync(categories);
            return true;

        }
        public async Task<bool> DeleteAsync(Guid id)
        {
            var categoriesDto = await ReadCategoriesFromFilesAsync();

            var categoryToDelete = categoriesDto.FirstOrDefault(c => c.Id == id);

            if (categoryToDelete is null)
            {
                return false;
            }

           categoriesDto.Remove(categoryToDelete);

            //pasar de Category Dto a Category Entity
            var categories = categoriesDto.Select(c => new Category
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description,
            }).ToList();

            await WriteCategoriesToFileAsync(categories); // el JSON es reescrito 
        return true;
        }
    
        
    /*  Métodos Privados  */

        // Leer Categorías
        private async Task<List<CategoryDto>> ReadCategoriesFromFilesAsync()
        { // verificar existencia → leer contenido → deserializar.
            if (!File.Exists(_JSON_FILE)) // Si el archivo no existe, devuelve una lista vacía.
            {
                return new List<CategoryDto>();
            }
       
            var json = await File.ReadAllTextAsync(_JSON_FILE);

            // 'ReadAllTextAsync' es un método asíncrono de la clase 'File' que lee todo el texto de un archivo.


            // 'JsonConvert' es una clase de la biblioteca 'Newtonsoft.Json'       
            // return JsonConvert.DeserializeObject<List<Category>>(json);
            /* "DeserializeObject" es un método que convierte una cadena JSON en un objeto .NET.
                En este caso, convierte el JSON en una lista de objetos Category. */

            var categories = JsonConvert.DeserializeObject<List<Category>>(json);

            var dtos = categories.Select(c => new CategoryDto
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description,
            }).ToList();

            return dtos;
        }

        // Escribir Categorias
        private async Task WriteCategoriesToFileAsync(List<Category> categories)
        { // preparar contenido (serialización) → verificar existencia → escribir contenido.
            var json = JsonConvert.SerializeObject(categories, Formatting.Indented);
        /* 'SerializeObject' es un método de JsonConvert que convierte un objeto .NET (objeto Category) en una cadena JSON.
            "Formatting.Indented" es una opción que da formato al JSON con sangrías (más legible).*/
            if (File.Exists(_JSON_FILE))
            {
                await File.WriteAllTextAsync(_JSON_FILE, json);
            }

        }

        //Metodo para comprobar si se repite el nombre de una categoria o no
        private async Task<bool> CheckCategory(CategoryCreateDto dto)
        {

            var categories = await ReadCategoriesFromFilesAsync();

            for (int i = 0; i < categories.Count; i++)
            {
                if (dto.Name.ToUpper().Trim() == categories[i].Name.ToUpper().Trim())
                {
                    return false;
                }
            }

            return true;
        }
    }
}