namespace RefactoringActivity;

public class World
{
    private Dictionary<string, Location> _locations;

    public World()
    {
        _locations = new Dictionary<string, Location>();
        InitializeWorld();
    }

    private void InitializeWorld()
    {
        Location start = new("Start", "You are at the starting point of your adventure.");
        Location forest = new("Forest", "You are in a dense, dark forest.");
        Location cave = new("Cave", "You see a dark, ominous cave.");

        InitializeExits(start, forest, cave);

        InitializeItems(start, forest, cave);

        InitializePuzzles(start);

        InitializeLocations(start, forest, cave);
    }

    private static void InitializePuzzles(Location start)
    {
        start.GetPuzzles().Add(new Puzzle("riddle",
            "What's tall as a house, round as a cup, and all the king's horses can't draw it up?", "well"));
    }

    private static void InitializeItems(Location start, Location forest, Location cave)
    {
        start.GetItems().Add("map");
        forest.GetItems().Add("key");
        forest.GetItems().Add("potion");
        cave.GetItems().Add("sword");
    }

    private static void InitializeExits(Location start, Location forest, Location cave)
    {
        start.GetExits().Add("north", "Forest");
        forest.GetExits().Add("south", "Start");
        forest.GetExits().Add("east", "Cave");
        cave.GetExits().Add("west", "Forest");
    }

    private void InitializeLocations(Location start, Location forest, Location cave)
    {
        _locations.Add("Start", start);
        _locations.Add("Forest", forest);
        _locations.Add("Cave", cave);
    }
    

    public bool MovePlayer(Player player, string direction)
    {
        if (_locations[player.GetCurrentLocation()].GetExits().ContainsKey(direction))
        {
            player.SetCurrentLocation(_locations[player.GetCurrentLocation()].GetExits()[direction]);
            return true;
        }

        return false;
    }

    public string GetLocationDescription(string locationName)
    {
        if (_locations.ContainsKey(locationName)) 
            return _locations[locationName].GetDescription();
        return "Unknown location.";
    }

    public string GetLocationDetails(string locationName)
    {
        if (!_locations.ContainsKey(locationName)) 
            return "Unknown location.";

        Location location = _locations[locationName];
        string details = location.GetDescription();
        
        if (location.GetExits().Count > 0)
        {
            details += " Exits lead: ";
            foreach (string exit in location.GetExits().Keys)
                details += exit + ", ";
            details = details.Substring(0, details.Length - 2);
        }

        if (location.GetItems().Count > 0)
        {
            details += "\nYou see the following items:";
            foreach (string item in location.GetItems()) 
                details += $"\n- {item}";
        }

        if (location.GetPuzzles().Count > 0)
        {
            details += "\nYou see the following puzzles:";
            foreach (Puzzle puzzle in location.GetPuzzles()) 
                details += $"\n- {puzzle.GetName()}";
        }

        return details;
    }

    public bool TakeItem(Player player, string itemName)
    {
        Location location = _locations[player.GetCurrentLocation()];
        if (location.GetItems().Contains(itemName))
        {
            location.GetItems().Remove(itemName);
            player.GetInventory().Add(itemName);
            Console.WriteLine($"You take the {itemName}.");
            return true;
        }

        return false;
    }

    public bool UseItem(Player player, string itemName)
    {
        if (player.GetInventory().Contains(itemName))
        {
            if (itemName == "potion")
            {
                Console.WriteLine("Ouch! That tasted like poison!");
                player.SetHealth(player.GetHealth() - 10);
                Console.WriteLine($"Your health is now {player.GetHealth()}.");
            }
            else
            {
                Console.WriteLine($"The {itemName} disappears in a puff of smoke!");
            }
            player.GetInventory().Remove(itemName);
            return true;
        }

        return false;
    }

    public bool SolvePuzzle(Player player, string puzzleName)
    {
        Location location = _locations[player.GetCurrentLocation()];
        Puzzle? puzzle = location.GetPuzzles().Find(p => p.GetName() == puzzleName);

        if (puzzle != null && puzzle.Solve())
        {
            location.GetPuzzles().Remove(puzzle);
            return true;
        }

        return false;
    }
    
    
}