using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using ProyectoRefri.Models;
using System.Data;

namespace ProyectoRefri.Controllers
{
    public class ProductoController : Controller
    {
        private readonly IConfiguration _confi;
         public ProductoController(IConfiguration confi)
                {
                    _confi = confi;
                }
        #region Acciones



        #endregion
       

        //LISTAR CLIENTE VISTA 
        public async Task<ActionResult> ListarCliente()
        {
            if (HttpContext.Session.GetString("Canasta") == null)
                HttpContext.Session.SetString("Canasta", JsonConvert.SerializeObject(new List<ProductoModel>()));

            return View(await Task.Run(() => ListarVCliente()));
        }
        //LISTAR ADMIN VISTA 
        public async Task<ActionResult> ListarAdmin() 
        {
            if (HttpContext.Session.GetString("Canasta") == null)
                HttpContext.Session.SetString("Canasta", JsonConvert.SerializeObject(new List<ProductoModel>()));

            return View(await Task.Run(() => ListarVAdmin()));
        }

        // GET: ProductoController
        public ActionResult Index()
        {
            return View();
        }

        // GET: ProductoController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ProductoController/Create
        public ActionResult Registrar()
        {
            //ProductoModel producto = new ProductoModel();  
            return View(new ProductoModel()); //producto
        }

        // POST: ProductoController/Create
        [HttpPost]
        public async Task<IActionResult> Registrar(ProductoModel model)
        {
         
                string mensaje = string.Empty;
            SqlConnection cn = new SqlConnection(_confi["ConnectionStrings:cn"]);

            try
            {
                SqlCommand cmd = new("usp_Productos_Agregar", cn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idProducto", model.idProducto);
                cmd.Parameters.AddWithValue("@nombre", model.nombre);
                cmd.Parameters.AddWithValue("@precio", model.precio);
                cmd.Parameters.AddWithValue("@stock", model.stock);
                cmd.Parameters.AddWithValue("@descripcion", model.descripcion);
                cmd.Parameters.AddWithValue("@estado", model.estado);
                cn.Open();
                int i = cmd.ExecuteNonQuery();

                mensaje = string.Format("Se creó {0} productos", i);

            }
            catch(Exception ex)
            {
                mensaje = ex.Message;

            }
            ViewBag.mensaje = mensaje;
            return View(model);
        }


        //METODO BUSCAR PARA MODIFICAR
        ProductoModel Buscar(string id)
        {
            ProductoModel? reg = ListarVAdmin().Where(x => x.idProducto == id).FirstOrDefault();
            return reg;
        }
        // GET:
        public async Task<IActionResult> Modificar(string id)
        {
            return View(await Task.Run(() => Buscar(id))); ;
        }

        // POST:
        [HttpPost]
        public async Task<IActionResult> Modificar(ProductoModel model)
        {
            string mensaje = string.Empty;
            SqlConnection cn = new SqlConnection(_confi["ConnectionStrings:cn"]);

            try
            {
                SqlCommand cmd = new("usp_productos_actualizar", cn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", model.idProducto);
                cmd.Parameters.AddWithValue("@nombre", model.nombre);
                cmd.Parameters.AddWithValue("@precio", model.precio);
                cmd.Parameters.AddWithValue("@stock", model.stock);
                cmd.Parameters.AddWithValue("@descripcion", model.descripcion);
                cmd.Parameters.AddWithValue("@estado", model.estado);
                cn.Open();
                int i = cmd.ExecuteNonQuery();

                mensaje = string.Format("Se modificó {0} productos", i);

            }
            catch (Exception ex)
            {
                mensaje = ex.Message;

            }
            ViewBag.mensaje = mensaje;
            return View(model);
        }

        // GET: ProductoController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProductoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }



        #region Métodos
        IEnumerable<ProductoModel> ListarVCliente()
        {

            List<ProductoModel> lista = new List<ProductoModel>();
            SqlConnection cn = new(_confi["ConnectionStrings:cn"]);
            SqlCommand cmd = new SqlCommand("usp_producto_listarVCliente", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                lista.Add(new ProductoModel
                {
                    idProducto = dr.GetString(0),
                    nombre = dr.GetString(1),
                    precio = dr.GetDouble(2),
                    stock = dr.GetInt32(3),
                    descripcion = dr.GetString(4)
                }); 
            }
            cn.Close();
            return lista;

        }


        IEnumerable<ProductoModel> ListarVAdmin()
        {

            List<ProductoModel> lista = new List<ProductoModel>();
            SqlConnection cn = new(_confi["ConnectionStrings:cn"]);
            SqlCommand cmd = new SqlCommand("usp_productos_listaVAdmin", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                lista.Add(new ProductoModel
                {
                    idProducto = dr.GetString(0),
                    nombre = dr.GetString(1),
                    precio = dr.GetDouble(2),
                    stock = dr.GetInt32(3),
                    descripcion = dr.GetString(4),
                    estado = dr.GetBoolean(5)
                });
            }
            cn.Close();
            return lista;

        }

        #endregion
    }
}
