using backend.cadastro.MS.Domain.Models;

namespace backend.cadastro.MS.Infra.Interfaces
{
    public interface IUsuarioBanco
    {
        public Task<Usuario> BuscaUsuarioBanco(Usuario usuario);
        public Task<Usuario> BuscaUsuarioBancoid(int id);
        public Task<bool> SalvaUsuarioBanco(Usuario usuario);
        public Task<Usuario> LoginUsuario(UsuarioLogin usuario);
        public Task<bool> EditaUsuario(int id, Usuario usuario);
    }
}