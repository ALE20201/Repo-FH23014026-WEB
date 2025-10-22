using Microsoft.AspNetCore.Mvc;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var list = new List<object>();

app.MapGet("/", () => Results.Redirect("/swagger"));

app.MapPost("/", (HttpRequest request) =>//ChatGPT
{
    // El header opcional xml (por defecto false)
    bool xml = false;

    if (request.Headers.TryGetValue("xml", out var headerValue))
    {
        bool.TryParse(headerValue, out xml);
    }

    if (xml)
    {
        // Retornar XML
        var xmlSerializer = new System.Xml.Serialization.XmlSerializer(typeof(List<object>));
        using var stringWriter = new StringWriter();
        xmlSerializer.Serialize(stringWriter, list);
        return Results.Content(stringWriter.ToString(), "application/xml");
    }

    // Retornar JSON por defecto
    return Results.Ok(list);
});

app.MapPut("/", ([FromForm] int quantity, [FromForm] string type = "") =>//Chatgpt
{
    if (quantity <= 0)
        return Results.BadRequest(new { error = "'quantity' must be higher than zero" });//ChatGPT

    if (type != "int" && type != "float")
        return Results.BadRequest(new { error = "'type' must be 'int' or 'float'" });//ChatGPT

    var random = new Random();
    if (type == "int")
    {
        for (; quantity > 0; quantity--)
        {
            list.Add(random.Next());
        }
    }
    else if (type == "float")
    {
        for (; quantity > 0; quantity--)
        {
            list.Add(random.NextSingle());
        }
    }
    return Results.Ok(list);
}).DisableAntiforgery();

app.MapDelete("/", ([FromForm] int quantity) =>
{
    if (quantity <= 0)
        return Results.BadRequest(new { error = "'quantity' must be higher than zero" });

    if (quantity > list.Count)
        return Results.BadRequest(new { error = $"Cannot remove {quantity} elements. List only has {list.Count}." });

    for (; quantity > 0; quantity--)
    {
        list.RemoveAt(0);
    }
    return Results.Ok(list);
}).DisableAntiforgery();

app.MapPatch("/", () =>//ChatGPT
{
    list.Clear();
    return Results.Ok(new { message = "List cleared successfully" });
});

app.Run();
