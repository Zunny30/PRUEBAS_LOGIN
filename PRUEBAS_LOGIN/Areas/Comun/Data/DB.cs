using System;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace PRUEBAS_LOGIN.DOM
{
    public static class DB
    {
        private static string ConnectionString =>
            WebConfigurationManager.ConnectionStrings["DB_ACCESO"].ConnectionString;

        /// <summary>
        /// Obtiene una conexión abierta a la base de datos.
        /// El llamador debe disponerla en un using.
        /// </summary>
        public static SqlConnection GetConnection()
        {
            try
            {
                var conn = new SqlConnection(ConnectionString);
                conn.Open();
                return conn;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al conectar con la base de datos: " + ex.Message);
            }
        }
    }
}