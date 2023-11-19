using System.Reflection;
using CB.AssemblyExtensions.TestAssembly;

namespace CB.AssemblyExtensions.Test;

public class Tests
{
    const string internalEmbeddedResourceFile = "EmbeddedTestFile.txt";
    const string externalEmbeddedResourceFile = "ExternalEmbeddedTestFile.txt";

    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void TestWithTypeOfAssemblyForInternalFile()
    {
        var assembly = typeof(Tests).Assembly;
        
        var result = assembly!.ReadAllTextFromEmbeddedFile(internalEmbeddedResourceFile);

        Assert.That(result, Is.EqualTo("Bla Bla Bla"));
    }

    [Test]
    public void TestWithTypeOfAssemblyForExternalFile()
    {
        var assembly = typeof(ExternalAssemblyClass).Assembly;
        
        var result = assembly!.ReadAllTextFromEmbeddedFile(externalEmbeddedResourceFile);

        Assert.That(result, Is.EqualTo("Bla Bla Bla"));
    }

    [Test]
    public void TestWithGetAssemblyForInternalFile()
    {
        var assembly = Assembly.GetAssembly(typeof(Tests));
        
        var result = assembly!.ReadAllTextFromEmbeddedFile(internalEmbeddedResourceFile);

        Assert.That(result, Is.EqualTo("Bla Bla Bla"));
    }

    [Test]
    public void TestWithGetAssemblyForExternalFile()
    {
        var assembly = Assembly.GetAssembly(typeof(ExternalAssemblyClass));
        
        var result = assembly!.ReadAllTextFromEmbeddedFile(externalEmbeddedResourceFile);

        Assert.That(result, Is.EqualTo("Bla Bla Bla"));
    }

    [Test]
    public void TestWithGetExecutingAssemblyForInternalFile()
    {
        var assembly = Assembly.GetExecutingAssembly();
        
        var result = assembly!.ReadAllTextFromEmbeddedFile(internalEmbeddedResourceFile);

        Assert.That(result, Is.EqualTo("Bla Bla Bla"));
    }

    [Test]
    public void TestThrowExceptionForForExternalFileWithGetExecutingAssembly()
    {
        var assembly = Assembly.GetExecutingAssembly();
        
        Assert.Catch<Exception>(
            () => assembly!.ReadAllTextFromEmbeddedFile(externalEmbeddedResourceFile),
            $"Embeded resource name '{externalEmbeddedResourceFile}' not found in assembly '{typeof(ExternalAssemblyClass).Assembly.FullName}'.");
    }
}