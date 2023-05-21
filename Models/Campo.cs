using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

public class Campo
{
    public int Id { get; set; }
    public string? Nome { get; set; }
    [JsonIgnore]
    string? TipoNome { get; set; }
    [ForeignKey("Tipo")]
    [Required]
    public int TipoId { get; set; }
    [Required]
    public Tipo? Tipo { get; set; }
    IList<Formulario>? Formularios { get; set; }
}