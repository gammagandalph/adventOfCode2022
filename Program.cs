using static adventOfCode2022.RockPaperScissors;

namespace adventOfCode2022
{
  internal class Program
  {
    static void Main(string[] args)
    {
      Day03();
    }

    static void Day01()
    {
      Console.WriteLine("Day 1:");
      Calories cal = new Calories();
      Console.WriteLine("Largest Inventory Total: " + cal.GetLargestInventoryTotal());
      Console.WriteLine("Largest Three Inventories Total: " + cal.GetTopThreeInventoriesTotal());
    }

    static void Day02()
    {
      Console.WriteLine("Day 2:");
      var moveDictionary = new Dictionary<string, Move>{
      {"A", Move.ROCK},
      {"B", Move.PAPER},
      {"C", Move.SCISSORS},
      {"X", Move.ROCK},
      {"Y", Move.PAPER},
      {"Z", Move.SCISSORS}};


      var input = ReadFileLines("./input_day02.txt");

      int score = 0;

      foreach (var gameString in input)
      {
        string[] moves = gameString.Split(' ');
        var game = new RockPaperScissors(moveDictionary[moves[0]], moveDictionary[moves[1]]);
        score += game.Score();
      }

      var outcomeDictionary = new Dictionary<string, GameResult>{
        {"X", GameResult.LOSS},
        {"Y", GameResult.DRAW},
        {"Z", GameResult.WIN}
      };


      Console.WriteLine("Total Score with Strategy Guide: " + score);

      score = 0;

      foreach (var gameString in input)
      {
        Move playerMove = 0;
        Move opponentMove = moveDictionary[gameString.Substring(0, 1)];
        GameResult desiredOutcome = outcomeDictionary[gameString.Substring(2, 1)];
        if (desiredOutcome == GameResult.LOSS) GetLosingMove(opponentMove, out playerMove);
        if (desiredOutcome == GameResult.DRAW) GetDrawingMove(opponentMove, out playerMove);
        if (desiredOutcome == GameResult.WIN) GetWinningMove(opponentMove, out playerMove);

        score += new RockPaperScissors(opponentMove, playerMove).Score();
      }

      Console.WriteLine("Total Score with actual Strategy Guide: " + score);
    }

    static void Day03()
    {
      var inventories = ReadFileLines("./input_day03.txt");
      var checker = new PackingChecker(inventories);
      Console.WriteLine("Sum of Priorities: " + checker.GetPrioritiesScore());
      Console.WriteLine("Sum of Badge Priorities: " + checker.GetBadgePriorities());

    }

    static string[] ReadFileLines(string path)
    {
      return System.IO.File.ReadAllLines(@path);
    }
  }


}


