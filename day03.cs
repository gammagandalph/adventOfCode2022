namespace adventOfCode2022
{
  public class Backpack
  {
    private string _inventory;
    public string Inventory { get { return _inventory; } set { _inventory = value; } }
    public string FirstCompartment { get { return _inventory.Substring(0, _inventory.Length / 2); } }
    public string SecondCompartment { get { return _inventory.Substring((_inventory.Length / 2)); } }

    public Backpack(string inventory)
    {
      _inventory = inventory;
    }

    public char findDuplicate()
    {
      var duplicates = FirstCompartment.Where(i => SecondCompartment.Contains(i));
      if (duplicates.Count() > 0) return duplicates.ElementAt(0);
      throw new ArgumentOutOfRangeException();
    }

    /// <summary>
    /// <c>GetPriority</c> returns the numerical priority for an item. Throws ArgumentOutOfRangeException if item is not in [azAZ].
    /// </summary>
    public static int GetPriority(char item)
    {
      if (Char.IsLower(item)) return (int)item - 96;
      else if (Char.IsUpper(item)) return (int)item - 38;
      else throw new ArgumentOutOfRangeException();
    }
  }

  public class PackingChecker
  {
    private List<Backpack> _backpacks;
    public List<Backpack> Backpacks { get { return _backpacks; } }
    public PackingChecker(List<Backpack> backpacks)
    {
      _backpacks = backpacks;
    }
    public PackingChecker(string[] inventories)
    {
      _backpacks = new List<Backpack>();
      foreach (var inventory in inventories)
      {
        _backpacks.Add(new Backpack(inventory));
      }
    }
    public int GetBadgePriorities()
    {
      int score = 0;
      for (int i = 0; i < _backpacks.Count(); i += 3)
      {
        var group = new List<Backpack>() { _backpacks.ElementAt(i), _backpacks.ElementAt(i + 1), _backpacks.ElementAt(i + 2) };

        foreach (var item in group[0].Inventory)
        {
          if (group[1].Inventory.Contains(item) && group[2].Inventory.Contains(item))
          {
            score += Backpack.GetPriority(item);
            break;
          }
        }
      }
      return score;
    }
    public int GetPrioritiesScore()
    {
      int score = 0;
      foreach (var backpack in _backpacks)
      {
        score += Backpack.GetPriority(backpack.findDuplicate());
      }
      return score;
    }
  }
}