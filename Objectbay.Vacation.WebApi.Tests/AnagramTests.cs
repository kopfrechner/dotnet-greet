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
        var orderedWord = string.Concat(word.ToLower().Where(c => !Char.IsWhiteSpace(c)).OrderByDescending(x => x));
        var orderedAnagram = string.Concat(anagram.ToLower().Where(c => !Char.IsWhiteSpace(c)).OrderByDescending(x => x));

        orderedAnagram.Should().Be(orderedWord);
    }
}