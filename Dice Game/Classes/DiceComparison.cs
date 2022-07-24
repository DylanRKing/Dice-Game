using System.Collections;

namespace Dice_Game.Classes;
internal class DiceComparison : IComparer
{
    public int Compare(object? x, object? y)
    {
        return new CaseInsensitiveComparer().Compare(((Dice)x).Value, ((Dice)y).Value);
    }
}
