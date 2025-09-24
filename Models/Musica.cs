using System;

namespace API_spotify_att.Models
{
    public class Musica
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Artista { get; set; } = string.Empty;
        public string ArquivoMP3 { get; set; } = string.Empty;
        public string ImagemCapa { get; set; } = string.Empty;
        public DateTime DataPostagem { get; set; }
    }
}
