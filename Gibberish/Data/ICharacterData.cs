using Gibberish.Enums;

namespace Gibberish.Data;

public interface ICharacterData
{
    List<char> GetLatinUpper();
    List<char> GetLatinLower();
    List<char> GetLatinSet();
    List<char> GetStandardPunctuation();
    IEnumerable<char> GetFunkySet(CharacterSetReplacementBehavior setBehavior);
}