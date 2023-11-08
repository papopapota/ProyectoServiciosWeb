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

        #endregion


    }
}
