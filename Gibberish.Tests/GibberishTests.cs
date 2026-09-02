using Gibberish.Enums;
using Gibberish.Tests.Mocks;

namespace Gibberish.Tests;

public class GibberishTests
{
    
    private const string TestString = "Hello World! This is a good test of the formatter.";
    private GibberishFormatter _formatter = new();
    private readonly MockCharacterData _fakeData = new();


    [SetUp]
    public void Setup()
    {
        _formatter = new GibberishFormatter
        {
            CapitalizationBehavior = CapitalizationBehavior.None,
            PunctuationOrderBehavior = PunctuationOrderBehavior.None,
            LetterOrderBehavior = LetterOrderBehavior.None,
            CharacterSetReplacementBehavior = CharacterSetReplacementBehavior.None
        };
    }

    #region Capitalization Tests

    [TestCase(CapitalizationBehavior.Lower, "Hello World!", "hello world!")]
    [TestCase(CapitalizationBehavior.Upper, "Hello World!", "HELLO WORLD!")]
    [TestCase(CapitalizationBehavior.None, "Hello World!", "Hello World!")]
    public void Should_alter_capitalization(CapitalizationBehavior behavior, string input, string expected)
    {
        _formatter.CapitalizationBehavior = CapitalizationBehavior.Lower;
        Assert.That(TestString.ToGibberish(_formatter), Is.EqualTo(TestString.ToLower()));
    }
    
    #endregion
    
    #region Letter Order Tests
    
    [Test]
    public void Letters_should_skip_when_set_to_none_text()
    {
        _formatter.LetterOrderBehavior = LetterOrderBehavior.None;
        Assert.That(TestString.ToGibberish(_formatter), Is.EqualTo(TestString));
    }

    [Test]
    public void Letters_should_be_same_size_but_different()
    {
        _formatter.LetterOrderBehavior = LetterOrderBehavior.Shuffle;
        var gibberish = TestString.ToGibberish(_formatter);

        Assert.That(gibberish, Is.Not.EqualTo(TestString));
        Assert.That(gibberish, Has.Length.EqualTo(TestString.Length));
    }

    [Test]
    public void Letters_should_be_shuffled()
    {
        _formatter.LetterOrderBehavior = LetterOrderBehavior.Shuffle;
        var gibberish = TestString.ToGibberish(_formatter);

        Assert.That(SortDistinctCharacters(gibberish), Is.EqualTo(SortDistinctCharacters(TestString)));
    }
    
    [Test]
    public void Letters_should_be_replaced_for_random()
    {
        _formatter.LetterOrderBehavior = LetterOrderBehavior.Random;
        const string testData = "ABCD";
        var gibberish = testData.ToGibberish(_formatter, _fakeData);

        Assert.Multiple(() =>
        {
            Assert.That(gibberish, Is.Not.EqualTo(testData));
            Assert.That(gibberish, Does.Contain('A'));
            Assert.That(gibberish, Does.Contain('B'));
            Assert.That(gibberish, Does.Contain('C'));
            Assert.That(gibberish, Does.Contain('D'));
        });
    }
    
    [Test]
    public void Letters_should_be_random()
    {
        _formatter.LetterOrderBehavior = LetterOrderBehavior.Random;
        const string testData = "AAAA";
        var possibleResults = new List<string> { "BBBB", "CCCC", "DDDD" };
        var gibberish = testData.ToGibberish(_formatter, _fakeData);
        
        Assert.That(possibleResults, Does.Contain(gibberish));
    }

    
    #endregion

    #region Punctiation Order Tests

    
    [Test]
    public void Punctuation_should_skip_when_set_to_none_text()
    {
        _formatter.PunctuationOrderBehavior = PunctuationOrderBehavior.None;
        Assert.That(TestString.ToGibberish(_formatter), Is.EqualTo(TestString));
    }

    [Test]
    public void Punctuation_should_be_same_size_but_different()
    {
        _formatter.PunctuationOrderBehavior = PunctuationOrderBehavior.Shuffle;
        var gibberish = TestString.ToGibberish(_formatter);

        Assert.That(gibberish, Is.Not.EqualTo(TestString));
        Assert.That(gibberish, Has.Length.EqualTo(TestString.Length));
    }

    [Test]
    public void Punctuation_should_be_shuffled()
    {
        _formatter.PunctuationOrderBehavior = PunctuationOrderBehavior.Shuffle;
        var gibberish = TestString.ToGibberish(_formatter);

        Assert.That(SortDistinctCharacters(gibberish), Is.EqualTo(SortDistinctCharacters(TestString)));
    }
    
    [Test]
    public void Punctuation_should_be_replaced_for_random()
    {
        _formatter.PunctuationOrderBehavior = PunctuationOrderBehavior.Random;
        const string testData = ",.?!";
        var gibberish = testData.ToGibberish(_formatter, _fakeData);

        Assert.That(gibberish, Is.Not.EqualTo(testData));
        Assert.That(gibberish, Does.Contain(','));
        Assert.That(gibberish, Does.Contain('.'));
        Assert.That(gibberish, Does.Contain('?'));
        Assert.That(gibberish, Does.Contain('!'));
    }
    
    [Test]
    public void Punctuation_should_be_random()
    {
        _formatter.PunctuationOrderBehavior = PunctuationOrderBehavior.Random;
        const string testData = "!!!!";
        var possibleResults = new List<string> { ",,,,", "....", "????" };
        var gibberish = testData.ToGibberish(_formatter, _fakeData);
        
        Assert.That(possibleResults, Does.Contain(gibberish));
    }

    #endregion
    
    #region Character Set Shuffler Tests

    [Test]
    public void Character_shuffle_should_skip_when_set_to_none_text()
    {
        _formatter.CharacterSetReplacementBehavior = CharacterSetReplacementBehavior.None;
        Assert.That(TestString.ToGibberish(_formatter), Is.EqualTo(TestString));
    }
    
    [Test]
    public void Character_shuffle_georgian_should_replace_all_letters()
    {
        _formatter.CharacterSetReplacementBehavior = CharacterSetReplacementBehavior.Georgian;
        const string replace = "ABCD!?";
        var gibberish = replace.ToGibberish(_formatter, _fakeData);
        
        Assert.That(gibberish, Is.EqualTo("zzzz!?"));
    }
    
    [Test]
    public void Character_shuffle_thai_should_replace_all_letters()
    {
        _formatter.CharacterSetReplacementBehavior = CharacterSetReplacementBehavior.Thai;
        const string replace = "ABCD!?";
        var gibberish = replace.ToGibberish(_formatter, _fakeData);
        
        Assert.That(gibberish, Is.EqualTo("yyyy!?"));
    }

    #endregion
    
    
    private static string SortDistinctCharacters(string input)
    {
        var characters = input.ToArray().Distinct().ToArray();
        Array.Sort(characters);
        return new string(characters);
    }
    
}