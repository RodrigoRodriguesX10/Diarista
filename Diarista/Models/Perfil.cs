using Diarista.Classifiers;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Diarista.Models
{
    public class Perfil
    {
        public Perfil()
        {
            ServicosFeitos = new List<Servico>();
            ServicosSolicitados = new List<Servico>();
        }
        [Key]
        public string Cpf { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string Descricao { get; set; }
        public User Usuario { get; set; }
        public TipoUsuario TipoUsuario { get; set; }

        public virtual List<Servico> ServicosSolicitados { get; set; }
        public virtual List<Servico> ServicosFeitos { get; set; }
        public virtual List<Casa> Casas { get; set; }
    }
}