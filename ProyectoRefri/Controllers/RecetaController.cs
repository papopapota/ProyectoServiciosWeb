﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using ProyectoRefri.Models;
using System.Data;
using System.Diagnostics;

namespace ProyectoRefri.Controllers
{
    public class RecetaController : Controller
    {

        private readonly IConfiguration _confi;

        public RecetaController(IConfiguration confi)
        {
            _confi = confi;
        }

        #region Metodos



        #endregion




        #region Acciones
        public IActionResult Index()
        {
            return View();
        }

        //vista de LISTA en MENU PRINCIPAL
        public ActionResult Lista()
        {
            return View();
        }
        public ActionResult Registrar()
        {
            return View();
        }
        public ActionResult Details(int id)
        {
            return View();
        }

        // VISTA INFORMACIÓN REFRI (DONDE SE VERAN CUANTAS COSAS TIENES EN TU REFRI equide)
        public ActionResult RefriContenido(int id)
        {
            return View();
        }

        #endregion
    








        //CRUD RECETAS =>





        #region METODOS CRUD RECETAS
        IEnumerable<RecetaModel> Recetas()
        {
            List<RecetaModel> recetas = new List<RecetaModel>();
            SqlConnection cn = new SqlConnection(_confi["ConnectionStrings:cn"]);

            SqlCommand cmd = new SqlCommand("sp_listar_receta", cn);
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                recetas.Add(new RecetaModel()
                {
                    idReceta = dr.GetString(0),
                    nombre = dr.GetString(1),
                    imagen = dr.GetString(2),
                    preparacion = dr.GetString(3)
                });
            }
            cn.Close();
            return recetas;
        }
        public int GenerarId()
        {
            int idReceta = 0;
            using (SqlConnection cnn = new SqlConnection(_confi["ConnectionStrings:cn"]))
            {
                SqlCommand cmd = new SqlCommand("usp_Producto_GenerarId", cnn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cnn.Open();
                idReceta = Convert.ToInt32(cmd.ExecuteScalar());
                cnn.Close();
            }
            return idReceta;
        }

        RecetaModel Buscar(string id)
        {
            RecetaModel? reg = Recetas().Where(x => x.idReceta == id).FirstOrDefault();
            return reg;
        }
        public bool Eliminar(string receta)
        {
            bool resp = false;
            string mensaje = string.Empty;

            using (SqlConnection cnn = new SqlConnection(_confi["ConnectionStrings:cn"]))
            {
                using (SqlCommand cmd = new SqlCommand("sp_delete_receta", cnn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdReceta", receta);

                    try
                    {
                        cnn.Open();
                        resp = cmd.ExecuteNonQuery() != 0;
                    }
                    catch (Exception ex)
                    {
                        // Manejar la excepción, puedes registrarla o lanzarla nuevamente según tus necesidades.
                        // También puedes agregar algún mensaje de registro o notificación de error.
                        // Ejemplo: Logger.LogError(ex, "Error al intentar eliminar la receta.");
                        System.Console.WriteLine(ex+ "Error al intentar eliminar la receta");
                        mensaje = ex.Message;

                    }
                }
            }

            return resp;
        }
        #endregion

        #region ACCIONES CRUD RECETAS

        public async Task<IActionResult> ListarRecetas()
        {
            return View(await Task.Run(() => Recetas()));
        }

        public async Task<IActionResult> Create(RecetaModel model)
        {
            string mensaje = string.Empty;

            using (SqlConnection cnn = new SqlConnection(_confi["ConnectionStrings:cn"]))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_merge_receta", cnn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@p_idReceta", model.idReceta);
                    cmd.Parameters.AddWithValue("@p_nombre", model.nombre);
                    cmd.Parameters.AddWithValue("@p_imagen", model.imagen);
                    cmd.Parameters.AddWithValue("@p_preparacion", model.preparacion);
                    cnn.Open();
                    int i = cmd.ExecuteNonQuery();

                    mensaje = string.Format("Se ha creado {0} recetas", i);
                }
                catch (Exception ex)
                {
                    mensaje = ex.Message;
                }
            }

            ViewBag.mensaje = mensaje;
            return View(model);
        }
        public async Task<IActionResult> Edit(string id)
        {
            return View(await Task.Run(() => Buscar(id)));
        }
        [HttpPost]
        public async Task<IActionResult> Edit(RecetaModel model)
        {

            string mensaje = string.Empty;


            using (SqlConnection cnn = new SqlConnection(_confi["ConnectionStrings:cn"]))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_merge_receta", cnn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@p_idReceta", model.idReceta);
                    cmd.Parameters.AddWithValue("@p_nombre", model.nombre);
                    cmd.Parameters.AddWithValue("@p_imagen", model.imagen);
                    cmd.Parameters.AddWithValue("@p_preparacion", model.preparacion);
                    cnn.Open();
                    int i = cmd.ExecuteNonQuery();

                    mensaje = string.Format("Se ha actualizado {0} recetas", i);
                }
                catch (Exception ex)
                {
                    mensaje = ex.Message;
                }
            }

            ViewBag.mensaje = mensaje;
            return View(model);
        }
        
        
        public async Task<IActionResult> Delete(string id)
        {
            return View(await Task.Run(() => Buscar(id)));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(RecetaModel receta)
        {
            Trace.WriteLine("prueba");
            if (Eliminar(receta.idReceta))
            {
                return RedirectToAction("ListarRecetas");
            }
            else
            {
                return View();
            }
        }

        
        #endregion


    }
}
