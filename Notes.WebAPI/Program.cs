using Notes.Persistence;
using Notes.Application.Common.Mappings;
using System.Reflection;
using Notes.Application.Interfaces;
using Notes.Application;
using Microsoft.Identity.Client;
using Notes.Domain;
using Notes.WebApi.Middleware;
using Microsoft.EntityFrameworkCore;

namespace Notes.WebAPI
{
    public class Program
    {
        
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
                catch (Exception e) { }
            }
            builder.Services.AddAutoMapper(config => // adding automapper to program
            {
                config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
                config.AddProfile(new AssemblyMappingProfile(typeof(INotesDbContext).Assembly));
            });

            builder.Services.AddDbContext<DbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection")));

            builder.Services.AddAplication();
            builder.Services.AddControllers();

            

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyHeader();
                    policy.AllowAnyMethod();
                    policy.AllowAnyOrigin();
                });
            });

            var app = builder.Build();

            app.UseCustomExceptionHandler(); // implement custom middleware of getting exceptions
            app.UseRouting();
            app.UseHttpsRedirection();
            app.UseCors("AllowAll");
            

            
            //app.MapGet("/", () => "Hello World!");
            app.MapControllers(); // use controllers as endpoints
            app.Run();
        }
        
        
        
    }
}