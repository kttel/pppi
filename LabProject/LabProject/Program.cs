using LabProject.Repositories.UserRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;

namespace LabProject
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<ApplicationDbContext>(options => 
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddTransient<IAuthorRepository, AuthorRepository>();
            builder.Services.AddScoped<IPasswordEncryptionService, PasswordEncryptionService>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IMyService, MyService>();

            builder.Services.AddScoped<TagService>();
            builder.Services.AddScoped<AuthorService>();
            builder.Services.AddScoped<PostService>();

            builder.Services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;

            })
                .AddJwtBearer(options =>
                {
                    options.SaveToken = true;
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = "http://localhost:7034/",
                        ValidAudience = "http://localhost:7034/",
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("my-32-character-ultra-secure-and-ultra-long-secret")) 
                    };
                });

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API v1.0", Version = "v1" });
                c.SwaggerDoc("v2", new OpenApiInfo { Title = "API v2.0", Version = "v2" });
                c.SwaggerDoc("v3", new OpenApiInfo { Title = "API v3.0", Version = "v3" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme",
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });

            var app = builder.Build();
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1.0");
                    options.SwaggerEndpoint("/swagger/v2/swagger.json", "My API V2.0");
                    options.SwaggerEndpoint("/swagger/v3/swagger.json", "My API V3.0");
                    options.RoutePrefix = "swagger";

                    options.DocumentTitle = "My API - Swagger UI";
                    options.DocExpansion(DocExpansion.None);

                    options.OAuthClientId("my-client-id");
                    options.OAuthClientSecret("my-client-secret");
                    options.OAuthAppName("My API - Swagger UI");
                    options.OAuthScopeSeparator(" ");
                    options.OAuthUsePkce();
                });
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}

