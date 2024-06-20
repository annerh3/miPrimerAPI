// "Program.cs": Inicializa y construye la aplicaci�n.
// "Instancia": Un objeto concreto creado a partir de una clase.

using BlogUNAHApi;

var builder = WebApplication.CreateBuilder(args);
//  WebApplication.CreateBuilder(args): Crea un constructor de la aplicaci�n web, configurando servicios y otras configuraciones necesarias.

var startup = new Startup(builder.Configuration);
// Crea una instancia de la clase "Startup", pasando la configuraci�n de la aplicaci�n.

startup.ConfigureServices(builder.Services);
// Llama al m�todo "ConfigureServices" de Startup para registrar los servicios necesarios en el contenedor de dependencias.

var app = builder.Build();
//  Construye la aplicaci�n.

// Configuracion del middleware:
startup.Configure(app, app.Environment);
// Llama al m�todo "Configure" de Startup para configurar el pipeline de solicitud de la aplicaci�n.

app.Run();
// Ejecuta la aplicaci�n.