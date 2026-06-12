using TicTacToe;

namespace TicTacToe.Tests;

public class GameTests
{
    [Fact]
    public void NewGame_HasNineAvailableMoves()
    {
        var game = new Game();

        Assert.Equal(9, game.GetAvailableMoves().Count);
        Assert.False(game.IsGameOver());
    }

    [Fact]
    public void HumanCanWinWithTopRow()
    {
        var game = new Game();

        Assert.True(game.MakeMove(0, Game.Human));
        Assert.True(game.MakeMove(3, Game.Computer));
        Assert.True(game.MakeMove(1, Game.Human));
        Assert.True(game.MakeMove(4, Game.Computer));
        Assert.True(game.MakeMove(2, Game.Human));

        Assert.True(game.IsWin(Game.Human));
        Assert.True(game.IsGameOver());
    }

    [Fact]
    public void DrawGame_IsDetected()
    {
        var game = new Game();

        Assert.True(game.MakeMove(0, Game.Human));
        Assert.True(game.MakeMove(1, Game.Computer));
        Assert.True(game.MakeMove(2, Game.Human));
        Assert.True(game.MakeMove(4, Game.Computer));
        Assert.True(game.MakeMove(6, Game.Computer));
        Assert.True(game.MakeMove(5, Game.Human));
        Assert.True(game.MakeMove(8, Game.Computer));
        Assert.True(game.MakeMove(7, Game.Human));
        Assert.True(game.MakeMove(3, Game.Computer));

        Assert.True(game.IsDraw());
        Assert.True(game.IsGameOver());
    }

    [Fact]
    public void InvalidMove_IsRejected()
    {
        var game = new Game();

        Assert.False(game.MakeMove(-1, Game.Human));
        Assert.False(game.MakeMove(0, 'Z'));
        Assert.True(game.MakeMove(0, Game.Human));
        Assert.False(game.MakeMove(0, Game.Computer));
    }

    [Fact]
    public void Computer_BlocksImmediateHumanWin()
    {
        var game = new Game();

        Assert.True(game.MakeMove(0, Game.Human));
        Assert.True(game.MakeMove(1, Game.Human));

        Assert.Equal(2, game.ChooseComputerMove());
    }

    [Fact]
    public void GetWinner_ReturnsHumanWhenPlayerWins()
    {
        var game = new Game();

        game.MakeMove(0, Game.Human);
        game.MakeMove(3, Game.Computer);
        game.MakeMove(1, Game.Human);
        game.MakeMove(4, Game.Computer);
        game.MakeMove(2, Game.Human);

        Assert.Equal(Game.Human, game.GetWinner());
    }

    [Fact]
    public void GetWinner_ReturnsComputerWhenComputerWins()
    {
        var game = new Game();

        game.MakeMove(3, Game.Human);
        game.MakeMove(0, Game.Computer);
        game.MakeMove(5, Game.Human);
        game.MakeMove(1, Game.Computer);
        game.MakeMove(8, Game.Human);
        game.MakeMove(2, Game.Computer);

        Assert.True(game.IsWin(Game.Computer));
        Assert.Equal(Game.Computer, game.GetWinner());
    }

    [Fact]
    public void GetWinner_ReturnsEmptyWhenGameIsDraw()
    {
        var game = new Game();

        game.MakeMove(0, Game.Human);
        game.MakeMove(1, Game.Computer);
        game.MakeMove(2, Game.Human);
        game.MakeMove(4, Game.Computer);
        game.MakeMove(6, Game.Computer);
        game.MakeMove(5, Game.Human);
        game.MakeMove(8, Game.Computer);
        game.MakeMove(7, Game.Human);
        game.MakeMove(3, Game.Computer);

        Assert.True(game.IsDraw());
        Assert.Equal(Game.Empty, game.GetWinner());
    }
}
