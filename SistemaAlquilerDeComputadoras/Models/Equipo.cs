﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



namespace SistemaAlquilerDeComputadoras.Models
{
    public class Equipo
    {
        [Key]
        public int Codigo { get; set; }
        public int Almacenamiento { get; set; }
        public Boolean Estado { get; set; }
        public string? Foto { get; set; }
        [NotMapped]
        [Display(Name ="Cargar foto del Equipo")]
        public IFormFile? FotoFile { get; set; }
        public string? Pantalla { get; set; }
        public string? Procesador { get; set; }
        public int Ram { get; set; }
        public string? Resolucion { get; set; }
        //Relaciones *  ----> 1
        public virtual List<Flete>? Fletes { get; set; }
    }
}
