using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using Web_Api.Models;

namespace Web_Api.Controllers
{
    [ApiController]
    public class CotizacionController : BaseWebApiController
    {
        protected ConexionSQL cnn = null;
        private readonly ILogger<CotizacionController> _logger;


        public CotizacionController(IConfiguration configuration, ILogger<CotizacionController> logger)
        {
            _logger = logger;


        }

        [Route("api/Cotizacion")]
        public RespuestaJson Obtenermarcas()
        {
            cnn = new ConexionSQL();

            var query = "SELECT [IDAuto],[Marca] FROM[Examen_AARCO].[dbo].[ListaDeAutos]";
            DataSet datos = cnn.Conexion(query);
            List<Cotizacion> cotizacion = new List<Cotizacion>();


            if (datos.Tables.Count > 0)
            {
                foreach (DataRow itemmarcas in datos.Tables[0].Rows)
                {
                    cotizacion.Add(new Cotizacion
                    {
                        IDAuto = int.Parse(itemmarcas[0].ToString()),
                        Marca = itemmarcas[1].ToString()
                    });
                }
                Respuesta.Data = cotizacion;
                Respuesta.Estatus = EstatusRespuestaJSON.OK;
                Respuesta.Mensaje = "";
            }
            else
            {
                Respuesta.Data = null;
                Respuesta.Estatus = EstatusRespuestaJSON.ERROR;
                Respuesta.Mensaje = "Ocurrio un error al consultar los datos, intentelo nuevamente, si el error persiste contacte con el administrador del sistema.";
            }
            return Respuesta;
        }
    }
}