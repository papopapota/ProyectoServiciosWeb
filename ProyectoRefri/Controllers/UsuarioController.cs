using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoRefri.Models;

namespace ProyectoRefri.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IConfiguration _confi;

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
            return View();
        }
        public ActionResult Actualizar()
        {
            return View();
        }
        UsuarioModel Buscar(int id)
        {

            return null;
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult IniciarSesion(IFormCollection collection)
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

        // GET: UsuarioController/Create
        public ActionResult Registrar()
        {
            return View();
        }

        // POST: UsuarioController/Create
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

    }
}
