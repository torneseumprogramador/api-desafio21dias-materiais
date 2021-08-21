
using Microsoft.EntityFrameworkCore;
using api_desafio21dias.Models;

namespace api_desafio21dias.Servicos
{
  public class DbContexto : DbContext
  {
    public DbContexto(DbContextOptions<DbContexto> options) : base(options) { }

    public DbSet<Material> Materiais { get; set; }
  }
}