using backend.cadastro.MS.Domain.Models;
using backend.cadastro.MS.Infra.Database;
using backend.cadastro.MS.Infra.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace backend.cadastro.MS.Infra.Services
{
    public class BuscaUsuarioBancoService : IUsuarioBanco
    {
        private readonly UserBd _Userbd;
        public BuscaUsuarioBancoService(UserBd Userbd)
        {
            _Userbd = Userbd;
        }
        public async Task<Usuario> BuscaUsuarioBanco(Usuario usuario)
        {
            var response = await _Userbd.Usuarios.FirstOrDefaultAsync(user => user.Email == usuario.Email);
            _Userbd.SaveChanges();
            return response;
        }
        public async Task<Usuario> BuscaUsuarioBancoid(int id)
        {
            var response = await _Userbd.Usuarios.FirstOrDefaultAsync(on => on.Id == id);
            return response;
        }
        public async Task<bool> SalvaUsuarioBanco(Usuario usuario)
        {
            await _Userbd.Usuarios.AddAsync(usuario);
            _Userbd.SaveChanges();
            return true;
        }
        public async Task<Usuario> LoginUsuario(UsuarioLogin usuario)
        {
            var user = await _Userbd.Usuarios.FirstOrDefaultAsync(user => user.Email == usuario.Email);
            return user;
        }
        public async Task<bool> EditaUsuario(int id, Usuario usuario)
        {
            var ObjectUser = await _Userbd.Usuarios.FirstOrDefaultAsync(user => user.Id == id);
            ObjectUser.Nome = usuario.Nome;
            ObjectUser.Email = usuario.Email;
            ObjectUser.Telefone = usuario.Telefone;
            ObjectUser.Cep = usuario.Cep;
            ObjectUser.Senha = usuario.Senha;
            _Userbd.SaveChanges();
            return true;
        }
    }
}