using Notes.Persistence;
using Notes.Application.Common.Mappings;
using System.Reflection;
using Notes.Application.Interfaces;
using Notes.Application;
using Microsoft.Identity.Client;
using Notes.Domain;

namespace Notes.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddAplication();

            builder.Services.AddAutoMapper(typeof(Note));

            var app = builder.Build();

            //app.MapGet("/", () => "Hello World!");
            
            app.Run();
        }
        
        
        
    }
}