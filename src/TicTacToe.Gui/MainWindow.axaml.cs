using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;
using Avalonia.Threading;

namespace TicTacToe.Gui;

public partial class MainWindow : Window
{
    private readonly Game _game = new();
    private bool _isPlayerTurn = true;

    public MainWindow()
    {
        InitializeComponent();
        UpdateStatus();
    }

    private void OnCellClick(object? sender, RoutedEventArgs e)
    {
        if (sender is not Button button || !_isPlayerTurn || _game.IsGameOver())
            return;

        if (!int.TryParse(button.Tag?.ToString(), out var position))
            return;

        if (!_game.MakeMove(position, Game.Human))
            return;

        button.Content = Game.Human.ToString();
        button.IsEnabled = false;

        if (_game.IsGameOver())
        {
            FinishGame();
            return;
        }

        _isPlayerTurn = false;
        UpdateStatus("Computer is thinking...");

        Dispatcher.UIThread.Post(() =>
        {
            var computerMove = _game.ChooseComputerMove();
            if (computerMove >= 0 && _game.MakeMove(computerMove, Game.Computer))
            {
                var targetButton = this.FindControl<Button>($"Cell{computerMove}");
                if (targetButton is not null)
                {
                    targetButton.Content = Game.Computer.ToString();
                    targetButton.IsEnabled = false;
                }
            }

            _isPlayerTurn = true;
            if (_game.IsGameOver())
            {
                FinishGame();
            }
            else
            {
                UpdateStatus();
            }
        }, DispatcherPriority.Background);
    }

    private void OnNewGameClick(object? sender, RoutedEventArgs e)
    {
        _game.Reset();
        _isPlayerTurn = true;

        for (var i = 0; i < 9; i++)
        {
            var button = this.FindControl<Button>($"Cell{i}");
            if (button is not null)
            {
                button.Content = string.Empty;
                button.IsEnabled = true;
            }
        }

        UpdateStatus();
    }

    private void FinishGame()
    {
        var winner = _game.GetWinner();

        if (winner == Game.Human)
        {
            UpdateStatus("You win! Nice play.");
            ApplyBoardColor(Colors.LightGreen, Colors.Black);
        }
        else if (winner == Game.Computer)
        {
            UpdateStatus("Computer wins. Better luck next time.");
            ApplyBoardColor(Colors.LightCoral, Colors.Black);
        }
        else
        {
            UpdateStatus("It’s a draw. Solid match.");
            ApplyBoardColor(Colors.LightGoldenrodYellow, Colors.Black);
        }

        DisableBoard();
    }

    private void ApplyBoardColor(Color background, Color foreground)
    {
        var brush = new SolidColorBrush(background);
        var textBrush = new SolidColorBrush(foreground);

        for (var i = 0; i < 9; i++)
        {
            var button = this.FindControl<Button>($"Cell{i}");
            if (button is not null)
            {
                button.Background = brush;
                button.Foreground = textBrush;
            }
        }
    }

    private void DisableBoard()
    {
        for (var i = 0; i < 9; i++)
        {
            var button = this.FindControl<Button>($"Cell{i}");
            if (button is not null)
                button.IsEnabled = false;
        }
    }

    private void UpdateStatus(string? message = null)
    {
        if (StatusText is null)
            return;

        StatusText.Text = message ?? (_game.IsGameOver()
            ? "Game over."
            : _isPlayerTurn
                ? "Your turn."
                : "Computer is thinking...");
    }
}