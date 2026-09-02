using System.Text;

namespace Gibberish.Services;

public static class GibberishWorker
{
    public static string ReplaceCharacters(string source, IEnumerable<char> keys, IEnumerable<char> values)
    {
        var cypher = BuildCypherDict(keys, values);
        return ReplaceCharacters(source, cypher);
    }
    
    public static string ReplaceCharacters(string source, Dictionary<char, char> cypher)
    {
        
        var sb = new StringBuilder();
        foreach (var character in source.ToCharArray())
        {
            sb.Append(cypher.GetValueOrDefault(character, character));
        }

        return sb.ToString();
    }

    public static Dictionary<char, char> BuildCypherDict(IEnumerable<char> keys, IEnumerable<char> values)
    {
        return  keys.Zip(values, (k, v) => new { k, v }).ToDictionary(x => x.k, x => x.v);
    }

    public static Dictionary<char, char> JoinDictionaries(params Dictionary<char, char>[] cyphers)
    {
        return cyphers.SelectMany(x => x).ToDictionary(x => x.Key, y => y.Value);
    }
}