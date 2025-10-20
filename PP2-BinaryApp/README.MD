# PP1 - Suma de números naturales (C# .NET 8)

**Nombre:** Alex Daniel Monge Arias
**Carné:**  FH23014026

## Comandos dotnet utilizados (CLI)
- `dotnet new sln -n PP2Solution`
- `dotnet new mvc -n BinaryOperationsApp --framework net8.0`
- `dotnet sln add BinaryOperationsApp/BinaryOperationsApp.csproj`
- `dotnet run --project BinaryOperationsApp`

## Estructura del repositorio (carpeta PP2)
- `PP2/Program.cs` 
- `PP2/README.md` 
- `PP2/.gitignore (esta en el main)` 

## Fuentes y snippets consultados
- Stack Overflow: [(https://stackoverflow.com/questions)]

## Prompts y respuestas de chatbots utilizados
- ChatGPT : [(https://chatgpt.com/share/68d02f2f-d578-8006-b19c-784cda1ed968)] 

## Preguntas del enunciado — respuestas
1. **¿Cuál es el número que resulta al multiplicar, si se introducen los valores máximos permitidos en a y b?**  
Valores máximos permitidos: 11111111 (binario) = 255 (decimal).

Multiplicación: 255 * 255 = 65025 (decimal).

Representaciones:

Binario: 1111111000000001

Octal: 177001

Decimal: 65025

Hexadecimal: FE01

2. **¿Es posible hacer las operaciones en otra capa? ¿en cuál seria?**  
   Model, ya que se podrian encapsular los metodos en el propio modelo, lo cual le convendria a un proyecto de este calibre, la mejor opcion es en una capa de servicios como la utilizada en este.