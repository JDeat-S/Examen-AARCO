using Microsoft.AspNetCore.Mvc;
using Nancy.Json;
using Newtonsoft.Json;
using System.Text;

namespace AARCO.Controllers
{
    public class CotizacionController : Controller
    {
        private readonly ILogger<CotizacionController> _logger;
        private readonly IConfiguration _config;
        static HttpClient client = new HttpClient();
        private readonly string URLbase;

        public CotizacionController(ILogger<CotizacionController> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
            URLbase = _config.GetValue<string>("ServicesUrl:WebApi");
        }

        public async Task<IActionResult> CotizacionAsync()
        {
       //     string jsonser = JsonConvert.SerializeObject(input);
            string Serviciosconsulta = URLbase + "/WeatherForecast";
            var response = await client.GetAsync(Serviciosconsulta);
            string apiResponse = await response.Content.ReadAsStringAsync();
            var result = (new JavaScriptSerializer()).DeserializeObject(apiResponse);

            return View();
        }
    }
}