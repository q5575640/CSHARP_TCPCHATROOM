using System.Collections.Generic;

namespace TCPGameChatProject
{
    public class GameSession
    {
        public string GameType { get; }
        public string PlayerA { get; }
        public string PlayerB { get; }
        private Dictionary<string, string> choices = new Dictionary<string, string>();

        public GameSession(string gameType, string playerA, string playerB)
        {
            GameType = gameType;
            PlayerA = playerA;
            PlayerB = playerB;
        }

        public void RecordChoice(string player, string choice)
        {
            choices[player] = choice;
        }

        public bool IsComplete()
        {
            return choices.Count == 2;
        }

        public string GetResult(string player)
        {
            string choiceA = choices[PlayerA];
            string choiceB = choices[PlayerB];

            if (GameType == "RPS")
            {
                string result = JudgeRPS(choiceA, choiceB);
                return (player == PlayerA) ? result : ReverseResult(result);
            }
            else if (GameType == "DICE")
            {
                int a = int.Parse(choiceA);
                int b = int.Parse(choiceB);
                string result = JudgeDice(a, b);
                return (player == PlayerA) ? result : ReverseResult(result);
            }
            return "未知結果";
        }

        private string JudgeRPS(string a, string b)
        {
            if (a == b) return "平手";
            if ((a == "ROCK" && b == "SCISSORS") ||
                (a == "PAPER" && b == "ROCK") ||
                (a == "SCISSORS" && b == "PAPER"))
                return "你贏了";
            return "你輸了";
        }

        private string JudgeDice(int a, int b)
        {
            if (a == b) return "平手";
            if (a > b) return "你贏了";
            return "你輸了";
        }

        private string ReverseResult(string result)
        {
            if (result == "你贏了") return "你輸了";
            if (result == "你輸了") return "你贏了";
            return "平手";
        }
    }
}
