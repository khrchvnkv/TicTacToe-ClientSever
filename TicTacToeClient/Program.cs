using TicTacToeClient.Entities;
using TicTacToeClient.Services;
using TicTacToeClient.UserInputParser;

Console.Clear();
Console.WriteLine("Welcome to TicTacToe Game");
Console.WriteLine("Login or Register");

var username = Console.ReadLine();
var loginService = new LoginService();
var loginUser = await loginService.LoginUser(username);

var createGameBoolInputParser = new BoolUserInputParser("Do you want to create a game? Y/N");
var createGame = createGameBoolInputParser.ParseBoolInput();

var gameFactory = new GameFactory();

if (createGame)
{
    // Create
    var game = await gameFactory.CreateGame(loginUser.Id);
    var gameLobby = new GameLobbyService(game);
    await gameLobby.StartLobbyListening();
    Console.WriteLine($"Your game ID: {game.Id}. Waiting another player...");
}
else
{
    // Join
    Game? game;
    do
    {
        Console.Clear();
        Console.WriteLine("Enter Game ID");
        var gameId = Console.ReadLine();
        game = await gameFactory.JoinGame(gameId, loginUser.Id);
        if (game is not null) break;
    } while (true);

    var gameLobby = new GameLobbyService(game);
    await gameLobby.StartLobbyListening();
    
    Console.WriteLine($"Joined to {game.Id} game");
}
    