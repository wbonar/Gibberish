using Gibberish.Data;
using Gibberish.Services;

namespace Gibberish.Formatters;

public static class PunctuationShuffler
{
    private static readonly Shuffler Shuffler = new ();

    public static string ToRandomShufflePunctuation(this string value, ICharacterData characterData)
    {
        var punctuationArray = characterData.GetStandardPunctuation();
        return GibberishWorker.ReplaceCharacters(value, punctuationArray, Shuffler.CypherShuffle(punctuationArray));
    }
    public static string ToShufflePunctuation(this string value, ICharacterData characterData)
    {
        var usedPunctuation = characterData.GetStandardPunctuation().Intersect(value.ToCharArray()).Distinct().ToList();
        return GibberishWorker.ReplaceCharacters(value, usedPunctuation, Shuffler.CypherShuffle(usedPunctuation));
    }
}