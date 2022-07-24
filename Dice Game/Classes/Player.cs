using System.Text;

namespace Dice_Game.Classes;

public class Player
{
    // Player name
    public string Name { get; private set; }

    // Starting score for all players
    public int Score { get; private set; } = 0;

    // The temporary variable for holding score untill all re-roll attempts are completed
    private int CurrentRoll;

    // Re-Rolls this player has left
    private int ReRolls;

    // Number of dice this player has
    private readonly int NumDice;

    // Array of dice that the player will roll
    // Could just as easily be a list
    // Just doing this because arrays are still used a lot more
    private readonly Dice[] Dice;

    // When a player is created we expect a name to be given at creation
    // Alternatively this could be removed and a function for setting the name could be added
    public Player(string name, int numDice, int reRolls)
    {
        Name = name;
        NumDice = numDice;
        ReRolls = reRolls;

        // Initialise the dice array with NumDice
        Dice = new Dice[NumDice];
        for (int i = 0; i < NumDice; i++)
        {
            Dice[i] = new Dice();
        }
    }

    // Basically a singular method that kicks off all the little steps that need to happen during a players turn
    public void PlayTurn()
    {
        Console.WriteLine($"{Name} press any key to roll your dice.");
        _ = Console.ReadLine();
        RollDice();
        ReRoll();
        UpdateScore();
        ReportScore();
    }

    // This is how the player will roll all of their dice at once
    private void RollDice()
    {
        CurrentRoll = 0;

        // Roll every dice in our dice array
        // This will work just as well even if there were 10x the dice
        foreach (Dice? d in Dice)
        {
            d.Roll();
            CurrentRoll += d.Value;
        }

        ReportDice();
    }

    // Does the player want to re-roll or not
    private void ReRoll()
    {
        if (ReRolls > 0)
        {
            Console.WriteLine($"You have {ReRolls} re-roll(s) left");
            Console.WriteLine("Re-Roll the lowest dice? (Y = Yes : N = No)");
            string? response = Console.ReadLine().ToUpper();

            bool choiceMade = false;
            while (!choiceMade)
            {
                switch (response)
                {
                    case "Y":
                        ReRollLowestDice();
                        ReportDice();
                        ReRolls--;
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
    }

    // This will handle all re-rolls since our functionality is ever so slightly different
    private void ReRollLowestDice()
    {
        CurrentRoll = 0;
        // Instead of an if/else to see which is lowest, I'm just sorting the array
        // Again overly complex considering the use case
        // This could just as easilt be an if/else
        // The DiceComparison class show how to compare a Dice class by one of it's properties
        Array.Sort(Dice, new DiceComparison());

        // Since the array is sorted, the first value will always be the lowest so we simply re-roll that
        Dice[0].Roll();

        // Update our current roll to the re-roll values
        CurrentRoll = Dice[0].Value + Dice[1].Value;
    }

    // Whenever we are done with rolls and re-rolls we can use this to update the actual score with out final value from the current turn
    public void UpdateScore()
    {
        Score += CurrentRoll;
    }

    // Report all of the dice rolls from the most recent roll
    private void ReportDice()
    {
        // Since the number of dice is configurable, might as well make the dice reporting scalable
        // String builder allows you to do way more complicated stuff than this too
        // Alternatively you could also use string += as it functions basically the same way
        StringBuilder allDice = new StringBuilder();
        foreach (Dice d in Dice)
        {
            _ = allDice.Append($"[{d.Value}] ");
        }
        Console.WriteLine(allDice);
    }

    // Name and score for end of round/game reporting
    public void ReportScore()
    {
        Console.WriteLine($"{Name}: {Score}");
    }
}
