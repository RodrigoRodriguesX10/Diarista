namespace Diarista.Models
{
    public class Casa
    {
        public int Id { get; set; }
        public string PerfilId { get; set; }
        public string Endereco { get; set; }
        public string Descricao { get; set; }

        public Perfil Perfil { get; set; }
    }
}