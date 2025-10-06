using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace BinaryOperationsApp.Models
{
    public class BinaryModel
    {
        //   Entradas del usuario
        [Display(Name = "a")]
        [Required(ErrorMessage = "El valor de 'a' es obligatorio.")]
        [RegularExpression("^[01]+$", ErrorMessage = "El valor de 'a' solo puede contener 0 y 1.")]
        [StringLength(8, MinimumLength = 2, ErrorMessage = "El valor de 'a' debe tener entre 2 y 8 caracteres.")]
        public string A { get; set; } = string.Empty;

        [Display(Name = "b")]
        [Required(ErrorMessage = "El valor de 'b' es obligatorio.")]
        [RegularExpression("^[01]+$", ErrorMessage = "El valor de 'b' solo puede contener 0 y 1.")]
        [StringLength(8, MinimumLength = 2, ErrorMessage = "El valor de 'b' debe tener entre 2 y 8 caracteres.")]
        public string B { get; set; } = string.Empty;


        //  Binarios normalizados
        public string BinAByte { get; set; } = "00000000";
        public string BinBByte { get; set; } = "00000000";

        //  Conversiones individuales de A y B
        public string AOct { get; set; } = string.Empty;
        public string ADec { get; set; } = string.Empty;
        public string AHex { get; set; } = string.Empty;

        public string BOct { get; set; } = string.Empty;
        public string BDec { get; set; } = string.Empty;
        public string BHex { get; set; } = string.Empty;


        //  Resultados de operaciones binaro
        public string AndBin { get; set; } = string.Empty;
        public string OrBin { get; set; } = string.Empty;
        public string XorBin { get; set; } = string.Empty;


        //  Resultados aritméticos
        public string SumBin { get; set; } = string.Empty;
        public string MulBin { get; set; } = string.Empty;

        //  Conversiones de resultados
        public string AndOct { get; set; } = string.Empty;
        public string AndDec { get; set; } = string.Empty;
        public string AndHex { get; set; } = string.Empty;

        public string OrOct { get; set; } = string.Empty;
        public string OrDec { get; set; } = string.Empty;
        public string OrHex { get; set; } = string.Empty;

        public string XorOct { get; set; } = string.Empty;
        public string XorDec { get; set; } = string.Empty;
        public string XorHex { get; set; } = string.Empty;

        public string SumOct { get; set; } = string.Empty;
        public string SumDec { get; set; } = string.Empty;
        public string SumHex { get; set; } = string.Empty;

        public string MulOct { get; set; } = string.Empty;
        public string MulDec { get; set; } = string.Empty;
        public string MulHex { get; set; } = string.Empty;

        // Validación personalizada
        public IEnumerable<ValidationResult> Validate()
        {
            var results = new List<ValidationResult>();

            // Validar binarios
            if (!Regex.IsMatch(A, "^[01]+$"))
                results.Add(new ValidationResult("El valor de 'a' solo puede contener los caracteres 0 y 1.", new[] { "A" }));

            if (!Regex.IsMatch(B, "^[01]+$"))
                results.Add(new ValidationResult("El valor de 'b' solo puede contener los caracteres 0 y 1.", new[] { "B" }));

            // Validar múltiplos de 2
            if (A.Length % 2 != 0)
                results.Add(new ValidationResult("La longitud de 'a' debe ser múltiplo de 2.", new[] { "A" }));

            if (B.Length % 2 != 0)
                results.Add(new ValidationResult("La longitud de 'b' debe ser múltiplo de 2.", new[] { "B" }));

            // Validar máximo 8
            if (A.Length > 8)
                results.Add(new ValidationResult("El valor de 'a' no puede exceder 8 caracteres.", new[] { "A" }));

            if (B.Length > 8)
                results.Add(new ValidationResult("El valor de 'b' no puede exceder 8 caracteres.", new[] { "B" }));

            return results;
        }

        // Métodos seguros de conversión
        public int SafeBinaryToInt(string bin)
        {
            if (string.IsNullOrWhiteSpace(bin) || !Regex.IsMatch(bin, "^[01]+$"))
                return 0;

            try
            {
                return Convert.ToInt32(bin, 2);
            }
            catch
            {
                return 0;
            }
        }

        public string PadToByte(string bin)
        {
            if (string.IsNullOrWhiteSpace(bin))
                return "00000000";

            if (bin.Length > 8)
                bin = bin.Substring(bin.Length - 8); // solo los 8 bits menos significativos

            return bin.PadLeft(8, '0');
        }
    }
}
