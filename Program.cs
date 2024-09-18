
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configurações Swagger
 builder.Services.AddEndpointsApiExplorer();
 builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>();
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/", () => "Lista de Contatos");

app.MapGet("/contatos", async (AppDbContext db) =>
    await db.Contatos.ToListAsync());

app.MapGet("/contatos/{id}", async (int id, AppDbContext db) => 
    await db.Contatos.FindAsync(id)
      is Contato contato
        ? Results.Ok(contato)
          : Results.NotFound());


app.MapPost("/contatos", async (Contato contato, AppDbContext db) => {
    db.Contatos.Add(contato);
    await db.SaveChangesAsync();
    return Results.Created($"/contatos/{contato.Id}", contato);
});

app.MapPut("contatos/{id}", async (int id, Contato contatoAlterado, AppDbContext db) => 
{
    var contato = await db.Contatos.FindAsync(id);
    if (contato is null) return Results.NotFound();

    contato.Nome = contatoAlterado.Nome;
    contato.Email = contatoAlterado.Email;
    contato.NumeroTelefone = contatoAlterado.NumeroTelefone;

    await db.SaveChangesAsync();

    return Results.NoContent();

});

app.MapDelete("contatos/{id}", async (int id, AppDbContext db) =>
{
    if(await db.Contatos.FindAsync(id) is Contato contato){

        db.Contatos.Remove(contato);
        await db.SaveChangesAsync();
        return Results.NoContent();
    }
    return Results.NotFound();
 
});

app.MapGet("/produtos", () => "Produtos");
app.MapGet("/pessoas", () => "Pessoas");
app.MapGet("/pessoas/{id}", () => "Pessoa 1");

app.MapPost("/pessoas", () => "POST pessoa");
app.MapPost("/produtos", () => "POST produto");

app.MapPut("/pessoas/{id}", () => "PUT pessoa");
app.MapPut("/produtos/{id}", () => "PUT produto");

app.MapDelete("/pessoas/{id}", () => "DELETE pessoa");
app.MapDelete("/produtos/{id}", () => "DELETE produto");


app.Run();
