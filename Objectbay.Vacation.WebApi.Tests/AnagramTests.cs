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
        var checkableWord = word.ToCheckableAnagram();
        var checkableAnagram = anagram.ToCheckableAnagram();

        checkableAnagram.Should().Be(checkableWord);
    }
}

internal static class AnagramTrimmerExtension {
    public static string ToCheckableAnagram(this string word) =>
        string.Concat(
            word.ToLower()
                .Where(c => !Char.IsWhiteSpace(c))
                .OrderByDescending(x => x)
        );
}