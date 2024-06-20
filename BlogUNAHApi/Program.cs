// "Program.cs": Inicializa y construye la aplicación.
// "Instancia": Un objeto concreto creado a partir de una clase.

using BlogUNAHApi;

var builder = WebApplication.CreateBuilder(args);
//  WebApplication.CreateBuilder(args): Crea un constructor de la aplicación web, configurando servicios y otras configuraciones necesarias.

var startup = new Startup(builder.Configuration);
// Crea una instancia de la clase "Startup", pasando la configuración de la aplicación.

startup.ConfigureServices(builder.Services);
// Llama al método "ConfigureServices" de Startup para registrar los servicios necesarios en el contenedor de dependencias.

var app = builder.Build();
//  Construye la aplicación.

// Configuracion del middleware:
startup.Configure(app, app.Environment);
// Llama al método "Configure" de Startup para configurar el pipeline de solicitud de la aplicación.

app.Run();
// Ejecuta la aplicación.