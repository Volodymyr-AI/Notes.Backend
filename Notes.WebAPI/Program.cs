using Notes.Persistence;
using Notes.Application.Common.Mappings;
using System.Reflection;
using Notes.Application.Interfaces;
using Notes.Application;
using Microsoft.Identity.Client;

namespace Notes.WebAPI
{
    public class Program
    {
        public IConfiguration Configuration { get; }

        public Program(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public static void Main(string[] args)
        {
            
            var builder = WebApplication.CreateBuilder(args);

            using (var scope = builder.Services.BuildServiceProvider().CreateScope()) // invoke method of Db initialization
            {
                var serviceProvider = scope.ServiceProvider;
                try
                {
                    var context = serviceProvider.GetRequiredService<NotesDbContext>(); // for accessing dependencies
                    DbInitializer.Initialize(context); // initialize database
                }
                catch(Exception e) { }
            };

            builder.Services.AddAutoMapper(config => // Configuring AutoMapper to get info about currently running build
            {
                config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
                config.AddProfile(new AssemblyMappingProfile(typeof(INotesDbContext).Assembly));
            });

            builder.Services.AddPersistence();
            builder.Services.AddCors(); // cross-origin resource sharing
          
            var app = builder.Build();

            
            //app.MapGet("/", () => "Hello World!");

            app.Run();
        }
        
        
        
    }
}