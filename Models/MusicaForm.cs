using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_spotify_att.Models
{
    public class MusicaForm
    {
        public string Nome { get; set; } = string.Empty;
        public string Artista { get; set; } = string.Empty;
        public IFormFile Arquivo { get; set; } = null!;
        public IFormFile? Imagem { get; set; }
    }
}