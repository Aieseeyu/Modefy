using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Swashbuckle.AspNetCore.SwaggerGen;
using ModefyEcommerce.Data;
using ModefyEcommerce.Models;
using ModefyEcommerce.Repositories;
using Microsoft.Extensions.Configuration.UserSecrets;
using ModefyEcommerce.Tools;

namespace ModefyEcommerce
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Crée le builder de l'application ASP.NET Core
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

            // Ajoute les contrôleurs à l'application (pour les routes http api)
            builder.Services.AddControllers();



            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();



            // Ajoute CORS (Cross-Origin Resource Sharing) avec une politique permissive par défaut
            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });
            });

            // Enregistre la factory de connexion SQL avec un cycle de vie Scoped (une instance par requête)
            builder.Services.AddScoped<SqlConnectionFactory>();
            builder.Services.AddSingleton<HashHelper>();


            // Enregistre le repository produit, aussi en Scoped
            //builder.Services.AddScoped<ProductRepository>();

            // Construit l'application avec les services configurés
            WebApplication app = builder.Build();

            // Active CORS dans le pipeline HTTP
            app.UseCors();




                app.UseSwagger();
                app.UseSwaggerUI();




            // Redirige automatiquement vers HTTPS si l'utilisateur accède en HTTP
            app.UseHttpsRedirection();

            // Gère les autorisations (utile si tu ajoutes de l'authentification plus tard)
            app.UseAuthorization();

            // Mappe automatiquement les routes des contrôleurs (ex : /api/products)
            app.MapControllers();

            // Démarre l'application
            app.Run();
        }
    }
}
