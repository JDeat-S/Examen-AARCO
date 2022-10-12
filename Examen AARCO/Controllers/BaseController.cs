using Microsoft.AspNetCore.Mvc;

namespace Examen_AARCO.Controllers
{
    public class BaseController : Controller
    {
        protected RespuestaJson Respuesta { set; get; }

        public BaseController()
        {
            Respuesta = new RespuestaJson();
        }

    }



    public enum EstatusRespuestaJSON
    {
        SIN_RESPUESTA,
        OK,
        ERROR
    }

    public class RespuestaJson
    {
        public EstatusRespuestaJSON Estatus { get; set; }
        public string Mensaje { get; set; }
        public object data { get; set; }
        public string Summary { get; set; }
    }
}