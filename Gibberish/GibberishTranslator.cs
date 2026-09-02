using Gibberish.Data;
using Gibberish.Enums;
using Gibberish.Formatters;

namespace Gibberish;

public static class GibberishTranslator
{
    private static readonly GibberishFormatter DefaultFormatter = new();
    private static readonly ICharacterData DefaultCharacterData = new CharacterData();

    // Standard Use method
    // ReSharper disable once UnusedMember.Global
    public static string ToGibberish(this string value)
    {
        ArgumentNullException.ThrowIfNull(value);
        return value.ToGibberish(DefaultFormatter);
    }
    public static string ToGibberish(this string value, GibberishFormatter formatter, ICharacterData? characterData = null)
    {
        ArgumentNullException.ThrowIfNull(value);

        var gibberish = value;

        gibberish = formatter.CapitalizationBehavior switch
        {
            CapitalizationBehavior.Upper => gibberish.ToUpper(),
            CapitalizationBehavior.Lower => gibberish.ToLower(),
            _ => gibberish
        };

        gibberish = formatter.LetterOrderBehavior switch
        {
            LetterOrderBehavior.Random => gibberish.ToShuffleRandomLetters(characterData ?? DefaultCharacterData),
            LetterOrderBehavior.Shuffle => gibberish.ToShuffleLetters(characterData ?? DefaultCharacterData),
            _ => gibberish
        };

        gibberish = formatter.PunctuationOrderBehavior switch
        {
            PunctuationOrderBehavior.Random => gibberish.ToRandomShufflePunctuation(characterData ?? DefaultCharacterData),
            PunctuationOrderBehavior.Shuffle => gibberish.ToShufflePunctuation(characterData ?? DefaultCharacterData),
            _ => gibberish
        };

        gibberish = gibberish.ToShuffleCharacterSet(formatter.CharacterSetReplacementBehavior, characterData ?? DefaultCharacterData);

        return gibberish;
    }
}