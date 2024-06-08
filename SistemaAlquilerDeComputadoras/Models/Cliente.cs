using System.ComponentModel.DataAnnotations;

namespace SistemaAlquilerDeComputadoras.Models
{
    public class Cliente
    {
        [Key]
        public int Ci { get; set; }
        public string? NombreCompleto { get; set; }
    }
}
