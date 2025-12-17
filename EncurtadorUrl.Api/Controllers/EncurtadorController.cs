using Microsoft.AspNetCore.Mvc;
using EncurtadorUrl.Aplicacao.Interfaces;

namespace EncurtadorUrl.Api.Controllers;

[ApiController]
[Route("api/encurtador")]
public class EncurtadorController : ControllerBase
{
    private readonly IServicoEncurtadorUrl _servico;

    public EncurtadorController(IServicoEncurtadorUrl servico)
    {
        _servico = servico;
    }

    [HttpPost]
    public async Task<IActionResult> Criar([FromBody] RequisicaoCriarUrl request)
    {
        var resultado = await _servico.CriarAsync(request.UrlOriginal, request.Alias);
        var urlCurta = $"{Request.Scheme}://{Request.Host}/{resultado.CodigoCurto}";
        return Ok(new { urlCurta });
    }

    [HttpGet("/{codigo}")]
    public async Task<IActionResult> Redirecionar(string codigo)
    {
        var entidade = await _servico.ObterPorCodigoAsync(codigo);
        if (entidade == null)
            return NotFound();

        return Redirect(entidade.UrlOriginal);
    }
}

public record RequisicaoCriarUrl(string UrlOriginal, string? Alias);