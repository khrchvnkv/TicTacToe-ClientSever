namespace TicTacToeClient.UserInputParser
{
    public class BoolUserInputParser : UserInputParser
    {
        private const string YesText = "Y";
        private const string NoText = "N";

        public BoolUserInputParser(string questionText) : base(questionText) { }

        public bool ParseBoolInput()
        {
            var result = false;
            do
            {
                Console.Clear();
                Console.WriteLine(QuestionText);
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