using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using PRUEBAS_LOGIN.Areas.Comun.Models;
using PRUEBAS_LOGIN.DOM;

namespace PRUEBAS_LOGIN.Areas.Comun.Data
{
    public class UsuarioDAO
    {
        /// <summary>
        /// Crea un nuevo usuario en la base de datos
        /// </summary>
        /// <param name="usuario">Objeto Usuario con los datos a insertar</param>
        /// <returns>ID del usuario creado, 0 si falla</returns>
        public static int Crear(Usuario usuario)
        {
            const string sql = @"
                INSERT INTO dbo.USUARIO (Correo, Clave)
                VALUES (@Correo, @Clave);
                SELECT CAST(SCOPE_IDENTITY() AS INT);";

            using (var conn = DB.GetConnection())
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@Correo", usuario.Correo ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Clave", usuario.Clave ?? (object)DBNull.Value);

                try
                {
                    var result = cmd.ExecuteScalar();
                    return result != null ? Convert.ToInt32(result) : 0;
                }
                catch (SqlException sqlEx) when (sqlEx.Number == 2627 || sqlEx.Number == 2601)
                {
                    // violation of UNIQUE constraint (correo duplicado)
                    throw new Exception("El correo ya está registrado.", sqlEx);
                }
            }
        }

        /// <summary>
        /// Obtiene un usuario por su ID
        /// </summary>
        /// <param name="idUsuario">ID del usuario a buscar</param>
        /// <returns>Objeto Usuario si existe, null si no</returns>
        public static Usuario ObtenerPorId(int idUsuario)
        {
            const string sql = "SELECT IdUsuario, Correo, Clave FROM dbo.USUARIO WHERE IdUsuario = @IdUsuario";

            try
            {
                using (var conn = DB.GetConnection())
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@IdUsuario", idUsuario);
                    using (var rdr = cmd.ExecuteReader())
                    {
                        if (rdr.Read())
                        {
                            return new Usuario
                            {
                                IdUsuario = rdr.GetInt32(0),
                                Correo = rdr.IsDBNull(1) ? null : rdr.GetString(1),
                                Clave = rdr.IsDBNull(2) ? null : rdr.GetString(2)
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener usuario: " + ex.Message);
            }

            return null;
        }

        /// <summary>
        /// Obtiene un usuario por su correo
        /// </summary>
        /// <param name="correo">Correo del usuario a buscar</param>
        /// <returns>Objeto Usuario si existe, null si no</returns>
        public static Usuario ObtenerPorCorreo(string correo)
        {
            const string sql = "SELECT IdUsuario, Correo, Clave FROM dbo.USUARIO WHERE Correo = @Correo";

            try
            {
                using (var conn = DB.GetConnection())
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Correo", correo ?? (object)DBNull.Value);
                    using (var rdr = cmd.ExecuteReader())
                    {
                        if (rdr.Read())
                        {
                            return new Usuario
                            {
                                IdUsuario = rdr.GetInt32(0),
                                Correo = rdr.IsDBNull(1) ? null : rdr.GetString(1),
                                Clave = rdr.IsDBNull(2) ? null : rdr.GetString(2)
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener usuario por correo: " + ex.Message);
            }

            return null;
        }

        /// <summary>
        /// Obtiene todos los usuarios de la base de datos
        /// </summary>
        /// <returns>Lista de usuarios</returns>
        public static List<Usuario> ObtenerTodos()
        {
            const string sql = "SELECT IdUsuario, Correo, Clave FROM dbo.USUARIO";
            var lista = new List<Usuario>();

            try
            {
                using (var conn = DB.GetConnection())
                using (var cmd = new SqlCommand(sql, conn))
                using (var rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        lista.Add(new Usuario
                        {
                            IdUsuario = rdr.GetInt32(0),
                            Correo = rdr.IsDBNull(1) ? null : rdr.GetString(1),
                            Clave = rdr.IsDBNull(2) ? null : rdr.GetString(2)
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener todos los usuarios: " + ex.Message);
            }

            return lista;
        }

        /// <summary>
        /// Actualiza un usuario existente
        /// </summary>
        /// <param name="usuario">Objeto Usuario con los datos actualizados</param>
        /// <returns>true si se actualiz\u00f3 correctamente, false si no</returns>
        public static bool Actualizar(Usuario usuario)
        {
            const string sql = @"
                UPDATE dbo.USUARIO
                SET Correo = @Correo, Clave = @Clave
                WHERE IdUsuario = @IdUsuario";

            try
            {
                using (var conn = DB.GetConnection())
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Correo", usuario.Correo ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Clave", usuario.Clave ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@IdUsuario", usuario.IdUsuario);

                    var rows = cmd.ExecuteNonQuery();
                    return rows > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al actualizar usuario: " + ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Elimina un usuario por su ID
        /// </summary>
        /// <param name="idUsuario">ID del usuario a eliminar</param>
        /// <returns>true si se elimin\u00f3 correctamente, false si no</returns>
        public static bool Eliminar(int idUsuario)
        {
            const string sql = "DELETE FROM dbo.USUARIO WHERE IdUsuario = @IdUsuario";

            try
            {
                using (var conn = DOM.DB.GetConnection())
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@IdUsuario", idUsuario);
                    var rows = cmd.ExecuteNonQuery();
                    return rows > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al eliminar usuario: " + ex.Message);
                return false;
            }
        }
    }
}
