using Examen_AARCO.Controllers;
using Examen_AARCO.Models;
using Microsoft.AspNetCore.Mvc;
using Nancy.Json;
using System.Text;

namespace AARCO.Controllers
{
    public class CotizacionController : BaseController
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
            string Serviciosconsulta = URLbase + "/Marcas";
            var response = await client.GetAsync(Serviciosconsulta);
            string apiResponse = await response.Content.ReadAsStringAsync();

            ViewBag.MsjCode = apiResponse;
            return View();
        }

        private ActionResult ObtieneUsuarios(Cotizacion dtos)
        {
            try
            {
                var input = new
                {
                    Usuario = usuario,
                    iduser = iduser,
                };

                string inputJson = (new JavaScriptSerializer()).Serialize(input);
                WebClient client = new WebClient();
                client.Headers["Content-type"] = "application/json";
                client.Encoding = Encoding.UTF8;
                client.Headers.Add("Authorization", "Bearer " + Usuario.Token);
                string json = client.UploadString(urlAPIS + "/Usuarios", "POST", inputJson);
                var LstSerialize = new JavaScriptSerializer();
                RespuestaJson result = (new JavaScriptSerializer()).Deserialize<RespuestaJson>(json);

                if (result.Estatus == EstatusRespuestaJSON.OK)
                {
                    ViewBag.usuario = LstSerialize.Serialize(result.Data);
                }
                else if (result.Estatus == EstatusRespuestaJSON.LOGIN)
                {
                    Respuesta.Estatus = result.Estatus;
                    Respuesta.Mensaje = result.Mensaje;

                }
                else if (result.Estatus == EstatusRespuestaJSON.SIN_RESPUESTA)
                {
                    Respuesta.Estatus = result.Estatus;
                    Respuesta.Mensaje = result.Mensaje;
                }




                return Json(Respuesta, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Respuesta.Estatus = EstatusRespuestaJSON.ERROR;
                Respuesta.Mensaje = "Ocurrio un error al consultar la información, verifica tu conexión";
                ViewBag.Msj = Respuesta.Estatus + "," + Respuesta.Mensaje;
                return Json(Respuesta, JsonRequestBehavior.AllowGet);
            }
        }
    }
}