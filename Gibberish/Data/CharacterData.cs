using Gibberish.Enums;

namespace Gibberish.Data;

public class CharacterData : ICharacterData
{
    public List<char> GetLatinUpper() => Enumerable.Range('A', 'Z' - 'A' + 1).Select(c => (char)c).ToList();

    public List<char> GetLatinLower() => Enumerable.Range('a', 'z' - 'a' + 1).Select(c => (char)c).ToList();

    public List<char> GetLatinSet()
    {
        var fullSet = new List<char>();
        fullSet.AddRange(GetLatinUpper());
        fullSet.AddRange(GetLatinLower());
        return fullSet;
    }

    public List<char> GetStandardPunctuation() =>
    [
        ';',
        ':',
        ',',
        '\"',
        '.',
        '?',
        '\'',
        '%',
        '$',
        '\\',
        '/',
        '<',
        '>',
        '*',
        '&',
        '^',
        '#',
        '@',
        '!',
        '=',
        '-',
        '_',
        '+'
    ];

    public IEnumerable<char> GetFunkySet(CharacterSetReplacementBehavior setBehavior) =>
        setBehavior switch
        {
            CharacterSetReplacementBehavior.Thai => GetThaiSet(),
            CharacterSetReplacementBehavior.Georgian => GetGeorgianSet(),
            _ => GetLatinSet()
        };

    private static List<char> GetThaiSet() =>
    [
        '\u0E01',
        '\u0E02',
        '\u0E03',
        '\u0E04',
        '\u0E05',
        '\u0E06',
        '\u0E07',
        '\u0E08',
        '\u0E09',
        '\u0E0A',
        '\u0E0B',
        '\u0E0C',
        '\u0E0D',
        '\u0E0E',
        '\u0E0F',

        '\u0E10',
        '\u0E11',
        '\u0E12',
        '\u0E13',
        '\u0E14',
        '\u0E15',
        '\u0E16',
        '\u0E17',
        '\u0E18',
        '\u0E19',
        '\u0E1A',
        '\u0E1B',
        '\u0E1C',
        '\u0E1D',
        '\u0E1E',
        '\u0E1F',

        '\u0E20',
        '\u0E21',
        '\u0E22',
        '\u0E23',
        '\u0E24',
        '\u0E25',
        '\u0E26',
        '\u0E27',
        '\u0E28',
        '\u0E29',
        '\u0E2A',
        '\u0E2B',
        '\u0E2C',
        '\u0E2D',
        '\u0E2E',
        '\u0E2F',

        '\u0E30',
        '\u0E31',
        '\u0E32',
        '\u0E33',
        '\u0E34'
    ];

    private static List<char> GetGeorgianSet() =>
    [
        '\u10A0',
        '\u10A1',
        '\u10A2',
        '\u10A3',
        '\u10A4',
        '\u10A5',
        '\u10A6',
        '\u10A7',
        '\u10A8',
        '\u10A9',
        '\u10AA',
        '\u10AB',
        '\u10AC',
        '\u10AD',
        '\u10AE',
        '\u10AF',

        '\u10B0',
        '\u10B1',
        '\u10B2',
        '\u10B3',
        '\u10B4',
        '\u10B5',
        '\u10B6',
        '\u10B7',
        '\u10B8',
        '\u10B9',
        '\u10BA',
        '\u10BB',
        '\u10BC',
        '\u10BD',
        '\u10BE',
        '\u10BF',

        '\u10D0',
        '\u10D1',
        '\u10D2',
        '\u10D3',
        '\u10D4',
        '\u10D5',
        '\u10D6',
        '\u10D7',
        '\u10D8',
        '\u10D9',
        '\u10DA',
        '\u10DB',
        '\u10DC',
        '\u10DD',
        '\u10DE',
        '\u10DF',

        '\u10C0',
        '\u10C1',
        '\u10C2',
        '\u10C3',
        '\u10C4'
    ];
}