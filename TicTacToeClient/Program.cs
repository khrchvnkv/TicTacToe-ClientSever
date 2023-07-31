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
var gameLobby = new GameLobbyService();

if (createGame)
{
    // Create
    var game = await gameFactory.CreateGame(loginUser, gameLobby);
    await gameLobby.StartLobbyListening();
    Console.WriteLine($"Your game ID: {game.Id}. Waiting another player...");

    while (!game.IsInitialized)
    {
        Thread.Sleep(200);
    }

    loginUser.CurrentTurn = true;
    loginUser.IsAdmin = true;
    game.StartGame();
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
        game = await gameFactory.JoinGame(gameLobby, gameId, loginUser);
        if (game is not null) break;
    } while (true);

    Console.WriteLine($"Joined to {game.Id} game");

    await gameLobby.StartLobbyListening();

    await gameLobby.SendMessageToClientsAndStartGame(game.Id);
    game.IsInitialized = true;
    
    loginUser.CurrentTurn = false;
    loginUser.IsAdmin = false;
    
    game.StartGame();
}
    