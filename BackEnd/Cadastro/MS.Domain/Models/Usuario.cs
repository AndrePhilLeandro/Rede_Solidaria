using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace backend.cadastro.MS.Domain.Models
{
    public class Usuario
    {
        [JsonIgnore]
        [Key]
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string Cep { get; set; }
        public string Senha { get; set; }
        public bool EhAtivo { get; set; } = false;
    }
    public class UsuarioLogin
    {
        public string Email { get; set; }
        public string Senha { get; set; }
    }
}