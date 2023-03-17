using LabProject.Intefraces;
using Microsoft.EntityFrameworkCore;

namespace LabProject
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // DB just for future enhancements
            builder.Services.AddDbContext<BlogContext>(options => 
                options.UseSqlServer(builder.Configuration.GetConnectionString("BlogContext")));

            // I use AddScoped because it's effectively to maintain state within a request in such way.
            // I need creating a new instance of a service once per request within the scope with my program.
            builder.Services.AddScoped<TagService>();
            builder.Services.AddScoped<UserService>();
            builder.Services.AddScoped<PostService>();

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}

