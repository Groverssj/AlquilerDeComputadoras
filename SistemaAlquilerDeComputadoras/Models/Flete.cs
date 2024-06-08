using System.ComponentModel.DataAnnotations;

namespace SistemaAlquilerDeComputadoras.Models
{
    public class Flete
    {
        [Key]
        public int NroRecibo { get; set; }
        public decimal Costo { get; set; }
        public DateTime Fecha { get; set; }
        public DateTime HoraInicio { get; set; }
        public DateTime HoraFinal { get; set; }
    }
}
