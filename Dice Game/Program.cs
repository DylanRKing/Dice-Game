// Dice Game
// ----------
// Features:
// - Multiple players
// - Configurable number of turns
// - Re-Roll lowest dice option (re-rolls cap at the half of the total turns)
// - Shows score after every roll
// ----------
// Extras:
// - Dice Images
// - Intro Text

using Dice_Game.Classes;
using Dice_Game.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Dice_Game;

internal class Program
{
    public static ServiceProvider SProvider { get; private set; }

    private static void Main()
    {
        Config();
        GameLoop? game = SProvider.GetRequiredService<GameLoop>();
        game.Run();
    }

    private static void Config()
    {
        SProvider = new ServiceCollection()
            .AddScoped<IRoller, Roller>()
            .AddTransient<GameLoop>()
            .BuildServiceProvider();
    }
}