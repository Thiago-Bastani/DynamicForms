using System.ComponentModel.DataAnnotations;

public class RegistroViewModel
{
    public int Id { get; set; }
    [Required]
    public string? NomeFormulario { get; set; }
    public ICollection<RegistroInfoViewModel>? RegInfo {get; set;}
}