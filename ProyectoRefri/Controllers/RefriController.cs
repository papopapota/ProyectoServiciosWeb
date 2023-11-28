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
                    cmd.Parameters.AddWithValue("@idUsuario", 2);
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





    }
}
    
