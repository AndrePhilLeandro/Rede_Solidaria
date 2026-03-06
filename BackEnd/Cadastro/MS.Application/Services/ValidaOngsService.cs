using backend.cadastro.MS.Domain.Interfaces;
using backend.cadastro.MS.Domain.Models;

namespace backend.cadastro.MS.Application.Services
{
    public class ValidaOngsService : IValidaOngs
    {
        public async Task<bool> Validacao(Ongs ongs)
        {
            if (string.IsNullOrWhiteSpace(ongs.Nome) || string.IsNullOrWhiteSpace(ongs.CNPJ) || string.IsNullOrWhiteSpace(ongs.Email) || string.IsNullOrWhiteSpace(ongs.Telefone) || string.IsNullOrWhiteSpace(ongs.Cep) || string.IsNullOrWhiteSpace(ongs.Senha))
            {
                return false;
            }
            return true;
        }
    }
}