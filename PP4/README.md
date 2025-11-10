# PP1 - Suma de números naturales (C# .NET 8)

**Nombre:** Alex Daniel Monge Arias
**Carné:**  FH23014026

## Comandos dotnet utilizados (CLI)
- `dotnet new sln -n PP4Solution`
- `dotnet new console -n PP4App`
- `dotnet sln PP4Solution.sln add PP4App/PP4App.csproj`
- `dotnet add package Microsoft.EntityFrameworkCore.Sqlite --version 9.0.0`
- `dotnet add package Microsoft.EntityFrameworkCore.Design --version 9.0.0`
- `dotnet add package Microsoft.EntityFrameworkCore.Tools --version 9.0.0`
- `dotnet add package CsvHelper --version 30.0.0`
- `dotnet tool install --global dotnet-ef`
- `dotnet ef migrations add InitialCreate`
- `dotnet ef database update`
- `dotnet run`

## Estructura del repositorio (carpeta PP4)
- `PP4` 
    - `PP4Solution.sln` 
    - `PP4App` 
        - `PP4App.csproj`
        - `Program.cs`
    - `README.md`
          

## Fuentes y snippets consultados
- Microsoft Ignite: [(https://learn.microsoft.com/es-es/aspnet/core/web-api/?view=aspnetcore-9.0)]

- hdeleon.net. "CODE FIRST con Entity Framework en .NET 🦗" : [(https://youtu.be/x1zjZUZJ6UA?si=rweUdC0GM9cJdjED)]

## Prompts y respuestas de chatbots utilizados
- ChatGPT : [("Me ayudas con un error CS0246 en ASP.NET WebAPI que menciona que 'FromForm' no se encuentra.")]
- ChatGPT : [("Cómo establecer adecuadamente el ApplicationDbContext para EF Core utilizando SQLite (Data Source=app.db).")] 
- ChatGPT : [("Cómo probar mi Web API desde CMD con curl.")]
- ChatGPT : [("Qué hacer si dotnet ef migrations add InitialCreate produce un error de DbContextOptions.")]


## Preguntas de la PP4 
1. **¿Cómo cree que resultaría el uso de la estrategia de Code First para crear y actualizar una base de datos de tipo NoSQL (como por ejemplo MongoDB)? ¿Y con Database First? ¿Cree que habría complicaciones con las Foreign Keys?**  
   Usar Code First en MongoDB o NoSQL sería "complicado" y habría que utilizar librerías externas para acercarnos a ese resultado. Database First sí creo que sería directamente imposible o directamente no valdría la pena el esfuerzo. En primer lugar no existen las Foreign Keys en NoSQL y todo se debería manejar a través de la aplicación y no por medio del motor de la base de datos.

2. **¿Cuál carácter, además de la coma (,) y el Tab (\t), se podría usar para separar valores en un archivo de texto con el objetivo de ser interpretado como una tabla (matriz)? ¿Qué extensión le pondría y por qué? Por ejemplo: Pipe (|) con extensión .pipe.
**  
    Los caracteres (: con extensión .csv2) y (; con extensión .ssv) porque cumplen con las mismas características que la pipe:
    -Fácil de reconocer.
    -No se frecuenta.
    -Mantiene su integridad en las conversiones de formato.
