using System.ComponentModel.DataAnnotations.Schema;

public class RegistroInfoViewModel
{
    public int Id { get; set; }
    [ForeignKey("RegistroViewModel")]
    public int RegistroId { get; set; }
    public RegistroViewModel? Registro { get; set; }

    [ForeignKey("Campo")]
    public int CampoId { get; set; }
    public Campo? Campo { get; set; }
    public string? Dado { get; set; }
}