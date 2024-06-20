/*  -> Startup.cs: Configura los servicios y el pipeline de solicitud HTTP. */

using BlogUNAHApi.Services;
using BlogUNAHApi.Services.Interfaces;

namespace BlogUNAHApi
{
    public class Startup
    {
        private IConfiguration Configuration { get; }

        //Constructor de la clase Startup que recibe la configuración de la aplicación a través de IConfiguration.
        public Startup( IConfiguration configuration )
        {
            Configuration = configuration;
        }

        //  método se utiliza para registrar SERVICIOS en el contenedor de dependencias
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(); // valida a nivel de controladores
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

        // Add custom services
            services.AddTransient<ICategoriesService, CategoriesService>();
        // Registra el servicio CategoriesService como una implementación de ICategoriesService con un ciclo de vida transitorio.
        }

        // método se utiliza para configurar el pipeline de solicitud HTTP de la aplicación
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) // Configuracion del Middleware:
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
