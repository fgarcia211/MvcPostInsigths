using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.AspNetCore.Mvc;
using MvcPostInsigths.Extensions;
using MvcPostInsigths.Models;
using MvcPostInsigths.Repositories;

namespace MvcPostInsigths.Controllers
{
    public class JugadoresController : Controller
    {
        private RepositoryJugadores repo;
        private readonly ILogger<JugadoresController> _logger;
        private TelemetryClient telemetryClient;

        public JugadoresController(RepositoryJugadores repo, ILogger<JugadoresController> logger, TelemetryClient client)
        {
            this.repo = repo;
            this._logger = logger;
            this.telemetryClient = client;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult LoginJugador()
        {
            return View();
        }

        public IActionResult PerfilJugador()
        {
            Jugador jug = HttpContext.Session.GetObject<Jugador>("JUGADOR");

            if (jug != null)
            {
                string msg = "Acceso a perfil: " + jug.Nombre;
                SeverityLevel level = SeverityLevel.Warning;
                TraceTelemetry traza = new TraceTelemetry(msg, level);
                this.telemetryClient.TrackTrace(traza);

                return View(jug);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult LoginJugador(string nombre, string contrasenia)
        {
            Jugador jug = this.repo.FindJugador(nombre, contrasenia);
            
            if (jug != null)
            {
                string msg = "Login de jugador: " + jug.Nombre;
                SeverityLevel level = SeverityLevel.Information;
                TraceTelemetry traza = new TraceTelemetry(msg, level);
                this.telemetryClient.TrackTrace(traza);

                HttpContext.Session.SetObject("JUGADOR", jug);

                return RedirectToAction("Index");
            }

            ViewData["MENSAJE"] = "Credenciales incorrectas";
            return View();
        }

        public IActionResult LogoutJugador()
        {
            HttpContext.Session.Remove("JUGADOR");
            return RedirectToAction("Index");
        }
    }
}
