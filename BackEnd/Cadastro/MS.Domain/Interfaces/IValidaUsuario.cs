using backend.cadastro.MS.Domain.Models;

namespace backend.cadastro.MS.Domain.Interfaces
{
    public interface IValidaUsuario
    {
        public Task<bool> Validacao(Usuario usuario);
    }
}