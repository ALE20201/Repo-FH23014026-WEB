# PP1 - Suma de números naturales (C# .NET 8)

**Nombre:** Alex Daniel Monge Arias
**Carné:**  FH23014026

## Comandos dotnet utilizados (CLI)
- `dotnet new sln -n PP1Solution`
- `dotnet new console -n PP1 -f net8.0`
- `dotnet sln PP1Solution.sln add PP1/PP1.csproj`
- `dotnet run --project PP1`

## Estructura del repositorio (carpeta PP1)
- `PP1/Program.cs` 
- `PP1/README.md` 
- `PP1/.gitignore` 

## Fuentes y snippets consultados
- AlphaCodingSkills: [(https://www.alphacodingskills.com/cs/pages/cs-program-calculate-sum-of-natural-numbers.php?)]

- TutorialsPoint : [(https://www.tutorialspoint.com/chash-program-to-find-the-sum-of-first-n-natural-numbers?)]

## Prompts y respuestas de chatbots utilizados
- ChatGPT : [(https://chatgpt.com/share/68d02f2f-d578-8006-b19c-784cda1ed968)] 

## Preguntas del enunciado — respuestas
1. **¿Por qué difieren los valores (n y sum) entre métodos y estrategias?**  
   pasa por cómo C# maneja los enteros. Al usar int de 32 bits, si el número se hace muy grande, se produce un overflow, haciendo que el valor se vuelva negativo. Con la fórmula n*(n+1)/2, primero se multiplican y después dividen, así que el desbordamiento puede ocurrir antes que en la suma paso a paso. Además, si se busca el último número positivo y lo hacés de menor a mayor o de mayor a menor, el punto donde se produce el overflow aparece en momentos distintos. Por eso los métodos y estrategias pueden dar números diferentes aunque estén calculando la misma suma.

2. **¿Qué pasaría si se usa la versión recursiva `SumRec`?**  
   En números pequeños, SumRec da lo mismo que la suma iterativa (SumIte). El problema es que C# no maneja bien recursión muy profunda: si n es grande, se produce un StackOverflowException porque la pila se llena. Por eso, para buscar hasta valores máximos no conviene usar la recursión; sí sirve para probar con números pequeños.