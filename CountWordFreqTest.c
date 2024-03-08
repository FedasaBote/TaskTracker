// See https://aka.ms/new-console-template for more information

using NUnit.Framework;
using System.Collections.Generic;
using Program;

[TestFixture]
public class WordFrequencyTests
{
    [Test]
    public void CountWordFrequencies_EmptyInput_ReturnsEmptyDictionary()
    {
        // Arrange
        string input = "";

        // Act
        var result = Program.Program.CountWordFrequencies(input);

        // Assert
        Assert.That(result, Is.Empty);
    }

    [Test]
    public void CountWordFrequencies_SingleWordInput_ReturnsDictionaryWithOneEntry()
    {
        // Arrange
        string input = "hello";

        // Act
        var result = Program.Program.CountWordFrequencies(input);

        // Assert
        Assert.That(result, Has.Count.EqualTo(1));
        Assert.That(result, Does.ContainKey("hello"));
        Assert.That(result["hello"], Is.EqualTo(1));
    }

    [Test]
    public void CountWordFrequencies_MultipleWordsWithPunctuation_ReturnsCorrectFrequencies()
    {
        // Arrange
        string input = "Hello, world! Hello, there.";

        // Act
        var result = Program.Program.CountWordFrequencies(input);

        // Assert
        Assert.That(result, Has.Count.EqualTo(3));
        Assert.That(result, Does.ContainKey("hello"));
        Assert.That(result, Does.ContainKey("world"));
        Assert.That(result, Does.ContainKey("there"));
        Assert.That(result["hello"], Is.EqualTo(2));
        Assert.That(result["world"], Is.EqualTo(1));
        Assert.That(result["there"], Is.EqualTo(1));
    }
}


