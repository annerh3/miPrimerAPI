using BlogUNAHApi.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlogUNAHApi.Database
{
    public class BlogUNAHContext : DbContext
    {
        public BlogUNAHContext(DbContextOptions options) : base(options)
        {
            // aqui se plantea toda la configuracion de la base de datos
        }

        public DbSet<CategoryEntity> Categories { get; set; } // esto representa una tabla en la base de datos
        public DbSet<TagEntity> Tags { get; set; } 
        public DbSet<PostEntity> Posts { get; set; } 
        public DbSet<PostTagEntity> PostsTags { get; set; } 


    }
}
