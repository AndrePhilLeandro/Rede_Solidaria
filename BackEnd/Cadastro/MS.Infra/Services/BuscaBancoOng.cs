using backend.cadastro.MS.Domain.Interfaces;
using backend.cadastro.MS.Domain.Models;
using backend.cadastro.MS.Infra.Database;
using backend.cadastro.MS.Infra.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace backend.cadastro.MS.Infra.Services
{
    public class BuscaOngBancoService : IOngBanco
    {
        private readonly OngsBd _Ongsbd;
        public BuscaOngBancoService(OngsBd Ongsbd)
        {
            _Ongsbd = Ongsbd;
        }
        public async Task<Ongs> BuscaOngBanco(Ongs ongs)
        {
            var response = await _Ongsbd.Ongs.FirstOrDefaultAsync(on => on.CNPJ == ongs.CNPJ || on.Email == ongs.Email);
            _Ongsbd.SaveChanges();
            return response;
        }
        public async Task<Ongs> BuscaOngBancoid(int id)
        {
            var response = await _Ongsbd.Ongs.FirstOrDefaultAsync(on => on.Id == id);
            return response;
        }
        public async Task<bool> SalvaOngBanco(Ongs ongs)
        {
            await _Ongsbd.Ongs.AddAsync(ongs);
            _Ongsbd.SaveChanges();
            return true;
        }
        public async Task<List<Ongs>> RetornaOngBanco()
        {
            return _Ongsbd.Ongs.ToList();
        }
        public async Task<Ongs> LoginOng(OngsLogin ongsLogin)
        {
            var ong = await _Ongsbd.Ongs.FirstOrDefaultAsync(on => on.CNPJ == ongsLogin.CNPJ);
            return ong;
        }
        public async Task<bool> EditaOng(int id, Ongs ongs)
        {
            var ObjectOng = await _Ongsbd.Ongs.FirstOrDefaultAsync(on => on.Id == id);
            ObjectOng.Nome = ongs.Nome;
            ObjectOng.CNPJ = ongs.CNPJ;
            ObjectOng.Email = ongs.Email;
            ObjectOng.Telefone = ongs.Telefone;
            ObjectOng.Cep = ongs.Cep;
            ObjectOng.Senha = ongs.Senha;
            _Ongsbd.SaveChanges();
            return true;
        }
    }
}