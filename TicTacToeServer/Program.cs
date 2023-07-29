using Microsoft.EntityFrameworkCore;
using TicTacToe.Core.IConfiguration;
using TicTacToe.Data;
using TicTacToe.Hubs;
using TicTacToe.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("Default")));
builder.Services.AddSignalR();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<LoginService>();
builder.Services.AddScoped<GameLobbyService>();

var app = builder.Build();

app.MapPost("/api/login/{username}", async (LoginService loginService, string username) =>
{
    try
    {
        var user = await loginService.CreateUserOrLogin(username);
        return Results.Created($"/api/user/{username}", user);
    }
    catch (Exception e)
    {
        Console.WriteLine($"User authorization error: {e.Message}");
        return Results.StatusCode(500);
    }
});

app.MapPost("/api/create-game/{userGuid}", async (GameLobbyService gameLobbyService, Guid userGuid) =>
{
    try
    {
        var game = await gameLobbyService.CreateGame(userGuid);
        return Results.Created($"/api/games/{game.Id}", game);
    }
    catch (Exception e)
    {
        Console.WriteLine($"Game creating server error: {e.Message}");
        return Results.StatusCode(500);
    }
});

app.MapPatch("/api/join-game/{gameGuid}/{userGuid}", async (GameLobbyService gameLobbyService, 
    Guid gameGuid, Guid userGuid) =>
{
    try
    {
        var game = await gameLobbyService.JoinGame(gameGuid, userGuid);
        if (game is null)
            return Results.NotFound("Game with this ID does not exist or there are no free places in the game");

        return Results.NoContent();
    }
    catch (Exception e)
    {
        Console.WriteLine($"Game joining server error: {e.Message}");
        return Results.StatusCode(500);
    }
});
app.MapHub<GameHub>("/game-hub");

app.Run();