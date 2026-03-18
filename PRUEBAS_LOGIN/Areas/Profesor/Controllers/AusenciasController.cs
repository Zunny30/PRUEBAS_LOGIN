using PRUEBAS_LOGIN.Areas.Profesor.Models;
using PRUEBAS_LOGIN.Areas.Comun.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Web.Mvc;

namespace PRUEBAS_LOGIN.Areas.Profesor.Controllers
{
   
    public class AusenciasController : Controller
    {

        private static string CadenaConexion =>
        WebConfigurationManager.ConnectionStrings["DB_ACCESO"].ConnectionString;

        public ActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Crear(Ausencia model)
        {
            try
            {
                int idUsuario = Convert.ToInt32(Session["IdUsuario"]);

                using (SqlConnection con = new SqlConnection(CadenaConexion))
                {

                    string query = @"INSERT INTO Ausencias 
                    (IdUsuario,Tipo,Asunto,FechaInicio,FechaFin)
                    VALUES (@IdUsuario,@Tipo,@Asunto,@FechaInicio,@FechaFin)";

                    SqlCommand cmd = new SqlCommand(query, con);

                    cmd.Parameters.AddWithValue("@IdUsuario", idUsuario);
                    cmd.Parameters.AddWithValue("@Tipo", model.Tipo);
                    cmd.Parameters.AddWithValue("@Asunto", model.Asunto);
                    cmd.Parameters.AddWithValue("@FechaInicio", model.FechaInicio);
                    cmd.Parameters.AddWithValue("@FechaFin", model.FechaFin);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }

                ViewBag.Mensaje = "Solicitud registrada correctamente";

                // 🔥 REDIRECCIÓN CORRECTA
                return RedirectToAction("ListaAusencia", "Ausencias", new { area = "Profesor" });
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = "Error: " + ex.Message;
                return View();
            }
        }

        public ActionResult ListaAusencia()
        {
            try
            {
                int idUsuario = Convert.ToInt32(Session["IdUsuario"]);
                var lista = new List<Ausencia>();

                using (SqlConnection con = new SqlConnection(CadenaConexion))
                {
                    string query = @"
                SELECT Tipo, Asunto, FechaInicio, FechaFin
                FROM Ausencias
                WHERE IdUsuario = @IdUsuario
                ORDER BY FechaInicio DESC";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@IdUsuario", idUsuario);
                        con.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var a = new Ausencia
                                {
                                    Tipo = reader["Tipo"] != DBNull.Value ? reader["Tipo"].ToString() : null,
                                    Asunto = reader["Asunto"] != DBNull.Value ? reader["Asunto"].ToString() : null,
                                    FechaInicio = reader["FechaInicio"] != DBNull.Value ? Convert.ToDateTime(reader["FechaInicio"]) : DateTime.MinValue,
                                    FechaFin = reader["FechaFin"] != DBNull.Value ? Convert.ToDateTime(reader["FechaFin"]) : DateTime.MinValue
                                };

                                lista.Add(a);
                            }
                        }
                    }
                }

                return View(lista);
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = "Error: " + ex.Message;
                return View(new List<Ausencia>());
            }
        }

    }
}