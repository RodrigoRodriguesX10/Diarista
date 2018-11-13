using Diarista.Classifiers;
using Diarista.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Diarista.ViewModels
{
    public class CadastroUsuario
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
        [Required]
        public string Cpf { get; set; }
        [Required]
        public string Nome { get; set; }
        public string Endereco { get; set; }
        public string Telefone { get; set; }
        public string Descricao { get; set; }
        public TipoUsuario TipoUsuario { get; set; }

        public static explicit operator User(CadastroUsuario c)
        {
            return new User
            {
                Perfil = new Perfil
                {
                    Cpf = c.Cpf,
                    Descricao = c.Descricao,
                    Nome = c.Nome,
                    Telefone = c.Telefone,
                    TipoUsuario = c.TipoUsuario,
                    Casas = new List<Casa>{
                        new Casa
                        {
                            Descricao = "Seu Docimílio",
                            Endereco = c.Endereco
                        }
                    }
                },
                Email = c.Email,
                Password = c.Password,
                TipoUsuario = c.TipoUsuario
            };
        }
    }
}