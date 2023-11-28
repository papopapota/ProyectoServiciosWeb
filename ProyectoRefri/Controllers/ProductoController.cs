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
            return View();
        }

        // POST: ProductoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registrar(IFormCollection collection)
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

        // GET: ProductoController/Edit/5
        public ActionResult Modificar(int id)
        {
            return View();
        }

        // POST: ProductoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Modificar(int id, IFormCollection collection)
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
                    imagen = dr.GetString(5),
                    estado = dr.GetBoolean(6)
                });
            }
            cn.Close();
            return lista;

        }

        #endregion
    }
}
