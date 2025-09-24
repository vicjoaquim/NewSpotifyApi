using API_spotify_att.Context;
using API_spotify_att.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_spotify_att.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MusicasController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IWebHostEnvironment _env;

    public MusicasController(AppDbContext context, IWebHostEnvironment env)
    {
        _context = context;
        _env = env;
    }

    [HttpGet("Listar")]
    public async Task<IActionResult> Listar()
    {
        var musicas = await _context.Musicas
            .OrderByDescending(m => m.DataPostagem)
            .Select(m => new
            {
                m.Id,
                nomeMusica = m.Nome,
                artistaMusica = m.Artista,
                arquivo = m.ArquivoMP3,
                imagem = m.ImagemCapa,
                dataPostagem = m.DataPostagem
            })
            .ToListAsync();

        return Ok(musicas);
    }

    [HttpPost("Cadastrar")]
    public async Task<IActionResult> CadastrarMusica([FromForm] MusicaDTO dto)
    {
        if (dto.Arquivo == null || dto.Arquivo.Length == 0)
            return BadRequest("Arquivo de áudio é obrigatório");

        var uploadsPath = Path.Combine(_env.WebRootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot"), "Uploads");

        var arquivoNome = $"{Guid.NewGuid()}_{dto.Arquivo.FileName}";
        var filePathMusica = Path.Combine(uploadsPath, arquivoNome);
        await using var stream = new FileStream(filePathMusica, FileMode.Create);
        await dto.Arquivo.CopyToAsync(stream);

        string arquivoImagemUrl = string.Empty;
        if (dto.Imagem != null && dto.Imagem.Length > 0)
        {
            var imagemNome = $"{Guid.NewGuid()}_{dto.Imagem.FileName}";
            var filePathImagem = Path.Combine(uploadsPath, imagemNome);
            await using var streamImg = new FileStream(filePathImagem, FileMode.Create);
            await dto.Imagem.CopyToAsync(streamImg);
            arquivoImagemUrl = $"/Uploads/{imagemNome}";
        }

        var musica = new Musica
        {
            Nome = dto.NomeMusica,
            Artista = dto.ArtistaMusica,
            ArquivoMP3 = $"/Uploads/{arquivoNome}",
            ImagemCapa = arquivoImagemUrl,
            DataPostagem = DateTime.Now
        };

        _context.Musicas.Add(musica);
        await _context.SaveChangesAsync();

        return Ok(new
        {
            mensagem = "Música cadastrada com sucesso!",
            nomeMusica = musica.Nome,
            artistaMusica = musica.Artista,
            arquivo = musica.ArquivoMP3,
            imagem = musica.ImagemCapa,
            dataPostagem = musica.DataPostagem
        });
    }
}
