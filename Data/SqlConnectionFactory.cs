using Microsoft.Data.SqlClient;
//using Microsoft.Extensions.Configuration;

namespace ModefyEcommerce.Data
{

    public class SqlConnectionFactory
    {
        private readonly string _connectionString;

        /// <summary>
        /// Le constructeur reçoit la configuration de l'application
        /// (injection automatique si SqlConnectionFactory est enregistré dans le conteneur DI).
        /// </summary>
        /// <param name="configuration">Objet de configuration contenant les chaînes de connexion</param>
        public SqlConnectionFactory(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public SqlConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}
