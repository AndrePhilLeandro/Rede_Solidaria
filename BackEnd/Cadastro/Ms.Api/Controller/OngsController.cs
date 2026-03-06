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
    public class OngsController : ControllerBase
    {
        private readonly IValidaOngs _validaOngs;
        private readonly IOngBanco _buscaBanco;
        private readonly PasswordHasher<string> _hasher;
        public OngsController(IValidaOngs validaOngs, IOngBanco buscaBanco, PasswordHasher<string> hasher)
        {
            _validaOngs = validaOngs;
            _buscaBanco = buscaBanco;
            _hasher = hasher;
        }
        [AllowAnonymous]
        [HttpPost("CadastroOngs")]
        public async Task<IActionResult> CadastroOngs([FromBody] Ongs ongs)
        {
            if (_buscaBanco.BuscaOngBanco(ongs) != null)
            {
                return Conflict("ONG Ja Cadastrada!");
            }
            if (await _validaOngs.Validacao(ongs) == false)
            {
                return BadRequest("Dados Invalidos!");
            }
            ongs.Senha = GerarHash(ongs.Senha);
            if (await _buscaBanco.SalvaOngBanco(ongs) == false)
            {
                return BadRequest();
            }
            return Created();
        }
        [AllowAnonymous]
        [HttpPost("LoginOngs")]
        public async Task<IActionResult> LoginOngs([FromBody] OngsLogin ongs)
        {
            var ong = await _buscaBanco.LoginOng(ongs);
            if (ong == null)
            {
                return BadRequest("Usuario ou senha invalido!");
            }
            if (VerificarSenha(ongs.Senha, ong.Senha) == false)
            {
                return BadRequest("Usuario ou senha invalido!");
            }
            return Ok();
        }
        [Authorize(Roles = "User")]
        [HttpGet("GetOngs")]
        public async Task<IActionResult> GetOngs()
        {
            return Ok(await _buscaBanco.RetornaOngBanco());
        }
        [Authorize(Roles = "Company")]
        [HttpPut("EditaOng")]
        public async Task<IActionResult> EditaOng([FromBody] Ongs ongs, int id)
        {
            ongs.Senha = GerarHash(ongs.Senha);
            if (await _buscaBanco.BuscaOngBancoid(id) == null)
            {
                return BadRequest("Id Invalido!");
            }
            if (await _validaOngs.Validacao(ongs) == false)
            {
                return BadRequest("Dados Invalidos!");
            }
            await _buscaBanco.EditaOng(id, ongs);
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