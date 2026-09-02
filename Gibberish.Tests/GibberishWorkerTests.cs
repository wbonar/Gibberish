using Gibberish.Services;

namespace Gibberish.Tests;

public class GibberishWorkerTests
{
    private const string Abcd = "ABCD";
    private const string Wxyz = "WXYZ";


    private readonly List<char> _abcdList = ['A', 'B', 'C', 'D'];
    private readonly List<char> _wxyzList = ['W', 'X', 'Y', 'Z'];
    private readonly Dictionary<char, char> _simpleCypher = new() { { 'A', 'W' }, { 'B', 'X' }, { 'C', 'Y' }, { 'D', 'Z' } };

    [Test]
    public void Should_replace_characters_from_enumerable()
    {
        Assert.That(GibberishWorker.ReplaceCharacters(Abcd, _abcdList, _wxyzList), Is.EqualTo(Wxyz));
    }
    
    [Test]
    public void Should_replace_characters_from_dictionary()
    {
        Assert.That(GibberishWorker.ReplaceCharacters(Abcd, _simpleCypher), Is.EqualTo(Wxyz));
    }

    [Test]
    public void Should_build_dictionary()
    {
        Assert.That(GibberishWorker.BuildCypherDict(_abcdList, _wxyzList), Is.EqualTo(_simpleCypher));
    }

    [Test]
    public void Should_join_dictionaries()
    {
        var secondCypher = new Dictionary<char, char> { { 'a', 'w' }, { 'b', 'x' }, { 'c', 'y' }, { 'd', 'z' } };
        var joinedCypher = GibberishWorker.JoinDictionaries(_simpleCypher, secondCypher);
        
        Assert.Multiple(() =>
        {
            Assert.That(joinedCypher.Keys, Has.Count.EqualTo(8));
            Assert.That(joinedCypher.Keys.Intersect(_simpleCypher.Keys).Count(), Is.EqualTo(4));
            Assert.That(joinedCypher.Keys.Intersect(secondCypher.Keys).Count(), Is.EqualTo(4));
            Assert.That(joinedCypher['A'], Is.EqualTo(_simpleCypher['A']));
            Assert.That(joinedCypher['a'], Is.EqualTo(secondCypher['a']));
        });
    }
}