using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Formulario
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Preencha o Nome!")]
    public string? Nome { get; set; }
    public IList<Campo>? Campos { get; set; }
}