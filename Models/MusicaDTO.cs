using Microsoft.AspNetCore.Http;

namespace API_spotify_att.Models
{
    public class MusicaDTO
    {
        public string NomeMusica { get; set; } = string.Empty;
        public string ArtistaMusica { get; set; } = string.Empty;
        public IFormFile Arquivo { get; set; } = null!;
        public IFormFile? Imagem { get; set; }
    }
}
