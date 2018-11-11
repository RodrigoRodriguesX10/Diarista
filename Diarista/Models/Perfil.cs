using Diarista.Classifiers;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Diarista.Models
{
    public class Perfil
    {
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