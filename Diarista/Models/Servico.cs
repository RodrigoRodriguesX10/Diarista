using Diarista.Classifiers;
using System;

namespace Diarista.Models
{
    public class Servico
    {
        public int Id { get; set; }
        public string ContratanteId { get; set; }
        public string DiaristaId { get; set; }
        public int CasaId { get; set; }
        public string Descricao { get; set; }
        public int Avaliacao { get; set; }
        public Perfil Contratante { get; set; }
        public Perfil Diarista { get; set; }
        public Casa Casa { get; set; }
        public StatusServico Status { get; set; }
        public DateTime DataPublicacao { get; set; }
        public DateTime DataContratacao { get; set; }
    }
}