var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var farmacias = new List<Farmacia>
{
    new Farmacia {Id = 1, Title = "Dipirona", Descricao = "Remedio para dor de cabeca"},
    new Farmacia {Id = 2, Title = "Novalgina", Descricao = "Medicamento analgésico e antitérmico"},
    new Farmacia {Id = 3, Title = "Dorflex", Descricao = "Medicamento que possui ação analgésica e relaxante muscular"}
};

app.MapGet("/farmacia", () =>
{
    return farmacias;
});

app.MapGet("/farmacia/{id}", (int id) =>
{
    var farmacia = farmacias.Find(books => books.Id == id);

    if (farmacia is null)
        return Results.NotFound("Nao encontrado");

    return Results.Ok(farmacia);
});

app.MapPost("/farmacia", (Farmacia farmacia) => {
    farmacias.Add(farmacia);
    return farmacias;
});

app.MapPut("/farmacia/{id}", (Farmacia updateBook,int id) =>
{
    var farmacia = farmacias.Find(books => books.Id == id);

    if (farmacia is null)
        return Results.NotFound("Nao encontrado");

    farmacia.Title = updateBook.Title;
    farmacia.Descricao = updateBook.Descricao;

    return Results.Ok(farmacia);
});

app.MapDelete("/farmacia/{id}", (int id) =>
{
    var farmacia = farmacias.Find(books => books.Id == id);

    if (farmacia is null)
        return Results.NotFound("Nao encontrado");

    farmacias.Remove(farmacia);

    return Results.Ok(farmacia);
});

app.Run();


class Farmacia
{
    public int Id { get; set; }
    public  string Title { get; set; }
    public string Descricao { get; set; }
}