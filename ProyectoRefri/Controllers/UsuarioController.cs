using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using ProyectoRefri.Models;
using System.Data;

namespace ProyectoRefri.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IConfiguration _confi;
        const string SesionUsuario = "_User";

        int GenerarId() {
            int id = 0;
            string connectionString = _confi["ConnectionStrings:cn"];
            SqlConnection cnn = new SqlConnection(connectionString);
            cnn.Open();
            SqlCommand cmd = new SqlCommand("usp_GenerarCodigo", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                id = dr.GetInt32(0);

            }

            return id;
        }
        public UsuarioController(IConfiguration confi)
        {
            _confi = confi;
        }

        // GET: UsuarioController
        public ActionResult Index()
        {
            return View();
        }

        // GET: UsuarioController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        public ActionResult IniciarSesion()
        {
            return View(new UsuarioModel());
        }
        public ActionResult IniciarSesion(UsuarioModel usurege)
        {
            string connectionString = _confi["ConnectionStrings:cn"];
            SqlConnection cnn = new SqlConnection(connectionString);
            try
            {
                if (string.IsNullOrEmpty(usurege.email) || string.IsNullOrEmpty(usurege.contrasena))
                {
                    ModelState.AddModelError("", "Ingresar los datos solicitados");
                }
                cnn.Open();
                SqlCommand cmd = new SqlCommand("usp_ValidarUsuario", cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    HttpContext.Session.SetString(SesionUsuario, Convert.ToString(usurege.idUsuario));
                    return RedirectToAction("ListarCliente", "Producto");
                }
                else
                {
                    ModelState.AddModelError("", "Datos ingresados no válidos.");
                    return RedirectToAction(nameof(IniciarSesion));
                }
            }
            catch
            {
                return View(usurege);
            }
        }
        public ActionResult Actualizar()
        {
            string connectionString = _confi["ConnectionStrings:cn"];
            SqlConnection cnn = new SqlConnection(connectionString);
            UsuarioModel usu = new UsuarioModel();
            try
            {
                cnn.Open();
                SqlCommand cmd = new SqlCommand("usp_BuscarUsuario", cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idUsuario", HttpContext.Session.GetString(SesionUsuario));
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    usu.idUsuario = dr.GetInt32(0);
                    usu.nombres = dr.GetString(1);
                    usu.apellidos = dr.GetString(2);
                    usu.email = dr.GetString(3);
                    usu.telefono = dr.GetString(4);
                    usu.contrasena = dr.GetString(5);
                    usu.tipoUsuario = dr.GetInt32(6);
                    usu.estado = dr.GetInt32(7);
                    usu.idMembrecia = dr.GetInt32(8);

                }
            }

            catch (Exception ex)
            {

            }

            return View(usu);
        }

        [HttpPost]
        public ActionResult Actualizar(UsuarioModel usu)
        {
            string connectionString = _confi["ConnectionStrings:cn"];
            SqlConnection cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand cmd = new SqlCommand("usp_ActualizarUsuario", cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idUsuario", usu.idUsuario);
                cmd.Parameters.AddWithValue("@nombres", usu.nombres);
                cmd.Parameters.AddWithValue("@apellidos", usu.apellidos);
                cmd.Parameters.AddWithValue("@email", usu.email);
                cmd.Parameters.AddWithValue("@telefono", usu.telefono);
                cmd.Parameters.AddWithValue("@contrasena", usu.contrasena);
                cmd.Parameters.AddWithValue("@tipoUsuario", 1); // tipo usario
                cmd.Parameters.AddWithValue("@estado", 1); // estado
                cmd.Parameters.AddWithValue("@idMembrecia", 1); // di membrecia
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    return RedirectToAction("Actualizar", "Usuario");
                }
                else
                {
                    ModelState.AddModelError("", "Datos ingresados no válidos.");
                    return RedirectToAction(nameof(Actualizar));

                }

            }
            catch
            {
                return View(usu);
            }
        }

        UsuarioModel Buscar(int id)
        {
            
            return null;
        }
        [HttpPost]
        [ValidateAntiForgeryToken]


        // GET: UsuarioController/Create
        public ActionResult Registrar()
        {
            UsuarioModel usu = new UsuarioModel();
            
            return View(usu);
        }

        // POST: UsuarioController/Create
        [HttpPost]
        
        public ActionResult Registrar(UsuarioModel usu)
        {
            string connectionString = _confi["ConnectionStrings:cn"];
            SqlConnection cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand cmd = new SqlCommand("usp_RegistrarUsuario", cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idUsuario", GenerarId());
                cmd.Parameters.AddWithValue("@nombres", usu.nombres);
                cmd.Parameters.AddWithValue("@apellidos", usu.apellidos);
                cmd.Parameters.AddWithValue("@email", usu.email);
                cmd.Parameters.AddWithValue("@telefono", usu.telefono);
                cmd.Parameters.AddWithValue("@contrasena", usu.contrasena);
                cmd.Parameters.AddWithValue("@tipoUsuario", 1); // tipo usario
                cmd.Parameters.AddWithValue("@estado", 1); // estado
                cmd.Parameters.AddWithValue("@idMembrecia", 1); // di membrecia
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    return RedirectToAction("iniciarSesion", "Usuario");
                }
                else {
                    ModelState.AddModelError("", "Datos ingresados no válidos.");
                    return RedirectToAction(nameof(Registrar));

                }

            }
            catch
            {
                return View(usu);
            }
        }

    }
}
