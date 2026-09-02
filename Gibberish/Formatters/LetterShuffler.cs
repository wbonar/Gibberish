using Gibberish.Data;
using Gibberish.Services;

namespace Gibberish.Formatters;

public static class LetterShuffler
{
    private static readonly Shuffler Shuffler = new();

    public static string ToShuffleRandomLetters(this string value, ICharacterData characterData)
    {
        var lowerArray = characterData.GetLatinLower();
        var lowerCypherDict = GibberishWorker.BuildCypherDict(lowerArray, Shuffler.CypherShuffle(lowerArray));
        
        var upperArray = characterData.GetLatinUpper();
        var upperCypherDict = GibberishWorker.BuildCypherDict(upperArray, Shuffler.CypherShuffle(upperArray));
        
        return GibberishWorker.ReplaceCharacters(value, GibberishWorker.JoinDictionaries(lowerCypherDict, upperCypherDict));
    }
    
    public static string ToShuffleLetters(this string value, ICharacterData characterData)
    {
        var usedLetters = characterData.GetLatinSet().Intersect(value.ToCharArray()).Distinct().ToList();
        return GibberishWorker.ReplaceCharacters(value, usedLetters, Shuffler.CypherShuffle(usedLetters));
    }
}