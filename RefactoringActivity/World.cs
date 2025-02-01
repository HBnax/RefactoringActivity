namespace RefactoringActivity;

public class World
{
    public Dictionary<string, Location> Locations;
    
    private Location start;
    private Location forest;
    private Location cave;

    public World()
    {
        Locations = new Dictionary<string, Location>();
        InitializeWorld();
    }

    public Dictionary<string, Location> GetLocations()
    {
        return Locations;
    }

    private void InitializeWorld()
    {
        InitializeLocations();
        InitializeExits();
        InitializeItems();
        InitializePuzzles();
        PopulateDictionaryWithLocations();
    }

    private void InitializePuzzles()
    {
        start.AddPuzzle("riddle", "What's tall as a house, round as a cup, and all the king's horses can't draw it up?", "well");
    }
    
    private void InitializeLocations()
    {
        start = new Location("Start", "You are at the starting point of your adventure.");
        forest = new Location("Forest", "You are in a dense, dark forest.");
        cave = new Location("Cave", "You see a dark, ominous cave.");
    }

    private void PopulateDictionaryWithLocations()
    {
        Locations.Add("Start", start);
        Locations.Add("Forest", forest);
        Locations.Add("Cave", cave);
    }

    private void InitializeItems()
    {
        start.AddItem("map");
        forest.AddItem("key");
        forest.AddItem("potion");
        cave.AddItem("sword");
    }

    private void InitializeExits()
    {
        start.AddExit("north", "Forest");
        forest.AddExit("south", "Start");
        forest.AddExit("east", "Cave");
        cave.AddExit("west", "Forest");
    }

    public string GetLocationDescription(string locationName)
    {
        if (Locations.ContainsKey(locationName)) 
            return Locations[locationName].GetDescription();
        return "Unknown location.";
    }

    public string GetLocationDetails(string locationName)
    {
        if (!Locations.ContainsKey(locationName)) 
            return "Unknown location.";

        Location location = Locations[locationName];
        string details = location.GetDescription();
        
        details = CompileLocationDetails(location, details);

        return details;
    }

    private static string CompileLocationDetails(Location location, string details)
    {
        if (location.GetExits().Count > 0)
        {
            details = GetLocationExitDetails(details, location);
        }

        if (location.GetItems().Count > 0)
        {
            details = GetLocationItemDetails(details, location);
        }

        if (location.GetPuzzles().Count > 0)
        {
            details = GetLocationPuzzleDetails(details, location);
        }

        return details;
    }

    private static string GetLocationPuzzleDetails(string details, Location location)
    {
        details += "\nYou see the following puzzles:";
        foreach (Puzzle puzzle in location.GetPuzzles()) 
            details += $"\n- {puzzle.GetName()}";
        return details;
    }

    private static string GetLocationItemDetails(string details, Location location)
    {
        details += "\nYou see the following items:";
        foreach (string item in location.GetItems()) 
            details += $"\n- {item}";
        return details;
    }

    private static string GetLocationExitDetails(string details, Location location)
    {
        details += " Exits lead: ";
        foreach (string exit in location.GetExits().Keys)
            details += exit + ", ";
        details = details.Substring(0, details.Length - 2);
        return details;
    }
    
    public bool MovePlayer(Player player, string direction)
    {
        if (Locations[player.GetCurrentLocation()].GetExits().ContainsKey(direction))
        {
            player.SetCurrentLocation(Locations[player.GetCurrentLocation()].GetExits()[direction]);
            return true;
        }

        return false;
    }
    
    public bool TakeItem(Player player, string itemName)
    {
        Location location = Locations[player.GetCurrentLocation()];
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
        Location location = Locations[player.GetCurrentLocation()];
        Puzzle? puzzle = location.GetPuzzles().Find(p => p.GetName() == puzzleName);

        if (puzzle != null && puzzle.Solve())
        {
            location.GetPuzzles().Remove(puzzle);
            return true;
        }

        return false;
    }
}
