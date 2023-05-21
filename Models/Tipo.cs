using System.ComponentModel.DataAnnotations;

public class Tipo
{
    public int Id { get; set; }
    [Required]
    public string? Nome { get; set; }
}