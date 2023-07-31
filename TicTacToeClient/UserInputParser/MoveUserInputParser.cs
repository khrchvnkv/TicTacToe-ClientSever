namespace TicTacToeClient.UserInputParser
{
    public class MoveUserInputParser : UserInputParser
    {
        private const int MinInputValue = 1;
        private const int MaxInputValue = 9;

        public MoveUserInputParser(string questionText) : base(questionText) { }
        
        public int ParseMoveInput(in string boardView)
        {
            var result = 0;
            do
            {
                Console.Clear();
                Console.WriteLine(boardView);
                Console.WriteLine(QuestionText);
                var input = Console.ReadLine();
                if (Int32.TryParse(input, out result) &&
                    result is >= MinInputValue and <= MaxInputValue)
                {
                    break;
                }
            } while (true);

            return result;
        }
    }
}