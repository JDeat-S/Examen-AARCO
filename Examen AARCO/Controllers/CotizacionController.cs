using Microsoft.AspNetCore.Mvc;

namespace AARCO.Controllers
{
    public class CotizacionController : Controller
    {
        private readonly ILogger<CotizacionController> _logger;

        public CotizacionController(ILogger<CotizacionController> logger)
        {
            _logger = logger;
        }

        public IActionResult Cotizacion()
        {
            return View();
        }
    }
}