using System.Threading.Tasks;
using EncurtadorUrl.Dominio.Entidades;

namespace EncurtadorUrl.Aplicacao.Interfaces;

public interface IServicoEncurtadorUrl
{
    Task<UrlEncurtada> CriarAsync(string urlOriginal, string? alias);
    Task<UrlEncurtada?> ObterPorCodigoAsync(string codigoCurto);
}