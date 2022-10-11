using System.ComponentModel.DataAnnotations;

namespace Web_Api.Models
{
    public class Cotizacion
    {
        public int IDAuto { get; set; }
        public string Marca { get; set; }
        public string SubMarca { get; set; }
        public string Modelo { get; set; }
        public string Descripcion { get; set; }
        public  string CP { get; set; }
        public  string Estado { get; set; }
        public string Municipio { get; set; }
        public  string Colonia { get; set; }


    }
}
