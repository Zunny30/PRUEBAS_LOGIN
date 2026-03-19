using PRUEBAS_LOGIN.Areas.Comun.Data;
using PRUEBAS_LOGIN.Areas.Comun.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace PRUEBAS_LOGIN.Areas.Comun.Controllers
{

    public class AccesoController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }


        public ActionResult Registrar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registrar(Usuario oUsuario)
        {
            try
            {
                // Validar que el correo no esté vacío
                if (string.IsNullOrEmpty(oUsuario.Correo) || string.IsNullOrEmpty(oUsuario.Clave))
                {
                    ViewData["Mensaje"] = "Correo y contraseña son obligatorios";
                    return View();
                }

                // Hashear la contraseña
                oUsuario.Clave = ConvertirSha256(oUsuario.Clave);

                // Verificar si el correo ya existe
                Usuario usuarioExistente = UsuarioDAO.ObtenerPorCorreo(oUsuario.Correo);
                if (usuarioExistente != null)
                {
                    ViewData["Mensaje"] = "El correo ya está registrado";
                    return View();
                }

                // Crear el usuario
                int idUsuario = UsuarioDAO.Crear(oUsuario);

                if (idUsuario > 0)
                {
                    TempData["Mensaje"] = "Usuario registrado exitosamente";
                    return RedirectToAction("Login", "Acceso", new { area = "Comun" });
                }
                else
                {
                    ViewData["Mensaje"] = "Error al registrar el usuario";
                    return View();
                }
            }
            catch (Exception ex)
            {
                ViewData["Mensaje"] = "Error: " + ex.Message;
                return View();
            }
        }
        [HttpPost]
        public ActionResult Login(Usuario oUsuario)
        {
            try
            {
                // Validar datos
                if (string.IsNullOrEmpty(oUsuario.Correo) || string.IsNullOrEmpty(oUsuario.Clave))
                {
                    ViewData["Mensaje"] = "Correo y contraseña son obligatorios";
                    return View();
                }

                // Hashear la contraseña
                string claveHasheada = ConvertirSha256(oUsuario.Clave);

                // Buscar el usuario por correo
                Usuario usuarioEncontrado = UsuarioDAO.ObtenerPorCorreo(oUsuario.Correo);

                // Validar que el usuario existe y su contraseña es correcta
                if (usuarioEncontrado != null && usuarioEncontrado.Clave == claveHasheada)
                {
                    // Crear sesión
                    Session["usuario"] = usuarioEncontrado;
                    Session["IdUsuario"] = usuarioEncontrado.IdUsuario;
                    return RedirectToAction("Index", "Home", new { area = "Comun" });
                }
                else
                {
                    ViewData["Mensaje"] = "Correo o contraseña incorrectos";
                    return View();
                }
            }
            catch (Exception ex)
            {
                ViewData["Mensaje"] = "Error: " + ex.Message;
                return View();
            }
        }

        /// <summary>
        /// Convierte un texto a su equivalente SHA256
        /// </summary>
        /// <param name="texto">Texto a convertir</param>
        /// <returns>Hash SHA256 del texto</returns>
        public static string ConvertirSha256(string texto)
        {
            StringBuilder sb = new StringBuilder();
            using (SHA256 hash = SHA256Managed.Create())
            {
                Encoding enc = Encoding.UTF8;
                byte[] result = hash.ComputeHash(enc.GetBytes(texto));
                foreach (byte b in result)
                    sb.Append(b.ToString("x2"));
            }
            return sb.ToString();
        }
    }

}