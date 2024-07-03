using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SistemaAlquilerDeComputadoras.Models;

namespace SistemaAlquilerDeComputadoras.Data
{
    public class SistemaAlquilerDeComputadorasContext : DbContext
    {
        public SistemaAlquilerDeComputadorasContext (DbContextOptions<SistemaAlquilerDeComputadorasContext> options)
            : base(options)
        {
        }

        public DbSet<SistemaAlquilerDeComputadoras.Models.Equipo> Equipo { get; set; } = default!;
        public DbSet<SistemaAlquilerDeComputadoras.Models.Flete> Flete { get; set; } = default!;
    }
}
