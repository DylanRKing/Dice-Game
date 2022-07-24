namespace Dice_Game.Interfaces;

// Interface for handling rolling
// This lets you define a structure or ruleset for any class that would use the interface
// That way whenever you use an instance of the class (i.e. IRoller) you already know what it will do
// It's a bit overly coimplex for this but it means you can write a bunch of different way to do something
// But they'll all still look the same to other classes
public interface IRoller
{
    public int Roll();
}
