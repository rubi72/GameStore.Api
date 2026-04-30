using GameStore.Api;
using GameStore.Api.Dtos;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
const string GetGameEndpointName = "GetGame";

List<GameDto> games = [
  new (1,
      "Street Fighter II",
      "Fighting",
      19.99M,
      new DateOnly(1992, 7, 15)),
  new(2,
      "Final Fantas VII Rebirth",
      "RPG",
      69.99M,
      new DateOnly(2024, 2, 29)),
  new(3,
      "Astro Bot",
      "Platformer",
      59.99M,
      new DateOnly(2024, 9 , 6))
];

// GET /games 
app.MapGet("/games", () => games);


// GET /games/1
app.MapGet("/games/{id}", (int id) =>
{
    var game = games.Find(game => game.Id == id);
    
    return game == null ? Results.NotFound() : Results.Ok(game);
}).WithName(GetGameEndpointName);


// POST /games
app.MapPost("/games", (CreateGameDto newGame) =>
{
    GameDto game1 = new(
        games.Count + 1,
        newGame.Name,
        newGame.Genre,
        newGame.Price,
        newGame.ReleaseDate
    );
    games.Add(game1);

    return Results.CreatedAtRoute(GetGameEndpointName, new { id = game1.Id }, newGame);
});


// PUT /games/1
app.MapPut("/games/{id}", (int id, UpdateGameDto updatedGame) =>
{
    var index = games.FindIndex(game2 => game2.Id == id);

    if(index == -1)
        return Results.NotFound();

    games[index] = new GameDto(
        id,
        updatedGame.Name,
        updatedGame.Genre,
        updatedGame.Price,
        updatedGame.ReleaseDate
    );
    
    return Results.NoContent();
});


// GET /games/1/nombre/HolaQuePasa
app.MapGet("/games/{id}/nombre/{newName}", (int id, string newName) =>
{
    var index = games.FindIndex(game => game.Id == id);

    if (index == -1)
        return Results.NotFound();

    var game = games[index];
    games[index] = new GameDto(id, newName, game.Genre, game.Price, game.ReleaseDate);
    return Results.Ok(games[index]);
});


// DELETE /games/1
app.MapDelete("/games/{id}", (int id) =>
{
    var deleteGame = games.RemoveAll(gamesT => gamesT.Id == id);

    if(deleteGame == 0)
        return Results.NotFound();

    return Results.NoContent();
});


app.Run();

// WebApplication.CreateBuilder(args).Build().MapGet("/", () => "Hello World!");
