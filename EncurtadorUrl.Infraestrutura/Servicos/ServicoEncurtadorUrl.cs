using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EncurtadorUrl.Aplicacao.Interfaces;
using EncurtadorUrl.Dominio.Entidades;
using EncurtadorUrl.Infraestrutura.Dados;

namespace EncurtadorUrl.Infraestrutura.Servicos;

public class ServicoEncurtadorUrl : IServicoEncurtadorUrl
{
    private static readonly SemaphoreSlim _semaforo = new(1, 1);
    private readonly ContextoAplicacao _contexto;

    public ServicoEncurtadorUrl(ContextoAplicacao contexto)
    {
        _contexto = contexto;
    }

    public async Task<UrlEncurtada> CriarAsync(string urlOriginal, string? alias)
    {
        await _semaforo.WaitAsync();
        try
        {
            var codigoCurto = string.IsNullOrWhiteSpace(alias)
                ? GerarCodigoCurto()
                : alias;

            if (_contexto.UrlsEncurtadas.Any(x => x.CodigoCurto == codigoCurto))
                throw new InvalidOperationException("Alias já está em uso.");

            var entidade = new UrlEncurtada
            {
                UrlOriginal = urlOriginal,
                CodigoCurto = codigoCurto
            };

            _contexto.UrlsEncurtadas.Add(entidade);
            await _contexto.SaveChangesAsync();

            return entidade;
        }
        finally
        {
            _semaforo.Release();
        }
    }

    public Task<UrlEncurtada?> ObterPorCodigoAsync(string codigoCurto)
    {
        var entidade = _contexto.UrlsEncurtadas
            .FirstOrDefault(x => x.CodigoCurto == codigoCurto);

        return Task.FromResult(entidade);
    }

    private static string GerarCodigoCurto()
    {
        return Convert.ToBase64String(Guid.NewGuid().ToByteArray())
            .Replace("/", "_")
            .Replace("+", "-")
            .Substring(0, 6);
    }
}
