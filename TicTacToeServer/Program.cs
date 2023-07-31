using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TicTacToe.Core.IConfiguration;
using TicTacToe.Data;
using TicTacToe.Hubs;
using TicTacToe.Models;
using TicTacToe.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("Default")));
builder.Services.AddSignalR();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<LoginService>();
builder.Services.AddScoped<GameLobbyService>();

var app = builder.Build();

app.MapPost("/api/login", async (LoginService loginService, [FromBody] string username) =>
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

app.MapPost("/api/create-game", async (GameLobbyService gameLobbyService, [FromBody] User user) =>
{
    try
    {
        var game = await gameLobbyService.CreateGame(user.Id);
        return Results.Created($"/api/games/{game.Id}", game);
    }
    catch (Exception e)
    {
        Console.WriteLine($"Game creating server error: {e.Message}");
        return Results.StatusCode(500);
    }
});

app.MapPost("/api/add-move", async (GameLobbyService gameLobbyService, [FromBody] Move moveData) =>
{
    try
    {
        var gameId = moveData.GameId;
        var userId = moveData.UserId;
        var cellIndex = moveData.CellIndex;
        var move = await gameLobbyService.MakeMove(gameId, userId, cellIndex);
        return Results.Created($"/api/moves/{gameId}/{userId}/{cellIndex}", move);
    }
    catch (Exception e)
    {
        Console.WriteLine($"Move data sending server error: {e.InnerException}");
        return Results.StatusCode(500);
    }
});

app.MapPatch("/api/join-game/{gameGuid}/{userGuid}", async (GameLobbyService gameLobbyService, 
    [FromRoute] Guid gameGuid, [FromRoute] Guid userGuid) =>
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