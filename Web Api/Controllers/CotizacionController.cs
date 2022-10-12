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

        [Route("api/Marcas")]
        public RespuestaJson Obtenermarcas()
        {
            cnn = new ConexionSQL();

            var query = "SELECT  * FROM [Examen_AARCO].[dbo].[MarcaAutos]";
            DataSet datos = cnn.Conexion(query);
            List<Cotizacion> cotizacion = new List<Cotizacion>();


            if (datos.Tables.Count > 0)
            {
                foreach (DataRow itemmarcas in datos.Tables[0].Rows)
                {
                    cotizacion.Add(new Cotizacion
                    {
                        ID = int.Parse(itemmarcas[0].ToString()),
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

        [HttpPost]
        [Route("api/SubMarcas")]
        public RespuestaJson Obtenersubmarcas([FromBody]Cotizacion dtos)
        {
            cnn = new ConexionSQL();

            var query = "SELECT  * FROM [Examen_AARCO].[dbo].[SubMarcaAutos] where [IDMarca] = '"+ dtos.Marca + "'";
            DataSet datos = cnn.Conexion(query);
            List<Cotizacion> cotizacion = new List<Cotizacion>();


            if (datos.Tables.Count > 0)
            {
                foreach (DataRow itemmarcas in datos.Tables[0].Rows)
                {
                    cotizacion.Add(new Cotizacion
                    {
                        ID = int.Parse(itemmarcas[0].ToString()),
                        SubMarca = itemmarcas[2].ToString()
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
        [Route("api/Modelos")]
        public RespuestaJson Obtenermodelos(Cotizacion dtos)
        {
            cnn = new ConexionSQL();

            var query = "SELECT  * FROM [Examen_AARCO].[dbo].[ModeloAutos] where IDSubMarca = '" + dtos.SubMarca + "'"; 
            DataSet datos = cnn.Conexion(query);
            List<Cotizacion> cotizacion = new List<Cotizacion>();


            if (datos.Tables.Count > 0)
            {
                foreach (DataRow itemmarcas in datos.Tables[0].Rows)
                {
                    cotizacion.Add(new Cotizacion
                    {
                        ID = int.Parse(itemmarcas[0].ToString()),
                        Modelo = itemmarcas[2].ToString()
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
        [Route("api/Descipcion")]
        public RespuestaJson ObtenerDescipcion(Cotizacion dtos)
        {
            cnn = new ConexionSQL();

            var query = "SELECT  * FROM [Examen_AARCO].[dbo].[DescripcionAutos] where IDModelo = '" + dtos.Modelo + "'";
            DataSet datos = cnn.Conexion(query);
            List<Cotizacion> cotizacion = new List<Cotizacion>();


            if (datos.Tables.Count > 0)
            {
                foreach (DataRow itemmarcas in datos.Tables[0].Rows)
                {
                    cotizacion.Add(new Cotizacion
                    {
                        DescripcionId = itemmarcas[3].ToString(),
                        Descripcion = itemmarcas[2].ToString()
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