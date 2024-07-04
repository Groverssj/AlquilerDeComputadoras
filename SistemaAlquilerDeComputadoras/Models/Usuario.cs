using SistemaAlquilerDeComputadoras.Dto;
using System.ComponentModel.DataAnnotations;

namespace SistemaAlquilerDeComputadoras.Models
{
    public class Usuario
    {
        [Key]
        public int Ci { get; set; }
        public string? NombreCompleto { get; set; }
        public string? Cuenta { get; set; }
        public int Edad { get; set; }
        public string? Password { get; set; }
        public RolEnum Rol { get; set; }

        //Relaciones 1 ----> *
        public virtual List<Flete>? Fletes { get; set; }
    }
}
