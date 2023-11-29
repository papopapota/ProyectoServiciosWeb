using Microsoft.AspNetCore.Mvc;

using Microsoft.Data;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProyectoRefri.Models;
using System.Reflection;
using System.Data;

namespace ProyectoRefri.Controllers
{
    public class RefriController : Controller
    {
        public readonly IConfiguration _IConfig;
        public RefriController(IConfiguration IConfig)
        {
            _IConfig = IConfig;
        }

        IEnumerable<RefriModel> Refrigeradora()
        {
            List<RefriModel> listado = new List<RefriModel>();
            using (SqlConnection cn = new SqlConnection(_IConfig["ConnectionStrings:cn"]))
            {

                try {
                    SqlCommand cmd = new SqlCommand("usp_refri_mostraprod", cn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idrefri", 2);
                    cn.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        listado.Add(new RefriModel()
                        {
                            idProducto = dr.GetString(0),
                            nombre = dr.GetString(1),
                            cantidad = dr.GetInt32(2)
                        });
                    }
                    cn.Close();
                }
                catch (Exception ex) { }
            }
            return listado;
        }

        public async Task<IActionResult> ListadoRefri()
        {
            return View(await Task.Run(() => Refrigeradora()));
        }

        [HttpPost] public async Task<IActionResult> Create(string prod) 
        {
            string mensaje = string.Empty;
            using (SqlConnection cn = new SqlConnection(_IConfig["ConnectionStrings:cn"]))
            {
                try 
                {
                    SqlCommand cmd = new SqlCommand("usp_merge_refriprod", cn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", 1);
                    cmd.Parameters.AddWithValue("@idProd", prod);
                    cmd.Parameters.AddWithValue("@cantidad", 1);
                    cn.Open();
                    int c = cmd.ExecuteNonQuery();
                    mensaje = $"Se ha insertado {c} producto ";
                }
                catch (Exception ex) { mensaje = ex.Message; }
            }
            ViewBag.mensaje = mensaje;
            return RedirectToAction("ListarRefri", "Refri");
        }

    }
}
    
