using Dice_Game.Interfaces;

namespace Dice_Game.Classes;

// This class implements the I roller interface
// Because this is true, it must have a roll method that returns the expected type
// We will also be instantiating this class just once and re-using it whenever any dice needs rolling
// Unecessary to save resources with such a basic app but it's more efficient
// This also makes maintenance easier because you can just fix it once and have it resolved everywhere it's used
public class Roller : IRoller
{
    // Declare here random as it will oply ever be instatiated once thanks to dependancy injection
    private readonly Random random;

    // Declare the min/max values for our rnadom here in case they ever need to be changed for smaller/bigger dice
    // Max obviously looks wrong because of the way that rnaomd works but...eh
    private const int minRoll = 1;
    private const int maxRoll = 7;

    public Roller()
    {
        // Assign random once when the class is instantiated
        random = new Random();
    }

    // probably the only method that's actually needed since the dice will just be assigned values repeatedly
    public int Roll()
    {
        // Gives us back a dice value between min/max regarless of how many sides we give the dice
        return random.Next(minRoll, maxRoll);
    }
}
