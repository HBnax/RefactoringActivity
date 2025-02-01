namespace RefactoringActivity.Tests;

using System;
using Xunit;

public class TextAdventureTests
{
    [Fact]
    public void Player_InitialValuesAreSetCorrectly()
    {
        // Arrange
        var player = new Player(100);

        // Act & Assert
        Assert.Equal(100, player.GetHealth());
        Assert.Equal("Start", player.GetCurrentLocation());
        Assert.Empty(player.GetInventory());
    }

    [Fact]
    public void Player_CanAddAndCheckItems()
    {
        // Arrange
        var player = new Player(100);

        // Act
        player.GetInventory().Add("key");

        // Assert
        Assert.Contains("key", player.GetInventory());
        Assert.True(player.GetInventory().Contains("key"));
    }

    [Fact]
    public void World_InitializesLocationsCorrectly()
    {
        // Arrange
        var world = new World();

        // Act
        var startDescription = world.GetLocationDescription("Start");
        var forestDescription = world.GetLocationDescription("Forest");

        // Assert
        Assert.Contains("starting point", startDescription);
        Assert.Contains("dark forest", forestDescription);
    }

    [Fact]
    public void World_CanMovePlayerBetweenLocations()
    {
        // Arrange
        var world = new World();
        var player = new Player(100);

        // Act
        bool moved = world.MovePlayer(player, "north");

        // Assert
        Assert.True(moved);
        Assert.Equal("Forest", player.GetCurrentLocation());
    }

    [Fact]
    public void World_PreventsInvalidMovement()
    {
        // Arrange
        var world = new World();
        var player = new Player(100);

        // Act
        bool moved = world.MovePlayer(player, "west");

        // Assert
        Assert.False(moved);
        Assert.Equal("Start", player.GetCurrentLocation());
    }

    [Fact]
    public void World_CanTakeItemFromLocation()
    {
        // Arrange
        var world = new World();
        var player = new Player(100);

        // Act
        bool taken = world.TakeItem(player, "map");

        // Assert
        Assert.True(taken);
        Assert.Contains("map", player.GetInventory());
    }

    [Fact]
    public void World_PreventsTakingNonexistentItem()
    {
        // Arrange
        var world = new World();
        var player = new Player(100);

        // Act
        bool taken = world.TakeItem(player, "nonexistent");

        // Assert
        Assert.False(taken);
    }

    [Fact]
    public void Puzzle_CanBeSolvedWithCorrectAnswer()
    {
        // Arrange
        var puzzle = new Puzzle("riddle", "What has to be broken before you can use it?", "egg");

        // Act
        Console.SetIn(new System.IO.StringReader("egg\n"));
        bool solved = puzzle.Solve();

        // Assert
        Assert.True(solved);
    }

    [Fact]
    public void Puzzle_CannotBeSolvedWithIncorrectAnswer()
    {
        // Arrange
        var puzzle = new Puzzle("riddle", "What has to be broken before you can use it?", "egg");

        // Act
        Console.SetIn(new System.IO.StringReader("stone\n"));
        bool solved = puzzle.Solve();

        // Assert
        Assert.False(solved);
    }
    
}