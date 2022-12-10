using System.Text;

namespace adventOfCode2022
{
  public class CargoShip
  {
    private List<Stack<char>> _cargoStacks;
    private bool _isNewModel = false;
    public IEnumerable<Stack<char>> CargoStacks { get { return _cargoStacks; } }
    public bool NewModel { get { return _isNewModel; } set { _isNewModel = value; } }

    public CargoShip(string stackInput)
    {
      _cargoStacks = getStacksFromInput(stackInput);

    }
    public CargoShip(List<Stack<char>> stacks)
    {
      _cargoStacks = stacks;
    }
    public CargoShip(string stackInput, bool isNewModel)
    {
      _cargoStacks = getStacksFromInput(stackInput);
      _isNewModel = isNewModel;

    }
    public CargoShip(List<Stack<char>> stacks, bool isNewModel)
    {
      _cargoStacks = stacks;
      _isNewModel = isNewModel;
    }

    private List<Stack<char>> getStacksFromInput(string stackInput)
    {
      var lines = stackInput.Split('\n');
      var numStacks = (int)((lines[0].Length + 1) / 4);

      var list = new List<List<char>>();
      for (int i = 0; i < numStacks; i++)
      {
        list.Add(new List<char>());
      }

      foreach (var line in lines.Take(lines.Count() - 1))
      {
        var i = 0;
        var stackNumber = 0;
        while (i < line.Length)
        {
          if (line[i] == ' ') { i += 3; stackNumber++; }
          else
          {
            list[stackNumber].Add(line.Substring(i, 3)[1]);
            i += 3;
            stackNumber++;
          }
          if (i - 3 != line.Length) i++;
        }
      }

      var stacks = new List<Stack<char>>();

      foreach (var l in list)
      {
        l.Reverse();
        stacks.Add(new Stack<char>(l));
      }

      return stacks;
    }

    public void ExecuteMove(MoveInstruction move)
    {
      if (!_isNewModel) OldMoveExecution(move);
      else NewMoveExecution(move);
    }

    private void NewMoveExecution(MoveInstruction move)
    {
      var tempStack = new Stack<char>();
      for (int i = 0; i < move.Number; i++)
      {
        tempStack.Push(_cargoStacks[move.From - 1].Pop());
      }
      while (tempStack.Count > 0)
      {
        _cargoStacks[move.To - 1].Push(tempStack.Pop());
      }
    }

    private void OldMoveExecution(MoveInstruction move)
    {
      for (int i = 0; i < move.Number; i++)
      {
        _cargoStacks[move.To - 1].Push(_cargoStacks[move.From - 1].Pop());
      }
    }

    public override string ToString()
    {
      if (_cargoStacks.Count == 0) return "[Empty Ship]";
      var stringBuilder = new StringBuilder();
      var highestStackHeight = _cargoStacks.MaxBy<Stack<char>, int>(s => s.Count).Count;

      for (int j = 0; j < highestStackHeight; j++)
      {
        for (int i = 0; i < _cargoStacks.Count; i++)
        {
          if (highestStackHeight - j > _cargoStacks[i].Count) stringBuilder.Append("   ");
          else stringBuilder.AppendFormat("[{0}]", _cargoStacks[i].ToArray()[j - (highestStackHeight - _cargoStacks[i].Count)]);
          if (i != _cargoStacks.Count - 1) stringBuilder.Append(" ");
        }
        stringBuilder.AppendLine();
      }

      for (int i = 0; i < _cargoStacks.Count; i++)
      {
        stringBuilder.AppendFormat(" {0} ", (i + 1));
        if (i != _cargoStacks.Count - 1) stringBuilder.Append(" ");
      }


      return stringBuilder.ToString();
    }

    public class MoveInstruction
    {
      public int From { get; }
      public int To { get; }
      public int Number { get; }

      public MoveInstruction(int number, int from, int to)
      {
        Number = number;
        To = to;
        From = from;
      }

      public MoveInstruction(string instruction)
      {
        Number = Int16.Parse(instruction.Split(' ')[1]);
        From = Int16.Parse(instruction.Split(' ')[3]);
        To = Int16.Parse(instruction.Split(' ')[5]);
      }
    }
  }

}