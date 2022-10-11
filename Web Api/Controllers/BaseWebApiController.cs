using Microsoft.AspNetCore.Mvc;

namespace Web_Api.Controllers
{
    public class BaseWebApiController : ControllerBase
    {

        protected RespuestaJson Respuesta { set; get; }

        public BaseWebApiController()
        {
            Respuesta = new RespuestaJson();
        }

    }

    public enum EstatusRespuestaJSON
    {
        SIN_RESPUESTA, OK, ERROR
    }
    public class RespuestaJson
    {
        public EstatusRespuestaJSON Estatus { get; set; }
        public string Mensaje { get; set; }
        public object Data { get; set; }
    }
}
