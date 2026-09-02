using Gibberish.Data;
using Gibberish.Enums;

namespace Gibberish.Tests.Mocks;

public class MockCharacterData : ICharacterData
{
    public List<char> GetLatinUpper() => ['A', 'B', 'C', 'D'];

    public List<char> GetLatinLower() => ['a', 'b', 'c', 'd'];

    public List<char> GetLatinSet() => ['A', 'B', 'C', 'D', 'a', 'b', 'c', 'd'];

    public List<char> GetStandardPunctuation() => ['.', ',', '!', '?'];

    public IEnumerable<char> GetFunkySet(CharacterSetReplacementBehavior setBehavior) =>
        setBehavior switch
        {
            CharacterSetReplacementBehavior.None => GetLatinSet(),
            CharacterSetReplacementBehavior.Thai => GetThaiSet(),
            CharacterSetReplacementBehavior.Georgian => GetGeorgianSet(),
            _ => throw new ArgumentOutOfRangeException(nameof(setBehavior))
        };

    private static List<char> GetThaiSet() => ['y', 'y', 'y', 'y', 'y', 'y', 'y', 'y'];

    private static List<char> GetGeorgianSet() => ['z', 'z', 'z', 'z', 'z', 'z', 'z', 'z'];
}