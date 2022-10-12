using Examen_AARCO.Controllers;
using Examen_AARCO.Models;
using Microsoft.AspNetCore.Mvc;
using Nancy.Json;
using Newtonsoft.Json;
using System.Text;

namespace AARCO.Controllers
{
    public class CotizacionController : BaseController
    {
        public readonly ILogger<CotizacionController> _logger;
        public readonly IConfiguration _config;
        static HttpClient client = new HttpClient();
        public readonly string URLbase;

        public CotizacionController(ILogger<CotizacionController> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
            URLbase = _config.GetValue<string>("ServicesUrl:WebApi");
        }

        public async Task<IActionResult> Cotizacion()
        {
            string Serviciosconsulta = URLbase + "/Marcas";
                var response = await client.GetAsync(Serviciosconsulta);
            string apiResponse = await response.Content.ReadAsStringAsync();

            ViewBag.MsjCode = apiResponse;
            return View();
        }
        public async Task<IActionResult> ObtieneSubMarcas(string marca)
        {
            try
            {
                var input = new
                {
                    Marca = marca
                };

                string jsonser = JsonConvert.SerializeObject(input);
                string Serviciosconsulta = URLbase + "/SubMarcas";
                var response = await client.PostAsync(Serviciosconsulta, new StringContent(jsonser, Encoding.UTF8, "application/json"));
                string apiResponse = await response.Content.ReadAsStringAsync();
                Cotizacion result = (new JavaScriptSerializer()).Deserialize<Cotizacion>(apiResponse);

                Respuesta.Data = result;

                return Json(Respuesta);
            }
            catch (Exception ex)
            {
                Respuesta.Estatus = EstatusRespuestaJSON.ERROR;
                Respuesta.Mensaje = "Ocurrio un error al consultar la información, verifica tu conexión";
                ViewBag.Msj = Respuesta.Estatus + "," + Respuesta.Mensaje;
                return Json(Respuesta);
            }
        }
        public async Task<ActionResult> ObtieneModelos(Cotizacion dtos)
        {
            try
            {
                var input = new
                {
                    Modelo = dtos.Modelo
                };

                string jsonser = JsonConvert.SerializeObject(input);
                string Serviciosconsulta = URLbase + "/Modelos";
                var response = await client.PostAsync(Serviciosconsulta, new StringContent(jsonser, Encoding.UTF8, "application/json"));
                string apiResponse = await response.Content.ReadAsStringAsync();
                Cotizacion result = (new JavaScriptSerializer()).Deserialize<Cotizacion>(apiResponse);

                Respuesta.Data = result;

                return Json(Respuesta);
            }
            catch (Exception ex)
            {
                Respuesta.Estatus = EstatusRespuestaJSON.ERROR;
                Respuesta.Mensaje = "Ocurrio un error al consultar la información, verifica tu conexión";
                ViewBag.Msj = Respuesta.Estatus + "," + Respuesta.Mensaje;
                return Json(Respuesta);
            }
        }
        public async Task<ActionResult> ObtieneDescipcion(Cotizacion dtos)
        {
            try
            {
                var input = new
                {
                    Descripcion = dtos.Descripcion
                };

                string jsonser = JsonConvert.SerializeObject(input);
                string Serviciosconsulta = URLbase + "/Descipcion";
                var response = await client.PostAsync(Serviciosconsulta, new StringContent(jsonser, Encoding.UTF8, "application/json"));
                string apiResponse = await response.Content.ReadAsStringAsync();
                Cotizacion result = (new JavaScriptSerializer()).Deserialize<Cotizacion>(apiResponse);

                Respuesta.Data = result;

                return Json(Respuesta);
            }
            catch (Exception ex)
            {
                Respuesta.Estatus = EstatusRespuestaJSON.ERROR;
                Respuesta.Mensaje = "Ocurrio un error al consultar la información, verifica tu conexión";
                ViewBag.Msj = Respuesta.Estatus + "," + Respuesta.Mensaje;
                return Json(Respuesta);
            }
        }
    }
}