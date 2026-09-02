using Gibberish.Services;

namespace Gibberish.Tests;

public class CyberShufflerTests
{
    private readonly Shuffler _shuffler = new();

    [TestCase(3)]
    [TestCase(10)]
    [TestCase(100)]
    [TestCase(1000)]
    public void Validate_replacement_and_none_non_collision(int testSize)
    {
        var basicList = Enumerable.Range(0, testSize).ToList();
        var shuffledList = _shuffler.CypherShuffle(basicList);
        
        for (var index = 0; index < testSize; index++)
        {
            Assert.Multiple(() =>
            {
                Assert.That(shuffledList[index], Is.Not.EqualTo(index));
                Assert.That(shuffledList.Count(m => m == shuffledList[index]), Is.EqualTo(1));
            });
        }
    }
}