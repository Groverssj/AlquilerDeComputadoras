using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaAlquilerDeComputadoras.Models
{
    public class Equipo
    {
        [Key]
        public string? Codigo { get; set; }
        public int Almacenamiento { get; set; }
        public Boolean Estado { get; set; }
        public string? UrlFoto { get; set; }
        // para archivo foto
        [NotMapped]
        [Display (Name = "Cargar Foto")]
        public IFormFile FotoFile { get; set; }
        public string? Pantalla { get; set; }
        public string? Procesador { get; set; }
        public int Ram { get; set; }
        public string? Resolucion { get; set; }
    }
}
