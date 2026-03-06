using backend.cadastro.MS.Domain.Models;

namespace backend.cadastro.MS.Infra.Interfaces
{
    public interface IOngBanco
    {
        public Task<Ongs> BuscaOngBanco(Ongs ongs);
        public Task<Ongs> BuscaOngBancoid(int id);
        public Task<bool> SalvaOngBanco(Ongs ongs);
        public Task<List<Ongs>> RetornaOngBanco();
        public Task<Ongs> LoginOng(OngsLogin ongsLogin);
        public Task<bool> EditaOng(int id, Ongs ongs);
    }
}