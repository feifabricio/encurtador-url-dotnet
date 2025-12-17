using System;
namespace EncurtadorUrl.Dominio.Entidades;

public class UrlEncurtada
{
    public int Id { get; set; }
    public string UrlOriginal { get; set; } = null!;
    public string CodigoCurto { get; set; } = null!;
    public DateTime DataCriacao { get; set; } = DateTime.UtcNow;
}