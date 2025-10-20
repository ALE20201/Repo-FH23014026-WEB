using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Configuración JSON (dejamos nombres tal como los definimos)
builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.PropertyNamingPolicy = null;
});

// Swagger (Swashbuckle)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "PP3 - Minimal API",
        Version = "v1",
        Description = "API para la tarea PP3 - Include / Replace / Erase"
    });
});

var app = builder.Build();

// Habilitar swagger UI en todos los entornos (puedes restringir a Development si prefieres)
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "PP3 v1");
    c.RoutePrefix = "swagger"; // Swagger UI en /swagger
});

// --- Endpoint raíz: redirige a Swagger UI ---
app.MapGet("/", () => Results.Redirect("/swagger")).WithName("RootRedirect");

// --- Helpers ---
static IResult Error(string message)
{
    var body = new Dictionary<string, string> { { "error", message } };
    return Results.Json(body, statusCode: 400);
}

static bool ReadXmlHeader(HttpRequest req)
{
    if (req.Headers.TryGetValue("xml", out var vals))
    {
        var raw = vals.ToString();
        if (bool.TryParse(raw, out var parsed)) return parsed;
        if (raw == "1") return true;
        if (raw == "0") return false;
    }
    return false;
}

static string BuildResultXml(string ori, string neu)
{
    var sb = new StringBuilder();
    sb.AppendLine(@"<?xml version=""1.0"" encoding=""utf-16""?>");
    sb.AppendLine(@"<Result xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">");
    sb.AppendLine($"    <Ori>{EscapeXml(ori)}</Ori>");
    sb.AppendLine($"    <New>{EscapeXml(neu)}</New>");
    sb.AppendLine("</Result>");
    return sb.ToString();
}

static string EscapeXml(string s) => SecurityElement.Escape(s) ?? string.Empty;

// --- Endpoint /include (POST) ---
// Route: /include/{position}
// position: int >= 0 (route)
// value: query (string, length > 0)
// text: form (string, length > 0)
// xml: optional header
app.MapPost("/include/{position:int}", async (HttpRequest req, int position) =>
{
    if (position < 0) return Error("'position' must be 0 or higher");

    var value = req.Query["value"].ToString();
    if (string.IsNullOrWhiteSpace(value)) return Error("'value' cannot be empty");

    if (!req.HasFormContentType) return Error("'text' must be provided as form data");
    var form = await req.ReadFormAsync();
    var text = form["text"].ToString();
    if (string.IsNullOrWhiteSpace(text)) return Error("'text' cannot be empty");

    var words = text.Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();

    if (position >= words.Count)
    {
        words.Add(value);
    }
    else
    {
        words.Insert(position, value);
    }

    var resultText = string.Join(" ", words);
    var xmlRequested = ReadXmlHeader(req);

    if (xmlRequested)
    {
        var xml = BuildResultXml(text, resultText);
        return Results.Content(xml, "application/xml");
    }

    return Results.Json(new { ori = text, @new = resultText });
}).WithName("IncludeEndpoint");

// --- Endpoint /replace (PUT) ---
// Route: /replace/{length}
// length: int > 0
// value: query (string, length > 0)
// text: form (string, length > 0)
app.MapPut("/replace/{length:int}", async (HttpRequest req, int length) =>
{
    if (length <= 0) return Error("'length' must be greater than 0");

    var value = req.Query["value"].ToString();
    if (string.IsNullOrWhiteSpace(value)) return Error("'value' cannot be empty");

    if (!req.HasFormContentType) return Error("'text' must be provided as form data");
    var form = await req.ReadFormAsync();
    var text = form["text"].ToString();
    if (string.IsNullOrWhiteSpace(text)) return Error("'text' cannot be empty");

    var words = text.Split(' ', StringSplitOptions.None).ToList();

    for (int i = 0; i < words.Count; i++)
    {
        // Longitud exacta basada en la representación actual de la "palabra" (incluye puntuación)
        if (words[i].Length == length)
        {
            words[i] = value;
        }
    }

    var resultText = string.Join(" ", words);
    var xmlRequested = ReadXmlHeader(req);

    if (xmlRequested)
    {
        var xml = BuildResultXml(text, resultText);
        return Results.Content(xml, "application/xml");
    }

    return Results.Json(new { ori = text, @new = resultText });
}).WithName("ReplaceEndpoint");

// --- Endpoint /erase (DELETE) ---
// Route: /erase/{length}
// length: int > 0
// text: form (string, length > 0)
app.MapDelete("/erase/{length:int}", async (HttpRequest req, int length) =>
{
    if (length <= 0) return Error("'length' must be greater than 0");

    if (!req.HasFormContentType) return Error("'text' must be provided as form data");
    var form = await req.ReadFormAsync();
    var text = form["text"].ToString();
    if (string.IsNullOrWhiteSpace(text)) return Error("'text' cannot be empty");

    var words = text.Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();
    var kept = words.Where(w => w.Length != length).ToList();
    var resultText = string.Join(" ", kept);

    var xmlRequested = ReadXmlHeader(req);

    if (xmlRequested)
    {
        var xml = BuildResultXml(text, resultText);
        return Results.Content(xml, "application/xml");
    }

    return Results.Json(new { ori = text, @new = resultText });
}).WithName("EraseEndpoint");

// Run
app.Run();

