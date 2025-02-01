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
        if (_inventory.Count == 0)
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
}