using System.Text;
using FluentAssertions;
using Net.Codecrete.QrCodeGenerator;

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

    [Fact]
    public void Generate_QR_Code() {
        var qr = QrCode.EncodeText("https://www.praxis-angelika-lang.at", QrCode.Ecc.Medium);
        string svg = qr.ToSvgString(4);
        File.WriteAllText("angelika-lang-qr.svg", svg, Encoding.UTF8);
    
    }
}

internal static class AnagramTrimmerExtension
{
    public static string ToCheckableAnagram(this string word) =>
        string.Concat(
            word.ToLower()
                .Where(c => !Char.IsWhiteSpace(c))
                .OrderByDescending(x => x)
        );
}