using Gibberish.Enums;

namespace Gibberish;

public class GibberishFormatter
{
    public LetterOrderBehavior LetterOrderBehavior { get; set; } = LetterOrderBehavior.Random;
    public PunctuationOrderBehavior PunctuationOrderBehavior { get; set; } = PunctuationOrderBehavior.Random;
    public CharacterSetReplacementBehavior CharacterSetReplacementBehavior { get; set; } = CharacterSetReplacementBehavior.Georgian;
    public CapitalizationBehavior CapitalizationBehavior { get; set; } = CapitalizationBehavior.None;
}