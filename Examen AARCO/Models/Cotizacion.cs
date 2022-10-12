using System.ComponentModel.DataAnnotations;

namespace Examen_AARCO.Models
{
    public class Cotizacion
    {
        [Display(Name = "Marca: ")]
        public string Marca { get; set; }
        [Display(Name = "SubMarca: ")]
        public string SubMarca { get; set; }
        [Display(Name = "Modelo: ")]
        public string Modelo { get; set; }
        [Display(Name = "Descripcion: ")]
        public string Descripcion { get; set; }
        public string DescripcionId { get; set; }
        public  string CP { get; set; }
        public  string Estado { get; set; }
        [Display(Name = "Municipio: ")]
        public string Municipio { get; set; }
        [Display(Name = "Colonia: ")]
        public  string Colonia { get; set; }


    }
}
