using backend.cadastro.MS.Domain.Models;

namespace backend.cadastro.MS.Domain.Interfaces
{
    public interface IValidaOngs
    {
        public Task<bool> Validacao(Ongs ongs);
    }
}