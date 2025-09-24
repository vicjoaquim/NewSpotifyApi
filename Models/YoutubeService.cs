using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_spotify_att.Models
{
    public class YoutubeService
    {
        public async Task<string> BaixarConverterMP3(string url, string nomeArquivo)
        {
            // Apenas retorna o URL original como "MP3"
            await Task.Delay(100); // simula async
            return url;
        }
    }
}
