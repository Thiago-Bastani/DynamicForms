using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

public class Campo
{
    public int Id { get; set; }
    [Required]
    public string? Nome { get; set; }
    string? TipoNome { get; set; }
    [ForeignKey("Tipo")]
    [Required]
    public int TipoId { get; set; }
    public Tipo? Tipo { get; set; }
    IList<Formulario>? Formularios { get; set; }
}