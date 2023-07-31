namespace TicTacToeClient.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool CurrentTurn { get; set; }
        public bool IsAdmin { get; set; }
    }
}