using Diarista.Classifiers;
using System.ComponentModel.DataAnnotations;

namespace Diarista.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        public Perfil Perfil { get; set; }

        public TipoUsuario TipoUsuario { get; set; }
    }
}