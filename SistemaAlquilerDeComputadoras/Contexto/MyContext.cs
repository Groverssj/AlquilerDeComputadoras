using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SistemaAlquilerDeComputadoras.Models;

namespace SistemaAlquilerDeComputadoras.Contexto
{
	public class MyContext: DbContext
	{
		public MyContext(DbContextOptions options) : base(options)
		{
		}
		public DbSet<Usuario> Usuarios { get; set; }
		public DbSet<Cliente> Clientes { get; set; }
		public DbSet<Equipo> Equipos { get; set; }
		public DbSet<Flete> Fletes { get; set; }
	}
}
