namespace TicTacToeClient.UserInputParser
{
    public abstract class UserInputParser
    {
        protected readonly string QuestionText;

        public UserInputParser(string questionText)
        {
            QuestionText = questionText;
        }
    }
}