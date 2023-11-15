using Microsoft.AspNetCore.Mvc;

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


    }
}
