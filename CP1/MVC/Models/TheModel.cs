namespace MVC.Models;

using System.ComponentModel.DataAnnotations;

public class TheModel
{
    //ChatGPT
    [Required(ErrorMessage = "El valor es requerido.")]
    [StringLength(25, MinimumLength = 5, ErrorMessage = "La longitud de la frase debe ser de 5 a 25 caracteres.")]
    public string Phrase { get; set; } = "";

    public Dictionary<char, int> Counts { get; set; } = [];

    public string Lower { get; set; } = "";//CAMBIADO

    public string Upper { get; set; } = "";//CAMBIADO
}
