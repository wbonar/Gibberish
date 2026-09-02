using Gibberish.Data;
using Gibberish.Enums;
using Gibberish.Services;

namespace Gibberish.Formatters;

public static class CharacterSetReplacer
{
    public static string ToShuffleCharacterSet(this string value, CharacterSetReplacementBehavior characterSetReplacement, ICharacterData characterData)
    {
        if (characterSetReplacement == CharacterSetReplacementBehavior.None) return value;

        var latinSet = characterData.GetLatinSet();
        var funkySet = characterData.GetFunkySet(characterSetReplacement);
        
        return GibberishWorker.ReplaceCharacters(value, latinSet, funkySet);
    }
    
}