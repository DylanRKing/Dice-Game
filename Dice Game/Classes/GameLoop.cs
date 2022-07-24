namespace Dice_Game.Classes;

public class GameLoop
{
    private readonly string[] DiceIntro =
    {
        "########### ########### ########### ########### ########### ###########",
        "#         # #      O  # #      O  # #  O   O  # #  O   O  # #  O   O  #",
        "#    O    # #         # #    O    # #         # #    O    # #  O   O  #",
        "#         # #  O      # #  O      # #  O   O  # #  O   O  # #  O   O  #",
        "########### ########### ########### ########### ########### ###########",
        "-----------------------------------------------------------------------",
        "------------------------------ DICE GAME ------------------------------",
        "\n"
    };

    // Set the default values here so it's easy to change them all
    private static int numPlayers = 2;
    private static int numRounds = 3;
    private static readonly int numDice = 2;
    private static int currentRound = 1;
    private static readonly int numReRolls = numRounds / 2;

    // Create a list of players since we don't know how many we'll have
    private List<Player>? _players;

    public GameLoop()
    {
        // Display the Intro
        ShowIntro();

        // Set up players and rounds
        ManagePlayers();
        ManageRounds();
    }

    public void Run()
    {
        Console.WriteLine("-----------------------------------------------------------------------");
        Console.WriteLine($"The player with the highest score after {numRounds} rounds will win");
        Console.WriteLine("-----------------------------------------------------------------------");

        // Set the game to keep running until the max number of turns has been reached
        while (currentRound <= numRounds)
        {
            PlayRound();
            RoundSummary();
            currentRound++;
        }
        GameSummary();
        Reset();
    }

    #region Pre-Game Setup

    private void ShowIntro()
    {
        // Display the Intro
        foreach (string? line in DiceIntro)
        {
            Console.WriteLine(line);
        }
    }

    private void ManagePlayers()
    {
        // Create an instance of the list
        _players = new List<Player>();

        // Use string interpolation to show a message about players including the default set above
        Console.WriteLine($"How many players would you like to have? (The default is {numPlayers})");
        string? response = Console.ReadLine();

        if (!string.IsNullOrEmpty(response))
        {
            numPlayers = Convert.ToInt32(response);
        }

        // Start out loop from 1 since player 0 seems stupid
        for (int i = 1; i <= numPlayers; i++)
        {
            AddPlayer(i);
        }
    }

    private void AddPlayer(int num)
    {
        Console.WriteLine($"Please enter a name for Player {num}");

        // Get the user input 
        string? name = Console.ReadLine();

        // Evaluate whether is was blank and give default name
        if (string.IsNullOrEmpty(name))
        {
            name = $"Player {num}";
        }

        // Add our new player to the list with name and re-rolls
        _players.Add(new Player(name, numDice, numReRolls));
    }

    private void ManageRounds()
    {
        numRounds = 2;
        currentRound = 1;

        Console.WriteLine($"How many rounds would you like to play? (The default is {numRounds})");

        string? rounds = Console.ReadLine();

        if (!string.IsNullOrEmpty(rounds))
        {
            numRounds = Convert.ToInt32(rounds);
        }
    }

    #endregion

    #region Mid-Game Actions

    private void PlayRound()
    {
        Console.WriteLine($"------ Round {currentRound} ------");

        foreach (Player p in _players)
        {
            p.PlayTurn();
        }
    }

    private void RoundSummary()
    {
        Console.WriteLine("------ Round Summary ------");
        foreach (Player p in _players)
        {
            p.ReportScore();
        }
    }

    #endregion

    #region Post-Game Clean-Up

    private Player WinningPlayer()
    {
        return _players.MaxBy(x => x.Score);
    }

    private void GameSummary()
    {
        Player? winner = WinningPlayer();
        Console.WriteLine("-----------------------------------------------------------------------");
        Console.WriteLine("------------------------------ Game Over ------------------------------");
        Console.WriteLine("-----------------------------------------------------------------------");
        Console.WriteLine($"{winner.Name} is the winner with {winner.Score} points");
        Console.WriteLine("-----------------------------------------------------------------------");
        foreach (Player p in _players)
        {
            p.ReportScore();
        }
    }

    private void Reset()
    {
        Console.WriteLine("Would you like to reset the game? (Y = Yes : N = No)");
        string? response = Console.ReadLine().ToUpper();

        bool choiceMade = false;
        while (!choiceMade)
        {
            switch (response)
            {
                case "Y":
                    // Reset the game state as if it was launched again
                    Console.Clear();
                    ShowIntro();
                    ManagePlayers();
                    ManageRounds();
                    Run();
                    choiceMade = true;
                    break;
                case "N":
                    // Do nothing
                    choiceMade = true;
                    break;
                default:
                    Console.WriteLine("Unexpected reponse.  (Y = Yes : N = No)");
                    response = Console.ReadLine().ToUpper();
                    break;
            }
        }

    }

    #endregion
}
