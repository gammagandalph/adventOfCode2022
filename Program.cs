using System.Text;
using static adventOfCode2022.RockPaperScissors;

namespace adventOfCode2022
{
  internal class Program
  {
    static void Main(string[] args)
    {
      Day05();
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

    static void Day04()
    {
      var idRanges = new List<Tuple<Tuple<int, int>, Tuple<int, int>>>();

      string[] inputs = ReadFileLines("./input_day04.txt");
      foreach (var input in inputs)
      {
        idRanges.Add(new Tuple<Tuple<int, int>, Tuple<int, int>>(
          new Tuple<int, int>(Int32.Parse(input.Split(',')[0].Split('-')[0]), Int32.Parse(input.Split(',')[0].Split('-')[1])),
          new Tuple<int, int>(Int32.Parse(input.Split(',')[1].Split('-')[0]), Int32.Parse(input.Split(',')[1].Split('-')[1]))));
      }

      var completeOverlap = 0;
      var someOverlap = 0;
      var n = 0;

      foreach (var idRange in idRanges)
      {
        n++;
        var start1 = idRange.Item1.Item1;
        var end1 = idRange.Item1.Item2;
        var start2 = idRange.Item2.Item1;
        var end2 = idRange.Item2.Item2;

        if (start1 == start2 && end1 == end2) completeOverlap++;
        else if (start1 <= start2 && end1 >= end2) completeOverlap++;
        else if (start2 <= start1 && end2 >= end1) completeOverlap++;

        else if (start1 <= start2 && end1 >= start2) someOverlap++;
        else if (start2 <= start1 && end2 >= start1) someOverlap++;
      }
      Console.WriteLine("N: " + n);
      Console.WriteLine("Complete Overlap: " + completeOverlap);
      Console.WriteLine("Any Overlap: " + (completeOverlap + someOverlap));
    }

    static void Day05()
    {
      var ship = new CargoShip(System.IO.File.ReadAllText(@"./input_day05_starting_stack.txt"));
      var instructions = ReadFileLines(@"./input_day05_instructions.txt").Select(line => new CargoShip.MoveInstruction(line));
      foreach (var instruction in instructions)
      {
        ship.ExecuteMove(instruction);
      }

      var result = new StringBuilder();
      foreach (var stack in ship.CargoStacks)
      {
        result.Append(stack.Peek());
      }
      Console.WriteLine("Expected Result: " + result.ToString());

      var newShip = new CargoShip(System.IO.File.ReadAllText(@"./input_day05_starting_stack.txt"), true);

      var newResult = new StringBuilder();
      foreach (var instruction in instructions)
      {
        newShip.ExecuteMove(instruction);
      }
      foreach (var stack in newShip.CargoStacks)
      {
        newResult.Append(stack.Peek());
      }
      Console.WriteLine("Actual Result: " + newResult.ToString());


    }

    static string[] ReadFileLines(string path)
    {
      return System.IO.File.ReadAllLines(@path);
    }
  }


}


