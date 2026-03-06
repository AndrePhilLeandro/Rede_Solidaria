using backend.cadastro.MS.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.cadastro.MS.Infra.Database
{
    public class OngsBd : DbContext
    {
        public OngsBd(DbContextOptions<OngsBd> options) : base(options) { }
        public DbSet<Ongs> Ongs { get; set; }
    }
    public class UserBd : DbContext
    {
        public UserBd(DbContextOptions<UserBd> options) : base(options) { }
        public DbSet<Usuario> Usuarios { get; set; }
    }
}