using Dice_Game.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Dice_Game.Classes;

public class Dice
{
    // This will reference the singleton instace of the roller class
    // It doesn't matter how many dice you create, they will all use the same roller class
    // This means more "expensive" instances like random will only be created once and will be used repeatedly
    private readonly IRoller _roller;

    // The value of this specific dice wherever it is used
    public int Value { get; set; }

    // Get our instance of the scoped class
    public Dice()
    {
        _roller = Program.SProvider.GetRequiredService<IRoller>();
    }

    public void Roll()
    {
        // Basically the only thing the dice needs to work
        Value = _roller.Roll();
    }
}
