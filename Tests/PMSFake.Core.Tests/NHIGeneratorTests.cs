using System.Diagnostics.Metrics;
using Moq;

namespace PMSeeder.Core.Tests;

public class NHIGeneratorTests
{
    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void Generate_NHI_ReturnStringOf7Characters(bool supportNew)
    {
        // Arrange
        var config = new Mock<IGeneratorConfiguration>();
        config.Setup(c => c.IsNewFormat).Returns(supportNew);
        var generator = new NHIGenerator(config.Object);

        // Act
        var actual = generator.Generate().Length;

        // Assert
        Assert.Equal(7, actual);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void Generate_NHI_ReturnStringStartWith3UppercaseLetters(bool supportNew)
    {
        // Arrange
        var config = new Mock<IGeneratorConfiguration>();
        config.Setup(c => c.IsNewFormat).Returns(supportNew);
        var generator = new NHIGenerator(config.Object);

        // Act
        var actual = generator.Generate().Substring(0, 3).AsEnumerable();

        // Assert
        Assert.Contains(actual, c => char.IsUpper(c));
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void Generate_NHI_ReturnStringNotContainsOAndI(bool supportNew)
    {
        // Arrange
        var config = new Mock<IGeneratorConfiguration>();
        config.Setup(c => c.IsNewFormat).Returns(supportNew);
        var generator = new NHIGenerator(config.Object);

        // Act
        var actual = generator.Generate().AsEnumerable();

        // Assert
        Assert.DoesNotContain(actual, c => c == 'O' || c == 'I');
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void Generate_NHI_ReturnStringWith4thAnd5thCharactersBeingDigits(bool supportNew)
    {
        // Arrange
        var config = new Mock<IGeneratorConfiguration>();
        config.Setup(c => c.IsNewFormat).Returns(supportNew);
        var generator = new NHIGenerator(config.Object);

        // Act
        var actual = generator.Generate().Substring(3, 2).AsEnumerable();

        // Assert
        Assert.Contains(actual, c => char.IsNumber(c));
    }

    [Fact]
    public void Generate_OldNHI_ReturnStringWith6th7thCharactersBeingDigits()
    {
        // Arrange
        var config = new Mock<IGeneratorConfiguration>();
        config.Setup(c => c.IsNewFormat).Returns(false);
        var generator = new NHIGenerator(config.Object);

        // Act
        var actual = generator.Generate().Substring(5, 2).AsEnumerable();

        // Assert
        Assert.Contains(actual, c => char.IsNumber(c));
    }

    [Theory]
    [InlineData(false, 11)]
    [InlineData(true, 23)]
    public void Generate_OldNHI_ReturnStringWith7thCharacterBeingCheckSum(bool isNewFormat, int divider)
    {
        // Arrange
        var config = new Mock<IGeneratorConfiguration>();
        config.Setup(c => c.IsNewFormat).Returns(isNewFormat);
        var generator = new NHIGenerator(config.Object);
        var nhi = generator.Generate();
        const string validNhiChars = "ABCDEFGHJKLMNPQRSTUVWXYZ";
        var expected = nhi[6].ToString();

        // Act

        int result = 0;
        int counter = 7;
        foreach (var character in nhi[..6])
        {
            if (char.IsUpper(character))
            {
                result += counter * (validNhiChars.IndexOf(character) + 1);
            }
            else
            {
                result += counter * (int)char.GetNumericValue(character);
            }
            counter--;
        }
        int checkSum = divider - result % divider;
        var actual = isNewFormat ? validNhiChars[checkSum].ToString() : checkSum.ToString();
        actual = actual == "10" ? "0" : actual;

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Generate_NewNHI_ReturnStringWith6th7thCharactersBeingLetters()
    {
        // Arrange
        var config = new Mock<IGeneratorConfiguration>();
        config.Setup(c => c.IsNewFormat).Returns(true);
        var generator = new NHIGenerator(config.Object);

        // Act
        var actual = generator.Generate().Substring(5, 2).AsEnumerable();

        // Assert
        Assert.Contains(actual, c => char.IsUpper(c));
    }

}
