using FluentAssertions;

namespace Objectbay.Vacation.WebApi.Tests;

public class AnagramTests 
{    
    [Theory]
    [InlineData("Elvis", "Lives")]
    [InlineData("Dormitory", "Dirty Room")]
    [InlineData("Vacation time", "I am not active")]
    public void Check_anagram(string word, string anagram)
    {
        var orderedWord = word.OrderBy(x => x).ToString();
        var orderedAnagram = anagram.OrderBy(x => x).ToString();

        orderedAnagram.Should().Be(orderedWord);
    }
}