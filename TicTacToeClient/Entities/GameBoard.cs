using System.Text;

namespace TicTacToeClient.Entities
{
    public class GameBoard
    {
        private const int BoardCellsCount = 9;
        private const int CellsInLineCount = 3;
        
        public void ShowGameBoard(in HashSet<int> user1_moves, in HashSet<int> user2_moves)
        {
            Console.Clear();
            var sb = new StringBuilder();
            var exampleSb = new StringBuilder();
            
            char[] moves = new char[BoardCellsCount];
            for (int i = 0; i < moves.Length; i++)
            {
                if (user1_moves.Contains(i + 1))
                {
                    moves[i] = 'X';
                    continue;
                }
                if (user2_moves.Contains(i + 1))
                {
                    moves[i] = 'O';
                    continue;
                }
                moves[i] = ' ';
            }

            for (int i = 0; i < moves.Length; i++)
            {
                if ((i + 1) % CellsInLineCount == 0)
                {
                    sb.Append($"{moves[i]}");
                    sb.AppendLine();
                    
                    exampleSb.Append($"{i + 1}");
                    exampleSb.AppendLine();
                }
                else
                {
                    sb.Append($"{moves[i]}|");
                    exampleSb.Append($"{i + 1}|");
                }
            }

            Console.WriteLine(sb.ToString());
            exampleSb.AppendLine();
            Console.WriteLine(exampleSb.ToString());
        }
    }
}