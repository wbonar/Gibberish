namespace Gibberish.Services;

public class Shuffler
{
    private readonly Random _rng = new();
        
    public List<T> CypherShuffle<T>(List<T> collection)
    {
        ArgumentNullException.ThrowIfNull(collection);
        if (collection.Count < 2) return collection;


        var shuffledArray = new List<T>(collection);
        for (var enumerable = collection.Count - 1; enumerable >= 0; enumerable--)
        {
            var randomSelection = _rng.Next(enumerable); 
            (shuffledArray[enumerable], shuffledArray[randomSelection]) = (shuffledArray[randomSelection], shuffledArray[enumerable]);
        }

        return shuffledArray;

    }
}