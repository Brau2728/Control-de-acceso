using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;

namespace prueva1
{
    public class ConexionDB
    {
        // Método para obtener la conexión lista para usarse
        public static SqlConnection ObtenerConexion()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["CadenaConexion"].ConnectionString;
            SqlConnection conexion = new SqlConnection(connectionString);
            conexion.Open(); // Abrimos la conexión aquí para ahorrar líneas después
            return conexion;
        }
    }
}
