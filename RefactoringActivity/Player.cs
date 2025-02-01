namespace RefactoringActivity;

public class Player
{
    private int _health;
    private string _currentLocation;
    private List<string> _inventory;

    public Player(int health)
    {
        _health = health;
        _currentLocation = "Start";
        _inventory = new List<string>();
    }

    public void ShowInventory()
    {
        if (InventoryIsEmpty())
        {
            Console.WriteLine("Your inventory is empty.");
        }
        else
        {
            Console.WriteLine("You are carrying:");
            foreach (string item in _inventory)
            {
                Console.WriteLine($"- {item}");
            }
        }
    }
    
    public bool UseItem(string itemName)
    {
        if (_inventory.Contains(itemName))
        {
            if (itemName == "potion")
            {
                UsePotion();
            }
            else
            {
                Console.WriteLine($"The {itemName} disappears in a puff of smoke!");
            }
            _inventory.Remove(itemName);
            return true;
        }

        return false;
    }
    
    public bool TakeItem(string itemName, Location location)
    {
        if (location.GetItems().Contains(itemName))
        {
            location.GetItems().Remove(itemName);
            _inventory.Add(itemName);
            Console.WriteLine($"You take the {itemName}.");
            return true;
        }
        return false;
    }

    private void UsePotion()
    {
        Console.WriteLine("Ouch! That tasted like poison!");
        _health -= 10;
        Console.WriteLine($"Your health is now {_health}.");
    }
    
    public bool MovePlayer(Player player, string direction, World world)
    {
        if (world.Locations[GetCurrentLocation()].GetExits().ContainsKey(direction))
        {
            SetCurrentLocation(world.Locations[player.GetCurrentLocation()].GetExits()[direction]);
            return true;
        }

        return false;
    }
    
    public bool SolvePuzzle(string puzzleName, World world)
    {
        Location location = world.Locations[GetCurrentLocation()];
        Puzzle? puzzle = location.GetPuzzles().Find(p => p.GetName() == puzzleName);

        if (puzzle != null && puzzle.Solve())
        {
            location.GetPuzzles().Remove(puzzle);
            return true;
        }

        return false;
    }

    public int GetHealth()
    {
        return _health;
    }

    public void SetHealth(int health)
    {
        _health = health;
    }

    public string GetCurrentLocation()
    {
        return _currentLocation;
    }

    public void SetCurrentLocation(string location)
    {
        _currentLocation = location;
    }

    public List<string> GetInventory()
    {
        return _inventory;
    }

    public void AddInventoryItem(string item)
    {
        _inventory.Add(item);
    }

    public void RemoveInventoryItem(string item)
    {
        _inventory.Remove(item);
    }

    public bool HasItem(string item)
    {
        return _inventory.Contains(item);
    }

    private bool InventoryIsEmpty()
    {
        return _inventory.Count == 0;
    }
}
