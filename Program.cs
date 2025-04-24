using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ModefyEcommerce.Data;
using ModefyEcommerce.Models;
using ModefyEcommerce.Repositories;

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

            // Enregistre le repository produit, aussi en Scoped
            //builder.Services.AddScoped<ProductRepository>();

            // Construit l'application avec les services configurés
            WebApplication app = builder.Build();

            // Active CORS dans le pipeline HTTP
            app.UseCors();

            // Redirige automatiquement vers HTTPS si l'utilisateur accède en HTTP
            app.UseHttpsRedirection();

            // Gère les autorisations (utile si tu ajoutes de l'authentification plus tard)
            app.UseAuthorization();

            // Mappe automatiquement les routes des contrôleurs (ex : /api/products)
            app.MapControllers();

            //using (IServiceScope scope = app.Services.CreateScope())
            //{
            //    IServiceProvider services = scope.ServiceProvider;

            //    ProductRepository productRepository = services.GetRequiredService<ProductRepository>();
            //    List<Product> products = productRepository.GetAll();

            //    foreach (Product product in products)
            //    {
            //        Console.WriteLine($"Produit : {product.ProductName}");
            //    }
            //}

            // Démarre l'application
            app.Run();
        }
    }
}
