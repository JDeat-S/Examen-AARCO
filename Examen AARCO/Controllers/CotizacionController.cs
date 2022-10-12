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

        public async Task<IActionResult> ObtieneSubMarcas([FromBody] Cotizacion dtos)
        {
            try
            {
                var input = new
                {
                    Marca = dtos.Marca,
                    CP = "null",
                    Estado = "null",
                    Modelo = "null",
                    Colonia = "null",
                    SubMarca = "null",
                    Municipio = "null",
                    Descripcion = "null",
                    DescripcionId = "null"

                };

                string jsonser = JsonConvert.SerializeObject(input);
                string Serviciosconsulta = URLbase + "/SubMarcas";
                var response = await client.PostAsync(Serviciosconsulta, new StringContent(jsonser, Encoding.UTF8, "application/json"));
                string apiResponse = await response.Content.ReadAsStringAsync();
                

                return Json(apiResponse);
            }
            catch (Exception ex)
            {
                Respuesta.Estatus = EstatusRespuestaJSON.ERROR;
                Respuesta.Mensaje = "Ocurrio un error al consultar la información, verifica tu conexión";
                ViewBag.Msj = Respuesta.Estatus + "," + Respuesta.Mensaje;
                return Json(Respuesta);
            }
        }
        public async Task<ActionResult> ObtieneModelos([FromBody] Cotizacion dtos)
        {
            try
            {
                var input = new
                {
                    Modelo = "null",
                    Marca = "null",
                    CP = "null",
                    Estado = "null",
                    Colonia = "null",
                    SubMarca = dtos.SubMarca,
                    Municipio = "null",
                    Descripcion = "null",
                    DescripcionId = "null"

                };

                string jsonser = JsonConvert.SerializeObject(input);
                string Serviciosconsulta = URLbase + "/Modelos";
                var response = await client.PostAsync(Serviciosconsulta, new StringContent(jsonser, Encoding.UTF8, "application/json"));
                string apiResponse = await response.Content.ReadAsStringAsync();

                return Json(apiResponse);
            }
            catch (Exception ex)
            {
                Respuesta.Estatus = EstatusRespuestaJSON.ERROR;
                Respuesta.Mensaje = "Ocurrio un error al consultar la información, verifica tu conexión";
                ViewBag.Msj = Respuesta.Estatus + "," + Respuesta.Mensaje;
                return Json(Respuesta);
            }
        }
        public async Task<ActionResult> ObtieneDescipcion([FromBody] Cotizacion dtos)
        {
            try
            {
                var input = new
                {
                    Descripcion = "null",
                    Marca = "null",
                    CP = "null",
                    Estado = "null",
                    Modelo = dtos.Modelo,
                    Colonia = "null",
                    SubMarca = "null",
                    Municipio = "null",
                    DescripcionId = "null"

                };

                string jsonser = JsonConvert.SerializeObject(input);
                string Serviciosconsulta = URLbase + "/Descipcion";
                var response = await client.PostAsync(Serviciosconsulta, new StringContent(jsonser, Encoding.UTF8, "application/json"));
                string apiResponse = await response.Content.ReadAsStringAsync();
                
                return Json(apiResponse);
            }
            catch (Exception ex)
            {
                Respuesta.Estatus = EstatusRespuestaJSON.ERROR;
                Respuesta.Mensaje = "Ocurrio un error al consultar la información, verifica tu conexión";
                ViewBag.Msj = Respuesta.Estatus + "," + Respuesta.Mensaje;
                return Json(Respuesta);
            }
        }
        public async Task<IActionResult> ObtenerCP([FromBody] Cotizacion dtos)
        {
            string Serviciosconsulta = "https://api-test.aarco.com.mx/api-examen/api/examen/sepomex/"+ dtos.CP;
            var response = await client.GetAsync(Serviciosconsulta);
            string apiResponse = await response.Content.ReadAsStringAsync();

            return Json(apiResponse);
        }
        public async Task<IActionResult> Peticion([FromBody] Cotizacion dtos)
        {
            var input = new
            {

                DescripcionId = dtos.DescripcionId

            };

            string jsonser = JsonConvert.SerializeObject(input);
            string Serviciosconsulta = "https://api-test.aarco.com.mx/api-examen/api/examen/crear-peticion";
            var response = await client.PostAsync(Serviciosconsulta, new StringContent(jsonser, Encoding.UTF8, "application/json"));
            string apiResponse = await response.Content.ReadAsStringAsync();
             Serviciosconsulta = "https://api-test.aarco.com.mx/api-examen/api/examen/peticion/" + apiResponse.Replace("\"", ""); 
             response = await client.GetAsync(Serviciosconsulta);
             apiResponse = await response.Content.ReadAsStringAsync();


            return Json(apiResponse);
        }
    }
}