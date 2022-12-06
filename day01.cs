namespace adventOfCode2022
{
  public class Calories
  {
    string[] lines;
    Dictionary<Int32, List<Int32>> inventories;
    public Calories()
    {
      lines = System.IO.File.ReadAllLines(@"./input_day01.txt");
      inventories = new Dictionary<Int32, List<Int32>>();
      Int32 inventoryCounter = 0;
      var currentInventory = new List<Int32>();

      for (Int32 i = 0; i < lines.Count(); i++)
      {
        if (lines[i] == "")
        {
          inventories.Add(inventoryCounter, currentInventory);
          inventoryCounter++;
          currentInventory = new List<Int32>();
        }
        else
        {
          currentInventory.Add(Int32.Parse(lines[i]));
        }
      }
    }

    public int GetLargestInventoryTotal()
    {
      var inventoryTotals = new List<int>();
      foreach (var inventory in inventories)
      {
        inventoryTotals.Add(inventory.Value.Sum());
      }
      return Enumerable.Max(inventoryTotals);
    }

    public int GetTopThreeInventoriesTotal()
    {
      var inventoryTotals = new List<int>();
      foreach (var inventory in inventories)
      {
        inventoryTotals.Add(inventory.Value.Sum());
      }
      inventoryTotals.Sort();
      inventoryTotals.Reverse();
      return inventoryTotals.ElementAt(0) + inventoryTotals.ElementAt(1) + inventoryTotals.ElementAt(2);
    }
  }
}

