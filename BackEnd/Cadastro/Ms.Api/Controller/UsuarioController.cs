using System.Runtime.Versioning;
using backend.cadastro.MS.Application.Services;
using backend.cadastro.MS.Domain.Interfaces;
using backend.cadastro.MS.Domain.Models;
using backend.cadastro.MS.Infra.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;

namespace backend.cadastro.MS.Api.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IValidaUsuario _validaUsuario;
        private readonly IUsuarioBanco _buscaBanco;
        private readonly PasswordHasher<string> _hasher;
        public UsuarioController(IValidaUsuario validaUsuario, IUsuarioBanco buscaBanco, PasswordHasher<string> hasher)
        {
            _validaUsuario = validaUsuario;
            _buscaBanco = buscaBanco;
            _hasher = hasher;
        }
        [AllowAnonymous]
        [HttpPost("CadastroUsuario")]
        public async Task<IActionResult> CadastroUsuario([FromBody] Usuario usuario)
        {
            if (_buscaBanco.BuscaUsuarioBanco(usuario) != null)
            {
                return Conflict("Usuario Ja Cadastrada!");
            }
            if (await _validaUsuario.Validacao(usuario) == false)
            {
                return BadRequest("Dados Invalidos!");
            }
            usuario.Senha = GerarHash(usuario.Senha);
            if (await _buscaBanco.SalvaUsuarioBanco(usuario) == false)
            {
                return BadRequest();
            }
            return Created();
        }
        [AllowAnonymous]
        [HttpPost("Login_Usuario")]
        public async Task<IActionResult> Login_Usuario([FromBody] UsuarioLogin usuario)
        {
            var user = await _buscaBanco.LoginUsuario(usuario);
            if (VerificarSenha(usuario.Senha, user.Senha) == false || user == null)
            {
                return BadRequest("Usuario ou senha invalido!");
            }
            return Ok();
        }
        [Authorize(Roles = "User")]
        [HttpPut("EditaUsuario")]
        public async Task<IActionResult> EditaUsuario([FromBody] Usuario usuario, int id)
        {
            usuario.Senha = GerarHash(usuario.Senha);
            if (await _buscaBanco.BuscaUsuarioBancoid(id) == null)
            {
                return BadRequest("Id Invalido!");
            }
            if (await _validaUsuario.Validacao(usuario) == false)
            {
                return BadRequest("Dados Invalidos!");
            }
            await _buscaBanco.EditaUsuario(id, usuario);
            return NoContent();
        }









        private string GerarHash(string senha)
        {
            return _hasher.HashPassword(null, senha);
        }

        private bool VerificarSenha(string senhaDigitada, string hashSalvo)
        {
            var resultado = _hasher.VerifyHashedPassword(
                null,
                hashSalvo,
                senhaDigitada
            );
            return resultado == PasswordVerificationResult.Success;
        }
    }
}