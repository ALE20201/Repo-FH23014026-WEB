using Microsoft.AspNetCore.Mvc;
using BinaryOperationsApp.Models;
using BinaryOperationsApp.Services;

namespace BinaryOperationsApp.Controllers
{
    public class HomeController : Controller
    {
        // GET /
        [HttpGet("/")]
        public IActionResult Index()
        {
            return View(new BinaryModel());
        }

        // POST /
        [HttpPost("/")]
        public IActionResult Index(BinaryModel model)
        {
            // Validaciones por DataAnnotations (atributo personalizado)
            if (!ModelState.IsValid)
            {
                // Copiar errores a ViewBag para la vista
                ViewBag.Error = "Hay errores en el formulario. Corrija las entradas.";
                return View(model);
            }

            // Normalizar: evitar nulls y quitar espacios
            model.A = (model.A ?? string.Empty).Trim();
            model.B = (model.B ?? string.Empty).Trim();

            // Preparar binarios con padding a 8 bits para mostrar
            model.BinAByte = model.A.PadLeft(8, '0');
            model.BinBByte = model.B.PadLeft(8, '0');

            // Operaciones binarias (métodos sobre strings)
            model.AndBin = BinaryOps.And(model.A, model.B);
            model.OrBin  = BinaryOps.Or(model.A, model.B);
            model.XorBin = BinaryOps.Xor(model.A, model.B);

            // Conversión segura a int
            int intA = BinaryOps.BinToInt(model.A);
            int intB = BinaryOps.BinToInt(model.B);

            // Aritmética
            int sum = intA + intB;
            int mul = intA * intB;

            model.SumBin = BinaryOps.IntToBin(sum);
            model.MulBin = BinaryOps.IntToBin(mul);

            // Rellenar representaciones para la tabla
            // Para a y b (usar intA/intB para evitar Convert en la view)
            model.AOct = BinaryOps.IntToOct(intA);
            model.ADec = intA.ToString();
            model.AHex = BinaryOps.IntToHex(intA);

            model.BOct = BinaryOps.IntToOct(intB);
            model.BDec = intB.ToString();
            model.BHex = BinaryOps.IntToHex(intB);

            // AND
            int andInt = BinaryOps.BinToInt(model.AndBin);
            model.AndOct = BinaryOps.IntToOct(andInt);
            model.AndDec = andInt.ToString();
            model.AndHex = BinaryOps.IntToHex(andInt);

            // OR
            int orInt = BinaryOps.BinToInt(model.OrBin);
            model.OrOct = BinaryOps.IntToOct(orInt);
            model.OrDec = orInt.ToString();
            model.OrHex = BinaryOps.IntToHex(orInt);

            // XOR
            int xorInt = BinaryOps.BinToInt(model.XorBin);
            model.XorOct = BinaryOps.IntToOct(xorInt);
            model.XorDec = xorInt.ToString();
            model.XorHex = BinaryOps.IntToHex(xorInt);

            // SUM
            model.SumOct = BinaryOps.IntToOct(sum);
            model.SumDec = sum.ToString();
            model.SumHex = BinaryOps.IntToHex(sum);

            // MUL
            model.MulOct = BinaryOps.IntToOct(mul);
            model.MulDec = mul.ToString();
            model.MulHex = BinaryOps.IntToHex(mul);

            return View(model);
        }
    }
}
