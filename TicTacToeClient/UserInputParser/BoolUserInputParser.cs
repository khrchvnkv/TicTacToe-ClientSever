namespace TicTacToeClient.UserInputParser
{
    public class BoolUserInputParser
    {
        private const string YesText = "Y";
        private const string NoText = "N";

        private readonly string _questionText;

        public BoolUserInputParser(string questionText)
        {
            _questionText = questionText;
        }

        public bool ParseBoolInput()
        {
            var result = false;
            do
            {
                Console.Clear();
                Console.WriteLine(_questionText);
                var input = Console.ReadLine();
                if (input == YesText)
                {
                    result = true;
                    break;
                }

                if (input == NoText)
                {
                    break;
                }
            } while (true);

            return result;
        }
    }
}