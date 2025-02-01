namespace RefactoringActivity;

public class Location
{
    private string _name;
    private string _description;
    private Dictionary<string, string> _exits;
    private List<string> _items;
    private List<Puzzle> _puzzles;
    
    public Location(string name, string description)
    {
        _name = name;
        _description = description;
        _exits = new Dictionary<string, string>();
        _items = new List<string>();
        _puzzles = new List<Puzzle>();
    }
    
    public string GetDescription()
    {
        return _description;
    }

    public Dictionary<string, string> GetExits()
    {
        return _exits;
    }
    
    public List<string> GetItems()
    {
        return _items;
    }
    
    public List<Puzzle> GetPuzzles()
    {
        return _puzzles;
    }
}