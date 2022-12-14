using Notes.Persistence;

namespace Notes.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            using (var scope = builder.Services.BuildServiceProvider().CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;
                try
                {
                    var context = serviceProvider.GetRequiredService<NotesDbContext>();
                    DbInitializer.Initialize(context);
                }
                catch(Exception e) { }
            }
                
            var app = builder.Build();

            //app.MapGet("/", () => "Hello World!");

            app.Run();
        }

        
        
    }
}